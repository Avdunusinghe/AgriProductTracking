using AgriProductTracker.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgriProductTracker.Business.Interfaces
{
    public interface ICurrentUserService
    {
        User GetUserByUsername(string userName);
    }
}
