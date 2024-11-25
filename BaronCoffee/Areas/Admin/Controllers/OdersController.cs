using BaronCoffee.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BaronCoffee.Areas.Admin.Controllers
{
    public class OdersController : Controller
    {
        private MyStoreEntities db = new MyStoreEntities();
        // GET: Admin/Oders
        public ActionResult Index()
        {
            // Lấy danh sách tất cả đơn hàng, bao gồm thông tin khách hàng
            var orders = db.Orders.Include("Customer").ToList();
            return View(orders);
        }

        // GET: Order/Detail/5
        public ActionResult Detail(int id)
        {
            // Lấy thông tin chi tiết của đơn hàng theo ID
            var order = db.Orders
                          .Include("OrderDetails.Product") // Bao gồm thông tin sản phẩm trong chi tiết đơn hàng
                          .Include("Customer")             // Bao gồm thông tin khách hàng
                          .FirstOrDefault(o => o.OrderID == id);

            if (order == null)
            {
                return HttpNotFound(); // Trả về 404 nếu không tìm thấy đơn hàng
            }

            return View(order);
        }
    }
}