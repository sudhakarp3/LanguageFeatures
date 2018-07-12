using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LanguageFeatures.Models;
using System.Text;

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
            //foreach( Product prod in products.Filter(prod => prod.Category == "Soccer"))
            foreach (Product prod in products
            .Filter(prod => prod.Category == "Soccer" && prod.Price > 20))
            {
                total += prod.Price;
            }

            return View("Result", (object)String.Format("Total: {0}", total));




        }

        public ViewResult CreateAnonArray()
        {
            //Using Automatic Type Inference 
            var myVariable = new Product { Name = "Kayak", Category = "Watersports", Price = 275M };
            string name = myVariable.Name; // legal

            //Creating an Array of Anonymously Typed Objects in the HomeController.cs File
            var oddsAndEnds = new[] {
                new { Name = "MVC", Category = "Pattern"},
                new { Name = "Hat", Category = "Clothing"},
                new { Name = "Apple", Category = "Fruit"}
            };
            oddsAndEnds.Count();
            StringBuilder result = new StringBuilder();
            foreach (var item in oddsAndEnds)
            {
                result.Append(item.Name).Append(" ");
            }
            return View("Result", (object)result.ToString());
        }
        //usual way to find top 3 products
        /*public ViewResult FindProducts()
        {
            Product[] products = {
new Product {Name = "Kayak", Category = "Watersports", Price = 275M},
new Product {Name = "Lifejacket", Category = "Watersports", Price = 48.95M},
new Product {Name = "Soccer ball", Category = "Soccer", Price = 19.50M},
new Product {Name = "Corner flag", Category = "Soccer", Price = 34.95M}
};
            // define the array to hold the results
            Product[] foundProducts = new Product[3];
            // sort the contents of the array
            Array.Sort(products, (item1, item2) => {
                return Comparer<decimal>.Default.Compare(item1.Price, item2.Price);
            });
            // get the first three items in the array as the results
            Array.Copy(products, foundProducts, 3);
            // create the result
            StringBuilder result = new StringBuilder();
            foreach (Product p in foundProducts)
            {
                result.AppendFormat("Price: {0} ", p.Price);
            }
            return View("Result", (object)result.ToString());
        }*/
        //Linq way to find top 3 products
        //Performing Language Integrated Queries
        public ViewResult FindProducts()
        {
            Product[] products = {
new Product {Name = "Kayak", Category = "Watersports", Price = 275M},
new Product {Name = "Lifejacket", Category = "Watersports", Price = 48.95M},
new Product {Name = "Soccer ball", Category = "Soccer", Price = 19.50M},
new Product {Name = "Corner flag", Category = "Soccer", Price = 34.95M}
};
            var foundProducts = from match in products
                                orderby match.Price descending
                                select new { match.Name, match.Price };
            // create the result
            int count = 0;
            StringBuilder result = new StringBuilder();
            foreach (var p in foundProducts)
            {
                result.AppendFormat("Price: {0} ", p.Price);
                if (++count == 3)
                {
                    break;
                }
            }
            return View("Result", (object)result.ToString());
        }
        public ViewResult FindProductsWithDot()
        {

            Product[] products = {
                new Product {Name = "Kayak", Category = "Watersports", Price = 275M},
                new Product {Name = "Lifejacket", Category = "Watersports", Price = 48.95M},
                new Product {Name = "Soccer ball", Category = "Soccer", Price = 19.50M},
                new Product {Name = "Corner flag", Category = "Soccer", Price = 34.95M}
            };

            var foundProducts = products.OrderByDescending(e => e.Price)
                                    .Take(3)
                                    .Select(e => new { e.Name, e.Price });
            //the value of summ will not changes
            var summ = products.Sum(e => e.Price);

            //Deferred LINQ Queries
            products[2] = new Product { Name = "Stadium", Price = 79600M };

            StringBuilder result = new StringBuilder();
            foreach (var p in foundProducts)
            {
                result.AppendFormat("Price: {0} ", p.Price);
            }
            result.AppendFormat("summ: {0} ", summ);

            return View("Result", (object)result.ToString());
        }


        }
    } 


    
   