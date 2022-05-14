using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgriProductTracker.ViewModel.Product
{
    public class ProductViewModel
    {
        public ProductViewModel()
        {
            ProductImages = new List<ProductImageViewModel>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string CreatedByName { get; set; }
        public string UpdatedByName { get; set; }
        public List<ProductImageViewModel> ProductImages { get; set; }

    }


    public class ProductImageViewModel
    {
        public long Id { get; set; }
        public string Attachment { get; set; }
        public string AttachmentName { get; set; }
    }
}
