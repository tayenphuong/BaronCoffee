using BaronCoffee.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace BaronCoffee.Controllers
{
    public class ProductDetailController : Controller
    {
        private MyStoreEntities1 db = new MyStoreEntities1();
        // GET: ProductDetail
        public ActionResult Index()
        {
            var products = db.Products.ToList(); // Lấy tất cả sản phẩm
            return View(products); // Truyền danh sách sản phẩm tới View
        }
        public ActionResult Details(int id)
        {
            // Tìm sản phẩm theo ID
            var product = db.Products.FirstOrDefault(p => p.ProductId == id);
            if (product == null)
            {
                return HttpNotFound(); // Trả về lỗi nếu không tìm thấy
            }

            return View(product); // Truyền sản phẩm tới View
        }
    }
}