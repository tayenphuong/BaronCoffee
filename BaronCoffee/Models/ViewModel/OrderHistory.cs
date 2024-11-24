using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaronCoffee.Models.ViewModel
{
    public class OrderHistory
    {
        public int OrderID { get; set; }
        public int UserID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public string Status { get; set; }  // Trạng thái đơn hàng (Đang chờ, Hoàn thành)
        public decimal TotalAmount { get; set; }
        public DateTime OrderDate { get; set; }
    }
}