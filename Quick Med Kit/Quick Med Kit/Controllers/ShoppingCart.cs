using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quick_Med_Kit.Controllers
{
    public class ShoppingCart
    {
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public decimal Prix { get; set; }
        public decimal Total { get; set; }
        public string ImagePath { get; set; }
        public string Description { get; set; }
    }
}