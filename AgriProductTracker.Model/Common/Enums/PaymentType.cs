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
        CreditCard = 1,
        [Description("Mobile Number")]
        MobileCharge = 2,
    }
}
