using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChanhThu_Store.Models
{
    public class CartItem
    {
        public SanPham _ShoppingProduct { get; set; }
        public int _ShoppingQuantity { get; set; }
    }
    public class Cart
    {   
        List<CartItem> items = new List<CartItem>();
        public IEnumerable<CartItem> Items { get { return items; } }

        public void Update_Quantity (string id , int _Quantity)
        {
            var item = items.Find(s => s._ShoppingProduct.MaSanPham == id);
            if(item != null)
            {
                item._ShoppingQuantity = _Quantity;
            }
        }
        public void Add(SanPham _Product,int _Quantity = 1) {
            var item = items.FirstOrDefault(s => s._ShoppingProduct.MaSanPham == _Product.MaSanPham);
            if(item == null)
            {
                items.Add(new CartItem
                {
                    _ShoppingProduct = _Product,
                    _ShoppingQuantity = _Quantity
                });
            }
            else
            {
                item._ShoppingQuantity += _Quantity;
            }
        }
    }
}