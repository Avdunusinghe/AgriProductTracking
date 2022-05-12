using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgriProductTracker.ViewModel.User
{
    public  class UserViewModel
    {
        public UserViewModel()
        {
            Roles = new List<int>();
            
        }

        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public bool IsActive { get;  set; }
        //public DateTime CreatedOn { get; set; }
        //public int? CreatedById { get; set; }
        //public string CreatedByName { get; set; }
       // public DateTime UpdatedOn { get; set; }
       // public string UpdatedByName { get; set; }
       // public int? UpdatedById { get; set; }

       public List<int> Roles { get; set; }
        //public List<UserImageViewModel> UserImages { get; set; }

  
    }

}
/*
public class UserImageViewModel
{
    public long Id { get; set; }
    public string Attachment { get; set; }
    public string AttachmentName { get; set; }
}
*/
