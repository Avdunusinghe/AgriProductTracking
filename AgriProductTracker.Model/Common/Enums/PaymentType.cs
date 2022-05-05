using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgriProductTracker.Model.Common.Enums
{
    public enum PaymentType
    {
        [Description("Credit Card")]
        InStock = 1,
        [Description("Mobile Number")]
        OutOfStock = 2,
    }
}
