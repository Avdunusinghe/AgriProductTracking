using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgriProductTracker.ViewModel.User
{
    public class UserFilterViewModel
    {
        public int RoleId { get; set; }
        public string SearchText { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
    }
}
