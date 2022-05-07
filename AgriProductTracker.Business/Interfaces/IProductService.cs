using AgriProductTracker.ViewModel;
using AgriProductTracker.ViewModel.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgriProductTracker.Business.Interfaces
{
    public interface IProductService
    {
        Task<ResponseViewModel> ProductSave(ProductViewModel vm, string userName);
        Task<ResponseViewModel> ProductDelete (long id);

    }
}
