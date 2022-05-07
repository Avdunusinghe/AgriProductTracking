using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgriProductTracker.Model
{
    public class User
    {

        public long Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime LastLoginDate { get; set; }
        public string ProfileImage { get; set; }
        public string Address { get; set; }
        public int LoginsessionId { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreateOn { get; set; }
        public long? CreatedById { get; set; }
        public DateTime UpdatedOn { get; set; }
        public long? UpdatedById { get; set; }



        public virtual User CreatedBy { get; set; }
        public virtual User UpdatedBy { get; set; }


        public virtual ICollection<UserRole> UserRoles { get; set; }
        public virtual ICollection<UserRole> CreatedUserRoles { get; set; }
        public virtual ICollection<UserRole> UpdatedUserRoles { get; set; }

        public virtual ICollection<User> CreatedUsers { get; set; }
        public virtual ICollection<User> UpdatedUsers { get; set; }


        #region Navigation Property Product
        public virtual ICollection<Product> CreatedProducts { get; set; }
        public virtual ICollection<Product> UpdatedProducts { get; set; }

        #endregion

        #region Navigation Property Payment
        public virtual ICollection<Payment> CreatedPayments { get; set; }
        public virtual ICollection<Payment> UpdatedPayments { get; set; }

        #endregion

        #region Navigation Property DeliveryService
        public virtual ICollection<DeliveryService> CreatedDeliveryServices { get; set; }
        public virtual ICollection<DeliveryService> UpdatedDeliveryServices { get; set; }
        #endregion

        #region Navigation Property Orders
        public virtual ICollection<Order> PlaceOders { get; set; } 
        #endregion
    }
}
