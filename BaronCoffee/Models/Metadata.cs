using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaronCoffee.Models
{
    public class Metadata
    {
        public partial class Category
        {
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
            public Category()
            {
                this.Products = new HashSet<Product>();
            }

            public int CategoryID { get; set; }
            public string CategoryName { get; set; }

            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
            public virtual ICollection<Product> Products { get; set; }
        }
        public partial class Customer
        {
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
            public Customer()
            {
                this.Orders = new HashSet<Order>();
            }

            public int CustomerID { get; set; }
            public string CustomerName { get; set; }
            public string CustomerPhone { get; set; }
            public string CustomerEmail { get; set; }
            public string CustomerAddress { get; set; }
            public string Username { get; set; }

            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
            public virtual ICollection<Order> Orders { get; set; }
            public virtual User User { get; set; }
        }
        public partial class Order
        {
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
            public Order()
            {
                this.OrderDetails = new HashSet<OrderDetail>();
            }

            public int OrderID { get; set; }
            public int CustomerID { get; set; }
            public System.DateTime OrderDate { get; set; }
            public decimal TotalAmount { get; set; }
            public string PaymentStatus { get; set; }
            public string AddressDelivery { get; set; }

            public virtual Customer Customer { get; set; }
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
            public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        }
        public partial class Product
        {
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
            public Product()
            {
                this.OrderDetails = new HashSet<OrderDetail>();
            }

            public int ProductID { get; set; }
            public int CategoryID { get; set; }
            public string ProductName { get; set; }
            public string ProductDescription { get; set; }
            public decimal ProductPrice { get; set; }
            public string ProductImage { get; set; }

            public virtual Category Category { get; set; }
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
            public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        }
        public partial class User
        {
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
            public User()
            {
                this.Customers = new HashSet<Customer>();
            }

            public string Username { get; set; }
            public string Password { get; set; }
            public string UserRole { get; set; }

            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
            public virtual ICollection<Customer> Customers { get; set; }
        }
    }
}

