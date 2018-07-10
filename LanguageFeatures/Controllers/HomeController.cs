using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LanguageFeatures.Models;

namespace LanguageFeatures.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public String Index()
        {
            return "Navigate to a URL to show an example";
        }
        public ViewResult AutoProperty()
        {
            Product myProduct = new Product();
            myProduct.Name = "Test prod";
            String strProdName = myProduct.Name;
            return View("Result", (object)String.Format("Product Name{0}", strProdName));
        }
        public ViewResult CreateProduct()
        {
            Product myProduct = new Product();
            myProduct.ProductID = 100;
            myProduct.Name = "Kayak";
            myProduct.Description = "A boat for one person";
            myProduct.Price = 275M;
            myProduct.Category = "Watersports";
            return View("Result", (object)String.Format("Category: {0}", myProduct.Category));


        }
        public ViewResult CreateProductObjectInitializer()
        {
            Product myProduct = new Product { ProductID = 100, Name = "kkk", Description = "check", Price = 22, Category = "wsports" };

            return View("Result", (object)String.Format("Category: {0}", myProduct.Category));


        }
        public ViewResult CreateCollection()
        {
            string[] stringArray = { "apple", "orange", "plum" };

            List<int> intList = new List<int> { 10, 20, 30, 40 };

            Dictionary<string, int> myDict = new Dictionary<string, int> {
                { "apple", 10 }, { "orange", 20 }, { "plum", 30 }
            };

            return View("Result", (object)stringArray[1]);

        }
        public ViewResult UseExtension()
        {
            ShoppingCart shop = new ShoppingCart
            {
                Products = new List<Product>
                {
                    new Product{Name="1",Price=12},
                    new Product{Name="2",Price=13},
                    new Product{Name="3",Price=14}
                }
            };
            decimal totPrice = shop.TotalPrices();
            return View("Result", (object)String.Format("Total Price: {0:c}", totPrice));
            
        }
        public ViewResult UseExtensionEnumerable()
        {

            IEnumerable<Product> products = new ShoppingCart
            {
                Products = new List<Product> {
                    new Product {Name = "Kayak", Price = 275M},
                    new Product {Name = "Lifejacket", Price = 48.95M},
                    new Product {Name = "Soccer ball", Price = 19.50M},
                    new Product {Name = "Corner flag", Price = 34.95M}
                }
            };
            
            // create and populate an array of Product objects
            Product[] productArray = {
                new Product {Name = "Kayak", Price = 275M},
                new Product {Name = "Lifejacket", Price = 48.95M},
                new Product {Name = "Soccer ball", Price = 19.50M},
                new Product {Name = "Corner flag", Price = 34.95M}
            };

            // get the total value of the products in the cart
            decimal cartTotal = products.TotalPrices();
            decimal arrayTotal = products.TotalPrices();

            return View("Result",
                (object)String.Format("Cart Total: {0}, Array Total: {1}",
                    cartTotal, arrayTotal));
        }
        public ViewResult UseFilterExtensionMethod()
        {
            IEnumerable<Product> products = new ShoppingCart
            {
                Products = new List<Product> {
                    new Product {Name = "Kayak", Category = "Watersports", Price = 275M},
                    new Product {Name = "Lifejacket", Category = "Watersports",
                       Price = 48.95M},
                    new Product {Name = "Soccer ball", Category = "Soccer",
                       Price = 19.50M},
                    new Product {Name = "Corner flag", Category = "Soccer",
                       Price = 34.95M}
                }
            };

            decimal total = 0;
            foreach (Product prod in products.FilterByCategory("Soccer"))
            {
                total += prod.Price;
            }

            return View("Result", (object)String.Format("Total: {0}", total));

        }
        public ViewResult UseFilterExtensionFunc()
        {
            // create and populate ShoppingCart
            IEnumerable<Product> products = new ShoppingCart
            {
                Products = new List<Product> {
                    new Product {Name = "Kayak", Category = "Watersports", Price = 275M},
                    new Product {Name = "Lifejacket", Category = "Watersports", Price = 48.95M},
                    new Product {Name = "Soccer ball", Category = "Soccer", Price = 19.50M},
                    new Product {Name = "Corner flag", Category = "Soccer", Price = 34.95M}
                }
            };
            /*Func<Product, bool> categoryFilter = delegate (Product prod) {
                return prod.Category == "Soccer";
            };*/
            Func<Product, bool> categoryFilter = prod => prod.Category == "Soccer";


            decimal total = 0;

            //foreach (Product prod in products.Filter(categoryFilter))
            foreach( Product prod in products.Filter(prod => prod.Category == "Soccer"))
            {
                total += prod.Price;
            }
            return View("Result", (object)String.Format("Total: {0}", total));

           
        }


    }
} 


    
   