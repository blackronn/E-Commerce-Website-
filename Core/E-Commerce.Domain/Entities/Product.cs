﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Entities
{
    public class Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string ImageUrl { get; set; }
        public int CategoryID { get; set; }
        //public Category Category { get; set; }
    }
}
