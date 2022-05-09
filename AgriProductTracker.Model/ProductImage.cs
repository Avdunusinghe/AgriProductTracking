using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgriProductTracker.Model
{
    public class ProductImage
    {
        public long Id { get; set; }
        public long ProductId { get; set; }
        public string Attachment { get; set; }
        public string AttachementName { get; set; }
        public virtual Product Product { get; set; }
    }
}
