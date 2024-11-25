using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaronCoffee.Models.ViewModel
{
    public class CategoryWithProductsVM
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public List<Product> FeaturedProducts { get; set; } // 3 sản phẩm nổi bật
    }

    public class CategoryPageVM
    {
        public List<CategoryWithProductsVM> CategoriesWithProducts { get; set; }
    }
}