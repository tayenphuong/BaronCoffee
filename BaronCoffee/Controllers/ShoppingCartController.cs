using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Configuration;
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


        [HttpPost]
        public ActionResult UpdateCartItem(int productId, int quantity)
        {
            var cartItems = Session["CartItems"] as List<CartItem>;
            if (cartItems != null)
            {
                var item = cartItems.FirstOrDefault(x => x.ProductId == productId);
                if (item != null && quantity > 0)
                {
                    item.Quantity = quantity;
                }
            }

            Session["CartItems"] = cartItems;
            return Json(new { success = true });
        }


        public ActionResult ShoppingCartPage()
        {
            var cartItems = Session["CartItems"] as List<CartItem>;
            System.Diagnostics.Debug.WriteLine("Số sản phẩm trong giỏ hàng: " + cartItems?.Count);

            // Truyền dữ liệu qua ViewBag
            ViewBag.CartItems = cartItems ?? new List<CartItem>();
            ViewBag.TotalQuantity = cartItems?.Sum(item => item.Quantity) ?? 0;
            ViewBag.TotalPrice = cartItems?.Sum(item => item.Quantity * item.UnitPrice) ?? 0;

            return View();
        }
        //Chức năng thêm vào giỏ hàng
        [HttpPost]
        public ActionResult AddToCart(int id, int quantity = 0)
        {
            var products = db.Products.SingleOrDefault(p => p.ProductID == id);

            if (products != null)
            {
                var cartService = Get_CartService();
                var cart = cartService.GetCart();

                    cart.AddItems(
                        products.ProductID,
                        products.ProductName,
                        products.ProductImage,
                        products.ProductPrice,
                        quantity,
                        products.Category.CategoryName,
                        products.ProductPrice
                    );

                // Lưu giỏ hàng vào Session
                Session["CartItems"] = cart.CartItems;

                // Kiểm tra dữ liệu giỏ hàng đã được lưu vào Session
                var cartItems = Session["CartItems"] as List<CartItem>;
                System.Diagnostics.Debug.WriteLine("Số sản phẩm trong giỏ hàng: " + (cartItems?.Count ?? 0));  // Kiểm tra số lượng sản phẩm trong giỏ hàng
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

        public ActionResult ConfirmOrder()
        {
            

            // Trả về trang ConfirmOrder
            return View();
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

        public ActionResult OrderHistory()
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

        [HttpPost]
        public ActionResult CancelOrder()
        {
            // Xóa lịch sử đơn hàng và giỏ hàng khỏi Session
            Session.Clear();

            // Điều hướng về trang "OrderHistory" sau khi hủy đơn hàng
            return RedirectToAction("OrderHistory");
        }




    }

}
