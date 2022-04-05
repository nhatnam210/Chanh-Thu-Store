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
        public SanPham Sanpham { get; set; }
        public int Soluong { get; set; }
    }
}