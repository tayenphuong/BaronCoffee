using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BaronCoffee.Models;
using PagedList.Mvc;

namespace BaronCoffee.Models.ViewModel
{
    public class ProductSearchVM
    {
        public string SearchTerm { get; set; }

        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }

        public string SortOder { get; set; }

        //public List<Product> Products { get; set; } // clip 12
        // clip 13 sử dụng thư viện Package PagedList.Mvc

        public int PageNumber { get; set; }
        public int PageSize { get; set; } = 10;// số sản phẩm mỗi trang

        public PagedList.IPagedList<Product> Products { get; set; }// khi sử dụng hỗ trợ phân trang, dòng này thay cho dòng 16


    }
}