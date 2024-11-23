using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;

namespace BaronCoffee.Models.ViewModel
{
    public class HomeProduct__2VM
    {
        public string SearchTerm { get; set; }



        public PagedList.IPagedList<Product> Products { get; set; }

        public List<Product> FeaturedProducts { get; set; }

        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }

        public string SortOder { get; set; }

        public List<Category> Categories { get; set; }
        public int? CategoryId { get; set; }
    }
}