using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTL.Models.Entity
{
    public class Cart
    {
        public List<CartItem> CartItems { get; set; }

        public Cart() { 
            this.CartItems = new List<CartItem>();
        }

        public void AddToCart(CartItem item, int quantity, string sizeName) { 
            var checkExits = CartItems.FirstOrDefault(x => x.ProductId == item.ProductId);
            if (checkExits != null)
            {
                checkExits.ProductSize = sizeName;
                checkExits.Quantity += quantity;
                checkExits.TotalPrice = checkExits.Price * checkExits.Quantity;
            }
            else
            {
                CartItems.Add(item);    
            }
        }

        public bool Remove(int id)
        {
            var checkExits = CartItems.SingleOrDefault(x => x.ProductId == id);
            if(checkExits != null)
            {
                CartItems.Remove(checkExits);
                return true;
            }
            return false;
        }

        public void UpdateQuantity(int id, int quantity)
        {
            var checkExits = CartItems.Single(x => x.ProductId == id);
            if(checkExits != null)
            {
                checkExits.Quantity += quantity;
                checkExits.TotalPrice = checkExits.Price * quantity;
            }
        }

        public decimal GetTotalPrice()
        {
            return CartItems.Sum(x => x.TotalPrice);
        }

        public int GetTotalQuantity()
        {
            return CartItems.Sum(x => x.Quantity);
        }

        public void ClearCart()
        {
            CartItems.Clear();
        }
    }
}