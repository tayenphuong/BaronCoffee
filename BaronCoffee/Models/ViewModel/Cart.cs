using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using BaronCoffee.Models;

namespace BaronCoffee.Models
{

        public class Cart
        {
            private List<CartItem> CartProduct = new List<CartItem>();


        public IEnumerable<CartItem> CartItems => CartProduct; //Khai báo kiểu IEnumberable để có thể truy vấn hiển thị dữ liệu list item trên




        //Thêm vào giỏ hàng
        public void AddItems(int productId, string productName, string ProductImagine, decimal unitPrice, int quantity, string category, decimal productPrice)
        {
            var existingItems = CartProduct.FirstOrDefault(p => p.ProductId == productId);

            //Đã từng có sản phầm trùng với product id mà bạn thêm vào trước đó nên nó chỉ việc tăng quantity của product tương ứng id đó lên 1
            if (existingItems != null)
            {
                existingItems.Quantity += quantity;

            }

            //Còn ko thì ta tạo mới 1 cart cho product đó
            else
            {


                CartProduct.Add(new CartItem { ProductId = productId, ProductName = productName, ProductImage = ProductImagine, UnitPrice = unitPrice, Quantity = quantity, productPrice = productPrice });
            }
            
        }


        //Xóa một sản phẩm khỏi giỏ hàng
        public void RemoveProductOutCart(int productId)
        {
            CartProduct.RemoveAll(p => p.ProductId == productId);
        }


        //Tính tổng giá trị value trong giỏ hàng
        public decimal TotalValue()
        {
            return CartProduct.Sum(i => i.TotalPrice);
        }


        //Cập nhật số lượng sản phẩm trong cart
        public void UpdateQuantity(int id, int quantity)
        {
            var items = CartProduct.FirstOrDefault(p => p.ProductId == id);


            //Cập nhật lại quantity của product có id bạn cần cập nhật 
            if (items != null)
            {
                items.Quantity = quantity;
            }
        }
    }
}
    
