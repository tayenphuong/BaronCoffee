using BaronCoffee.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;


namespace BaronCoffee.Areas.Admin.Controllers
{
    public class StatisticsController : Controller
    {
        // GET: Admin/Statistics

        private MyStoreEntities db = new MyStoreEntities();

        public ActionResult Index()
        {
            // Lấy danh sách khách hàng và đơn hàng từ cơ sở dữ liệu
            var customers = db.Customers.ToList(); 
            var orders = db.Orders.ToList();       

            // Thống kê số lượng đơn hàng của từng khách hàng
            var statistics = db.Customers
                .Select(c => new CustomerOrderStatistic
            {
                CustomerID = c.CustomerID,
                CustomerName = c.CustomerName,
                CustomerEmail = c.CustomerEmail,
                OrderCount = c.Orders.Count(o => o.CustomerID == c.CustomerID) //tính số lượng đơn hàng
                }).ToList();

            return View(statistics); // Truyền dữ liệu vào View
        }
    }

    // ViewModel cho kết quả thống kê
    public class CustomerOrderStatistic
    {
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public int OrderCount { get; set; }
    }
}
