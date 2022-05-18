using AgriProductTracker.Business.Interfaces;
using AgriProductTracker.Data.Data;
using AgriProductTracker.ViewModel;
using AgriProductTracker.ViewModel.Order;
using AgriProductTracking.util;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AgriProductTracker.Business
{
    public class OrderService : IOrderService
    {
        #region Private Members
        private readonly AgriProductTrackerDbContext _db;
        private readonly IConfiguration _configuration;
        private readonly ICurrentUserService _currentUserService;
        #endregion

        #region Constructor
        public OrderService(AgriProductTrackerDbContext _db, IConfiguration _configuration, ICurrentUserService _currentUserService)
        {
            this._db = _db;
            this._configuration = _configuration;
            this._currentUserService = _currentUserService;
        }
        #endregion

        #region Business Services Methods
        public List<OrderViewModel> GetAllOrders()
        {
            var dataSet = new List<OrderViewModel>();

            try
            {
                var query = _db.Orders.Where(o => o.IsProceesed == false).OrderBy(d => d.DateTime);

                var orderList = query.ToList();

                foreach (var item in orderList)
                {
                    var orderDetails = new OrderViewModel();

                    orderDetails.Id = item.Id;
                    orderDetails.Amount = item.TotalPrice;
                    orderDetails.DeliveryServiceId = item.DeleveryServiceId;
                    orderDetails.CutomerName = item.Customer.FullName;

                    var orderItems = item.OrderItems.ToList();

                    foreach (var orderItem in orderItems)
                    {
                        var product = _db.Products.Where(x => x.Id == orderItem.ProductId).FirstOrDefault();

                        var itemDetails = new OrderItemViewModel();

                        itemDetails.Id = orderItem.Id;
                        itemDetails.ProductId = orderItem.ProductId;
                        itemDetails.NumberOfItems = orderItem.NumberOfItems;

                        var productImages = product.ProductImages.ToList();

                        foreach (var image in productImages)
                        {
                            if (!string.IsNullOrEmpty(image.AttachementName))
                            {
                                var productImage = string.Format
                                (
                                    @"{0}{1}\{2}\{3}",
                                    _configuration.GetSection("FileUploadPath").Value,
                                    FolderNames.PRODUCT,
                                    product.Id,
                                    image.AttachementName
                                );

                                if (File.Exists(productImage))
                                {
                                    itemDetails.ProductImage.Id = image.Id;
                                    itemDetails.ProductImage.AttachmentName = image.AttachementName;
                                    itemDetails.ProductImage.Attachment = "data:image/jpg;base64," + ImageHelper.getThumnialImage(productImage);
                                }

                            }
                            break;
                        }

                        orderDetails.OrderItems.Add(itemDetails);
                    }


                    dataSet.Add(orderDetails);

                }


            }
            catch (Exception ex)
            {

            }

            return dataSet;
        }

        public async Task<ResponseViewModel> ConfirmOrder(int orderId, int deliveryServiceId)
        {
            var response = new ResponseViewModel();

            try
            {
                var order = _db.Orders.Where(x=>x.IsProceesed == false && x.Id == orderId).FirstOrDefault();

                if(order != null)
                {
                    order.IsProceesed = true;

                    _db.Orders.Update(order);

                    var smsServiceApiUrl = string.Empty;
                    
                    HttpClient client = new HttpClient();
                    HttpResponseMessage apiResponse = await client.GetAsync(smsServiceApiUrl);
                    apiResponse.EnsureSuccessStatusCode();

                    //HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(smsServiceApiUrl);
                    //httpWebRequest.Method = "POST";
                    //httpWebRequest.ContentType = "application/json";
                    //httpWebRequest.ContentLength = 0;
                    //using (Stream webStream = httpWebRequest.GetRequestStream())
                    //using (StreamWriter requestWriter = new StreamWriter(webStream, System.Text.Encoding.ASCII))
                    //{
                    //    requestWriter.Write(string.Empty);
                    //}

                    //WebResponse webResponse = httpWebRequest.GetResponse();
                    //using (Stream webStream = webResponse.GetResponseStream() ?? Stream.Null)
                    //using (StreamReader responseReader = new StreamReader(webStream))
                    //{
                    //    string apiResponse = responseReader.ReadToEnd();
                    //    Console.Out.WriteLine(apiResponse);
                    //}

                    response.IsSuccess = true;
                    response.Message = "Order Confirm Successfull";
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Order Not Found";
                }

                await _db.SaveChangesAsync();

            }
            catch (Exception ex)
            {

            }

            return response;
        }
        #endregion
    }
}
