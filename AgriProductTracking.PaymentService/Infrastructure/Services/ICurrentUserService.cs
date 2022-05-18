using AgriProductTracker.Model;

namespace AgriProductTracking.PaymentService.Infrastructure.Services
{
    public interface ICurrentUserService
    {
        User GetUserByUsername(string userName);
    }
}
