using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static System.Collections.Specialized.BitVector32;
using System.Web.Mvc;

namespace ThietKeWeb.Models
{
    public class CartService
    {
        //Sử dụng Session để lưu sản phẩm trong cart
        private readonly HttpSessionStateBase session;


        public CartService(HttpSessionStateBase session_Function)
        {
            this.session = session_Function;
        }


        //Lấy ra Cart được lưu trong session của người dùng
        public Cart GetCart()
        {
            var cart = (Cart)session["Cart"];
            if (cart == null)
            {
                cart = new Cart();
                session["Cart"] = cart;
            }
            return cart;
        }



        //ClearCart của người dùng
        public void clearCart()
        {
            session["Cart"] = null;
        }

    }
}
