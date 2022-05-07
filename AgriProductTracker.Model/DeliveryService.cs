using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgriProductTracker.Model
{
    public  class DeliveryService
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string TelePhoneNumber { get; set; }
        public string DiliveryDetails { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public long CreatedById { get; set; }
        public DateTime UpdatedOn { get; set; }
        public long UpdatedById { get; set; }

        public virtual User CreatedBy { get; set; }
        public virtual User UpdatedBy { get; set; }
    }
}
