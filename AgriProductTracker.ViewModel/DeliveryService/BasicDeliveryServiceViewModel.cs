using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgriProductTracker.ViewModel.DeliveryService
{
    public class BasicDeliveryServiceViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string TelePhoneNumber { get; set; }
        public string DiliveryDetails { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedById { get; set; }
        public string CreatedByName { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string UpdatedByName { get; set; }
        public int UpdatedById { get; set; }
        
    }
}
