﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgriProductTracker.ViewModel
{
    public class ProductViewModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public long CategoryId { get; set; }

        public Availability Availability { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public virtual ProductCategory ProductCategory { get; set; }
    }
}