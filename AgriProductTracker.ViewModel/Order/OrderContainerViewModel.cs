using AgriProductTracker.ViewModel.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgriProductTracker.ViewModel.Order
{
    public class OrderContainerViewModel
    {
        public OrderContainerViewModel()
        {
            OrderItems = new List<ProductViewModel>();
        }
        public List<ProductViewModel> OrderItems { get; set; }
        public int Id { get; set; }
        public string CardNumber { get; set; }
        public string ExperationDate { get; set; }
        public string Cvv { get; set; }
        public string ShippingAddress { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public decimal Amount { get; set; }
    }
}
