using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgriProductTracker.Model.Common.Enums
{
    public enum CardType
    {
        [Description("VISA")]
        InStock = 1,
        [Description("Master")]
        OutOfStock = 2,
    }
}
