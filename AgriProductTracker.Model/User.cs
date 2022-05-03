using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgriProductTracker.Model
{
    public class User
    {
        #region Navigation Property Product
        public virtual ICollection<Product> CreatedUsers { get; set; }
        public virtual ICollection<Product> UpdatedUsers { get; set; }

        #endregion

    }
}
