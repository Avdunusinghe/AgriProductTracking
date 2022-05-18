using AgriProductTracker.Business.Interfaces;
using AgriProductTracker.Data.Data;
using AgriProductTracker.ViewModel.Order;
using AgriProductTracking.util;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgriProductTracker.Business
{
    public class OrderService : IOrderService
    {
        private readonly AgriProductTrackerDbContext _db;
        private readonly IConfiguration _configuration;
        private readonly ICurrentUserService _currentUserService;

        public OrderService(AgriProductTrackerDbContext _db, IConfiguration _configuration, ICurrentUserService _currentUserService)
        {
            this._db = _db;
            this._configuration = _configuration;
            this._currentUserService = _currentUserService;
        }
        public List<OrderViewModel> GetAllOrders()
        {
            var dataSet = new List<OrderViewModel>();

            try
            {
                var query = _db.Orders.Where(o => o.IsProceesed == false).OrderBy(d=>d.DateTime);

                var orderList = query.ToList();

                foreach (var item in orderList)
                {
                    var orderDetails = new OrderViewModel();

                    orderDetails.Id = item.Id;
                    orderDetails.Amount = item.TotalPrice;
                    orderDetails.DeliveryServiceId = item.DeleveryServiceId;
                    orderDetails.CutomerName = item.Customer.FullName;

                    var orderItems = item.OrderItems.ToList();

                    foreach(var orderItem in orderItems)
                    {
                        var product = _db.Products.Where(x=>x.Id == orderItem.ProductId).FirstOrDefault();

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
                                    itemDetails.productImage.Id = image.Id;
                                    itemDetails.productImage.AttachmentName = image.AttachementName;
                                    itemDetails.productImage.Attachment = "data:image/jpg;base64," + ImageHelper.getThumnialImage(productImage);
                                }

                            }
                            break;
                        }

                        orderDetails.OrderItems.Add(itemDetails);
                    }


                 dataSet.Add(orderDetails);
                       
                }


            }catch (Exception ex)
            {

            }

            return dataSet;
        }
    }
}
