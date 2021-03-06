using AgriProductTracker.ViewModel;
using AgriProductTracker.ViewModel.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgriProductTracker.Business.Interfaces
{
    public interface IOrderService
    {
        List<OrderViewModel> GetAllOrders();
        Task<OrderConfirmResponseViewModel> ConfirmOrder(int orderId, int deliveryPartnerId);
        OrderViewModel GetOrderById(int id);
    }
}
