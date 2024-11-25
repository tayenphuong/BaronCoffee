using BaronCoffee.Models;// Đường dẫn tới model và DbContext
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BaronCoffee.Areas.Admin.Controllers
{
    public class StatisticsController : Controller
    {
        // GET: Admin/Statistics

        private MyStoreEntities db = new MyStoreEntities();

        // Action: Thống kê đơn hàng của khách hàng
        public ActionResult CustomerStatistics(int customerId)
        {
            // Lấy thông tin khách hàng
            var customer = db.Customers.Find(customerId);
            if (customer == null)
            {
                return HttpNotFound("Không tìm thấy khách hàng.");
            }

            // Đếm số lượng đơn hàng của khách hàng
            int orderCount = db.Orders.Count(o => o.CustomerID == customerId);

            // Truyền dữ liệu vào view
            ViewBag.CustomerName = customer.CustomerName;
            ViewBag.OrderCount = orderCount;

            return View();
        }
    }
}