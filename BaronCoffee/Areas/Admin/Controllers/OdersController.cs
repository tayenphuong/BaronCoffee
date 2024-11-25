using BaronCoffee.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BaronCoffee.Areas.Admin.Controllers
{
    public class OdersController : Controller
    {
        private MyStoreEntities db = new MyStoreEntities();
        // GET: Admin/Oders
        public ActionResult Index(string status= "Tất cả")
        {
            // Lấy danh sách tất cả đơn hàng, bao gồm thông tin khách hàng
            int customerId = 1; // Giả sử bạn có thông tin CustomerID trong phiên hoặc thông qua xác thực
            var orders = db.Orders.Where(o => o.CustomerID == customerId).Include(o => o.OrderDetails.Select(od => od.Product));
                           
            if (status != "Tất cả")
            {
                orders = orders.Where(o => o.PaymentStatus == status);
            }    
            return View(orders.ToList(););
        }

        // GET: Order/Detail/5
        public ActionResult Detail(int id)
        {
            var order = db.Orders
                  .Include(o => o.OrderDetails.Select(od => od.Product))
                  .FirstOrDefault(o => o.OrderID == id);

            if (order == null)
            {
                return HttpNotFound();
            }

            return View(order);
        }
    }
}