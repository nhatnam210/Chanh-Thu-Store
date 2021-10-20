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
        public ActionResult AddItem(string productId, int quantity)
        {
            var product = new SanPhamModel().ViewDetail(productId);
            var cart = Session[CartSession];
            if(cart != null)
            {
                var list = (List<CartItem>)cart;
                if(list.Exists(x => x.Product.MaSanPham == productId))
                {
                    foreach (var item in list)
                    {
                        if (item.Product.MaSanPham == productId)
                        {
                            item.Quantity += quantity;
                        }
                        
                    }
                }
                else
                {
                    var item = new CartItem();
                    item.Product = product;
                    item.Quantity = quantity;
                    list.Add(item);
                }
                Session[CartSession] = list;
            }
            else
            {
                var item = new CartItem();
                item.Product = product;
                item.Quantity = quantity;
                var list = new List<CartItem>();
                list.Add(item);
                Session[CartSession] = list;
            }
            return new HttpStatusCodeResult(204);
        }

    }
}