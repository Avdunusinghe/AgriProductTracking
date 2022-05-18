using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgriProductTracker.ViewModel.Order
{
    public class OrderConfirmResponseViewModel
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public int DeliveryServiceId { get; set; }
        public string DeliveryServiceEmail { get; set; }
        public string DeliveryServicePhoneNumber { get; set; }
    }
}
