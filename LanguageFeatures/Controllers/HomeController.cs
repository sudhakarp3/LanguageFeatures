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
            return View("Result",(object)String.Format("Product Name{0}",strProdName));
        }
        public ViewResult CreateProduct()
        {
            Product myProduct = new Product();
            myProduct.ProductID = 100;
            myProduct.Name = "Kayak";
            myProduct.Description = "A boat for one person";
            myProduct.Price = 275M;
            myProduct.Category = "Watersports";
            return View("Result",(object)String.Format("Category: {0}", myProduct.Category));
            

        }
        public ViewResult CreateProductObjectInitializer()
        {
            Product myProduct = new Product { ProductID=100,Name="kkk",Description="check",Price=22,Category="wsports"};
            
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
    }