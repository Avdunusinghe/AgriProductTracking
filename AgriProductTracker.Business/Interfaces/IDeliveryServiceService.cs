using AgriProductTracker.ViewModel;
using AgriProductTracker.ViewModel.DeliveryService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgriProductTracker.Business.Interfaces
{
   public interface IDeliveryServiceService
    {
        Task<ResponseViewModel> DeliveryServiceSave(DeliveryServiceViewModel vm, string userName);
        Task<ResponseViewModel> DeliveryServiceDelete(long id);
        DeliveryServiceViewModel GetDeliveryServicebyId(int id);
        List<DeliveryServiceViewModel> GetAllDeliveryServiceList();
        List<DropDownViewModel> GetAllDeliveryServices();
        PaginatedItemsViewModel<BasicDeliveryServiceViewModel> GetDeliveryServiceList(string searchText, int currentPage, int pageSize, int deliveryserviceId);
    }
}
