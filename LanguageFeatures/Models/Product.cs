using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LanguageFeatures.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public string name { get; set; }
        public string Name
        {
            get
            {
                return ProductID + name;
            }
            set
            {
                name = value;
            }
        }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Category { set; get; }
    }
}