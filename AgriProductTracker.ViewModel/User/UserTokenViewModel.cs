using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgriProductTracker.ViewModel.User
{
    public class UserTokenViewModel
    {
        public UserTokenViewModel()
        {
            Roles = new List<string>();
        }

        public bool IsLoginSuccess { get; set; }
        public string LoginMessage { get; set; }
        public string Token { get; set; }
        public string DisplayName { get; set; }
        public List<string> Roles { get; set; }
    }
}
