﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class ProductObject
    {

        public int ProductId { get; set; }

        public int CategoryId { get; set; }

        public string ProductName { get; set; } = null!;

        public string Weight { get; set; } = null!;

        public decimal UnitPrice { get; set; }

        public int UnitsInStock { get; set; }
    }
}
