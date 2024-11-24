using BaronCoffee.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace BaronCoffee.Controllers
{
    public class CategoryController : Controller
    {
        private MyStoreEntities1 db = new MyStoreEntities1();
        // GET: Category
        public ActionResult Index()
        {
            var categories = db.Categories;
            return View(categories.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new
                HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }
      
    }
}