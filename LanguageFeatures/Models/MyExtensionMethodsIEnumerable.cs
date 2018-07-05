using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LanguageFeatures.Models
{
    public static class MyExtensionMethodsIEnumerable
    {
        public static decimal TotalPrices(this IEnumerable<Product>prodctEnum)
        {
            decimal total = 0;
            foreach(Product prod in prodctEnum)
            {
                total += prod.Price;
            }
            return total;
        }
    }
}