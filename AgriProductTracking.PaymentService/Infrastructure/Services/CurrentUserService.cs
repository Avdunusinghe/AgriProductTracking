using AgriProductTracker.Data.Data;
using AgriProductTracker.Model;

namespace AgriProductTracking.PaymentService.Infrastructure.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly AgriProductTrackerDbContext _db;

        public CurrentUserService(AgriProductTrackerDbContext _db)
        {
            this._db = _db;
        }

        public User GetUserByUsername(string userName)
        {
            var user = _db.Users.FirstOrDefault(x => x.UserName == userName);

            return user;
        }
    }
}
