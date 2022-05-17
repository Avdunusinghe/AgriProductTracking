using AgriProductTracker.Model.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgriProductTracker.Model
{
    public class Order
    {
        public int Id { get; set; }
        public decimal TotalPrice { get; set; }
        public int DeleveryServiceId { get; set; }
        public int CustomerId { get; set; }
        public Province Province { get; set; }
        public string ShippingAddress { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public DateTime DateTime { get; set; }
        public bool IsProceesed { get; set; }

        public  virtual User Customer { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }

    }
}
