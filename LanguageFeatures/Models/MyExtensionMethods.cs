using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LanguageFeatures.Models
{
    public static class MyExtensionMethods
    {
        public static decimal TotalPrices(this ShoppingCart cartParam)
        {
            decimal total = 0;
            foreach(Product prod in cartParam.Products)
            {
                total += prod.Price;
            }
            return total;
        }
        public static IEnumerable<Product>  FilterByCategory(this IEnumerable<Product> productEnum, String FilterValue)
        {
            foreach (Product prod in productEnum)
            {
                if (prod.Category == FilterValue)
                {
                    yield return prod;
                }
            }

        }
        public static IEnumerable<Product> Filter(this IEnumerable<Product> prodEnum, Func<Product, bool> selectorParam)
        {
            foreach (Product prod in prodEnum)
                if (selectorParam(prod))
                    yield return prod;
        }

    }
}