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
        public ActionResult AddToCart(int id, int quantity = 1)
        {
            var products = db.Products.Find(id);

            if (products != null)
            {
                var cartService = Get_CartService();
                var cart = cartService.GetCart();
                cart.AddItems(products.ProductID, products.ProductName, products.ProductImage, products.ProductPrice, quantity, products.Category.CategoryName, products.ProductPrice);

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
            // Có thể ở đây bạn xử lý thêm nếu cần (ví dụ: lưu thông tin đơn hàng, gửi email, v.v.)

            // Trả về view ConfirmOrder.cshtml
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

        // Action xử lý khi xác nhận đơn hàng (Confirm Order)
        public ActionResult OrderHistory()
        {
            // Giả sử bạn đã lấy giỏ hàng và thông tin thanh toán
            var cart = Session["CartItems"] as Cart;
            var user = Session["User"] as User;

            if (cart != null && user != null)
            {
                // Lưu thông tin đơn hàng vào "orderHistory"
                var orderHistory = new OrderHistory
                {
                    OrderID = new Random().Next(1000, 9999),  // Tạo ID đơn hàng ngẫu nhiên
                    UserID = user.UserID,
                    ProductID = cart.CartItems.FirstOrDefault().ProductID,  // Lấy sản phẩm đầu tiên trong giỏ hàng
                    Quantity = cart.CartItems.Sum(item => item.Quantity),
                    Status = "Completed",  // Đơn hàng hoàn thành
                    TotalAmount = cart.CartItems.Sum(item => item.Quantity * item.UnitPrice),
                    OrderDate = DateTime.Now
                };

                // Lưu vào Session với tên "OrderHistories"
                var orderHistories = Session["OrderHistories"] as List<OrderHistory> ?? new List<OrderHistory>();
                orderHistories.Add(orderHistory);
                Session["OrderHistories"] = orderHistories;

                // Sau khi lưu, chuyển đến trang xác nhận
                return View(orderHistory);  // Trả về view xác nhận đơn hàng
            }

            return RedirectToAction("Index", "Home");  // Điều hướng nếu không có giỏ hàng
        }

        // Action hiển thị lịch sử đơn hàng
        public ActionResult OrderHistoryPage()
        {
            // Lấy danh sách lịch sử đơn hàng từ Session
            var orderHistories = Session["OrderHistories"] as List<OrderHistory>;

            // Nếu không có dữ liệu, khởi tạo danh sách trống
            if (orderHistories == null)
            {
                orderHistories = new List<OrderHistory>();
            }

            // Trả về view với danh sách các đơn hàng
            return View(orderHistories);
        }


    }
}
