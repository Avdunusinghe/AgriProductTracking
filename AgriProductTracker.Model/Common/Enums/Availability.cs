using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgriProductTracker.Model.Common.Enums
{
    public enum Availability
    {
        [Description("In Stock")]
        InStock = 1,
        [Description("Out Of Stock")]
        OutOfStock = 2,
    }
}
