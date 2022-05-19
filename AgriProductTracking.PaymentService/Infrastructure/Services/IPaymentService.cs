using AgriProductTracker.ViewModel;
using AgriProductTracker.ViewModel.Order;

namespace AgriProductTracking.PaymentService.Infrastructure.Services
{
    public interface IPaymentService
    {
        Task<CustomerOrderResponseViewModel> Payment(OrderContainerViewModel model, string userName);
    }
}
