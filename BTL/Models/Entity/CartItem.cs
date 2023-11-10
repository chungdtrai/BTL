using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTL.Models.Entity
{
    public class CartItem
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public string ProductImange { get; set; }
        public int Quantity { get; set; }
        public string ProductSize { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
    }
}