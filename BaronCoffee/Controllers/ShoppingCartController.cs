using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThietKeWeb.Models;

namespace ThietKeWeb.Controllers
{
    public class ShoppingCartController : Controller
    {
        private MyStoreEntities1 db = new MyStoreEntities1();


        // GET: ShoppingCart
        public ActionResult Index()
        {
            return View();
        }


        //Hàm lấy ra dịch vụ giỏ hàng
        public CartService Get_CartService()
        {
            CartService CrtService = new CartService(Session);
            return CrtService;
        }




        public ActionResult ShoppingCartPage()
        {
            var cart = Get_CartService().GetCart();

            return View(cart);
        }


        //Chức năng thêm vào giỏ hàng
        public ActionResult AddToCart(int id, int quantity = 1)
        {
            var products = db.Products.Find(id);


            var cartItem = new CartItem();


            if (products != null)
            {
                var cartService = Get_CartService();

                
                cartService.GetCart().AddItems(products.ProductID, products.ProductName, products.ProductImage, products.ProductPrice, quantity, products.Category.CategoryName, products.ProductPrice);

            }
            return RedirectToAction("ShoppingCartPage");
        }


        //Chức năng Xóa sản phẩm khỏi giỏ hàng
        public ActionResult DeleteItemsOnCart(int id)
        {
            var cartService = Get_CartService();

            cartService.GetCart().RemoveProductOutCart(id);

            return RedirectToAction("ShoppingCartPage");
        }


        //Chức năng xóa Sản phẩm
        public ActionResult ClearCart()
        {
            Get_CartService().clearCart();

            return RedirectToAction("ShoppingCartPage");
        }


        //Chức năng cập nhật số lượng
        [HttpPost]
        public ActionResult UpdateQuantity(int id, int quantity)
        {
            var cartService = Get_CartService();


            cartService.GetCart().UpdateQuantity(id, quantity);


            return RedirectToAction("ShoppingCartPage");
        }

    }
}
