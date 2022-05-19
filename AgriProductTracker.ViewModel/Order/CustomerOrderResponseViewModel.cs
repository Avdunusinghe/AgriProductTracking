using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgriProductTracker.ViewModel.Order
{
    public class CustomerOrderResponseViewModel
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public int CustomerId { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerMobileNumber { get; set; }
    }
}
