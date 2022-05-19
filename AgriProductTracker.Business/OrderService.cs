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
                    orderDetails.DeliveryServiceId = item.DeliveyPartnerId;
                    orderDetails.DateTime = item.DateTime;
                    orderDetails.CutomerName = item.Customer.FullName;
                    orderDetails.Amount = item.TotalPrice;
                    orderDetails.City = item.City;
                    orderDetails.PostalCode = item.PostalCode;
                    orderDetails.ShippingAdderess = item.ShippingAddress;
                    orderDetails.IsProcessed = item.IsProceesed;

                    dataSet.Add(orderDetails);

                }


            }
            catch (Exception ex)
            {

            }

            return dataSet;
        }

        public async Task<OrderConfirmResponseViewModel> ConfirmOrder(int orderId, int deliveryServiceId)
        {
            var response = new OrderConfirmResponseViewModel();

            try
            {
                var order = _db.Orders.Where(x=>x.IsProceesed == false && x.Id == orderId).FirstOrDefault();
                var deliveryService = _db.DeliveryServices.Where(x => x.Id == deliveryServiceId).FirstOrDefault();


                if (order != null)
                {
                    order.IsProceesed = true;

                    _db.Orders.Update(order);

                    response.IsSuccess = true;
                    response.DeliveryServiceId = deliveryService.Id;
                    response.DeliveryServicePhoneNumber = deliveryService.TelePhoneNumber;
                    response.DeliveryServiceEmail = deliveryService.Email;
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

        public OrderViewModel GetOrderById(int id)
        {
            var response = new OrderViewModel();

            try
            {
                var query = _db.Orders.Where(x=>x.Id == id).FirstOrDefault();

                response.Id = query.Id;
                response.Amount = query.TotalPrice;
                //response.DeliveryServiceId = query.DeleveryServiceId;
                response.CutomerName = query.Customer.FullName;

                var orderItems = query.OrderItems.ToList();

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

                    response.OrderItems.Add(itemDetails);
                }



            }
            catch (Exception ex)
            {

            }


            return response;
        }
        #endregion
    }
}
