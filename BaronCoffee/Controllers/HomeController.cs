using BaronCoffee.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using BaronCoffee.Models.ViewModel;
using System.Net;
using System.Web.UI;


namespace BaronCoffee.Controllers
{
    public class HomeController : Controller
    {

        private MyStoreEntities db = new MyStoreEntities();
        public ActionResult DiaChi()
        {

            return View();
        }
        public ActionResult TuyenDung()
        {

            return View();
        }
        public ActionResult TuyenDungChiTiet()
        {

            return View();
        }

        public ActionResult Index(string SearchTerm, int? page)
        {
            var model = new HomeProduct__2VM();
            var products = db.Products.AsQueryable();
            if (!string.IsNullOrEmpty(SearchTerm))
            {
                model.SearchTerm = SearchTerm;
                products = products.Where(p => p.ProductName.Contains(SearchTerm) ||
                                         p.ProductDescription.Contains(SearchTerm) ||
                                         p.Category.CategoryName.Contains(SearchTerm));


                int pageNumer = page ?? 1;
                int pageSize = 5;


                model.Products = products.OrderByDescending(p => p.OrderDetails.Count()).Take(5).ToPagedList(pageNumer, pageSize);
            }



            model.FeaturedProducts = db.Products.OrderByDescending(p => p.OrderDetails.Count()).Take(5).ToList();

            model.Categories = db.Categories.ToList();

            return View(model);

        }
        public ActionResult ProductsByCategory(int? categoryId, int? page, decimal? minPrice, decimal? maxPrice, string sortOder)
        {
            var model = new HomeProduct__2VM();
            var products = db.Products.AsQueryable();
            var categories = db.Categories.AsQueryable();
            if (categoryId.HasValue)
            {
                products = products.Where(p => p.CategoryID == categoryId);
            }


            if (minPrice.HasValue)
            {
                products = products.Where(p => p.ProductPrice >= minPrice.Value);
            }
            if (maxPrice.HasValue)
            {
                products = products.Where(p => p.ProductPrice <= maxPrice.Value);
            }
            switch (sortOder)
            {
                case "name_asc": products = products.OrderBy(p => p.ProductName); break;

                case "name_desc": products = products.OrderByDescending(p => p.ProductName); break;

                case "price_asc": products = products.OrderBy(p => p.ProductPrice); break;

                case "price_desc": products = products.OrderByDescending(p => p.ProductPrice); break;

                default:
                    products = products.OrderBy(p => p.ProductID); break;
            }

            int pageNumer = page ?? 1; // Trang mặc định là 1
            int pageSize = 12; // Số sản phẩm mỗi trang

            model.Products = products.ToPagedList(pageNumer, pageSize);
            model.Categories = db.Categories.ToList();

            // Lưu các tham số vào ViewModel
            model.SortOder = sortOder;
            model.MinPrice = minPrice;
            model.MaxPrice = maxPrice;
            model.CategoryId = categoryId;




            return View("ProductsByCategory", model);
        }
        public ActionResult ProductDetail(int? id, int? quatity)
        {
            var model = new ProductDetail();
            model.Categories = db.Categories.ToList();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Product pro = db.Products.Find(id);

            if (pro == null)
            {
                return HttpNotFound();
            }

            var products = db.Products.Where(p => p.CategoryID == pro.CategoryID && p.ProductID != pro.ProductID).AsQueryable();



            model.product = pro;


            if (quatity.HasValue)
            {
                model.quantity = quatity.Value;
            }
            return View(model);


        }
        public ActionResult CategoryPage()
        {
            var model = new CategoryPageVM();

            // Lấy tất cả danh mục
            var categories = db.Categories.ToList();

            // Tạo danh sách danh mục với sản phẩm nổi bật
            model.CategoriesWithProducts = categories.Select(category => new CategoryWithProductsVM
            {
                CategoryID = category.CategoryID,
                CategoryName = category.CategoryName,
                FeaturedProducts = db.Products
                    .Where(p => p.CategoryID == category.CategoryID) // Lọc sản phẩm theo CategoryID
                    .OrderByDescending(p => p.OrderDetails.Count())  // Sắp xếp theo số lượng đặt hàng
                    .Take(3)                                         // Lấy 3 sản phẩm đầu tiên
                    .ToList()
            }).ToList();

            return View(model);
        }

    }
}