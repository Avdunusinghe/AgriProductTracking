using AgriProductTracker.Business.Interfaces;
using AgriProductTracker.Data.Data;
using AgriProductTracker.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgriProductTracker.Business
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly AgriProductTrackerDbContext _db;

        public CurrentUserService(AgriProductTrackerDbContext _db)
        {
            this ._db = _db;
        }

        public User GetUserByUsername(string userName)
        {
            var user = _db.Users.FirstOrDefault(x => x.UserName == userName);

            return user;
        }
    }
}
