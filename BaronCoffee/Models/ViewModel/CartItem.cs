using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaronCoffee.Models
{
    public class CartItem
    {
        public int ProductId { get; set; }


        public string ProductName { get; set; }


        public decimal productPrice { get; set; }




        public int Quantity { get; set; }



        public decimal UnitPrice { get; set; }



        public string ProductImage { get; set; }


        //Tổng giá cho mỗi sản phẩm
        public decimal TotalPrice => Quantity * UnitPrice;
    }    
    
}