using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgriProductTracker.ViewModel.Order
{
    public class OrderViewModel
    {
        public OrderViewModel()
        {
            OrderItems = new List<OrderItemViewModel>();
            DeliveryServiceId = new List<OrderItemViewModel>();

        }

       

        public int Id { get; set; }
        public decimal Amount { get; set; }
        public int CutomerId { get; set; }
        public string CutomerName { get; set; }
        public DateTime DateTime { get; set; }
        public bool IsProcessed  { get; set; }
        public string ShippingAdderess { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        
        public List<OrderItemViewModel> DeliveryServiceId { get; set; }

        public List<OrderItemViewModel> OrderItems { get; set; }

    }
}
