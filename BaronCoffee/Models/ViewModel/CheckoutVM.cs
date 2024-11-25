using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList.Mvc;
namespace BaronCoffee.Models.ViewModel
{
    public class CheckoutVM
    {
        public List<CartItem> CartItems { get; set; }
        
        public int CustomerID { get; set; }

        public string AddressDelivery { get; set }
    }
}