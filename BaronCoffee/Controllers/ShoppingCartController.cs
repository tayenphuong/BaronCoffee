using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BaronCoffee.Models;
using BaronCoffee.Models.ViewModel;
using PagedList.Mvc;
namespace BaronCoffee.Controllers
{
    public class ShoppingCartController : Controller
    {
        private MyStoreEntities db = new MyStoreEntities();


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

        public ActionResult Checkout()
        {
            // Lấy giỏ hàng từ CartService
            var cartService = Get_CartService();
            var cartItems = cartService.GetCart().CartItems.ToList();

            // Nếu không có giỏ hàng, tạo giỏ hàng rỗng
            if (cartItems == null || !cartItems.Any())
            {
                cartItems = new List<CartItem>();
            }

            // Tính tổng tiền và số lượng sản phẩm
            ViewBag.CartItems = cartItems;
            ViewBag.TotalPrice = cartItems.Sum(item => item.Quantity * item.UnitPrice);
            ViewBag.TotalQuantity = cartItems.Sum(item => item.Quantity);

            var CheckoutVM = new CheckoutVM
            {
                CartItems = cartItems

            };


            return View(CheckoutVM);

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
