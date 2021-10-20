using ChanhThu_Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChanhThu_Store.Models
{
    [Serializable]
    public class CartItem
    {
        public SanPham Product { get; set; }
        public int Quantity { get; set; }
    }
}