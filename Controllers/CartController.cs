using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChanhThu_Store.Models;
using ChanhThu_Store.Models.DAO;

namespace ChanhThu_Store.Controllers
{
    public class CartController : Controller
    {
        private const string CartSession = "CartSession";
        
        // GET: Cart
        public ActionResult Index()
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if(cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }
        public ActionResult AddItem(string masanpham, int soluong)
        {
            var sanpham = new SanPhamModel().ViewDetail(masanpham);
            var cart = Session[CartSession];
            if(cart != null)
            {
                var list = (List<CartItem>)cart;
                if(list.Exists(x => x.Sanpham.MaSanPham == masanpham))
                {
                    foreach (var item in list)
                    {
                        if (item.Sanpham.MaSanPham == masanpham)
                        {
                            item.Soluong += soluong;
                        }
                        
                    }
                }
                else
                {
                    var item = new CartItem();
                    item.Sanpham = sanpham;
                    item.Soluong = soluong;
                    list.Add(item);
                }
                Session[CartSession] = list;
            }
            else
            {
                var item = new CartItem();
                item.Sanpham = sanpham;
                item.Soluong = soluong;
                var list = new List<CartItem>();
                list.Add(item);
                Session[CartSession] = list;
            }
            return new HttpStatusCodeResult(204);
        }

    }
}