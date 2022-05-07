using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgriProductTracker.Model
{
    public class Order
    {
        public long Id { get; set; }
        public decimal TotalPrice { get; set; }
        public long DeleveryServiceId { get; set; }
        public long CustomerId { get; set; }
        public DateTime DateTime { get; set; }
        public bool IsProceesed { get; set; }

        public  virtual User Customer { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }

    }
}
