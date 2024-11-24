using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaronCoffee.Models.ViewModel
{
    public class OrderHistory
    {
        public int OrderID { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
        public decimal TotalAmount { get; set; }
        public List<CartItem> Products { get; set; }  // Danh sách sản phẩm
    }

}