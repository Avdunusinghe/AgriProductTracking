using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgriProductTracker.Model
{
    public class OrderItem
    {
        public long Id { get; set; }
        public long ProductId { get; set; }
        public long OrderId { get; set; }
        public int NumberOfItems { get; set; }

        public virtual Product Product { get; set; }
        public virtual Order Order { get; set; }

    }
}
