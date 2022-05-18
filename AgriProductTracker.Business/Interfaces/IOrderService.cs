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
    }
}
