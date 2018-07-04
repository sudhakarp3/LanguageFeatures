﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LanguageFeatures.Models
{
    public class ShoppingCart
    {
        public List<Product> Products { get; set; }
        public  decimal TotalPrices( ShoppingCart cartParam)
        {
            decimal total = 0;
            foreach (Product prod in cartParam.Products)
            {
                total += prod.Price;
            }
            return total;
        }

    }
}