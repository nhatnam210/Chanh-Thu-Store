using ChanhThu_Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChanhThu_Store.Controllers
{
    public class ShoppingCartController : Controller
    {
        private ChanhThuStoreContext db = new ChanhThuStoreContext();
        // GET: ShoppingCart
        public ActionResult AddtoCart(string id)
        {
            var pro = db.SanPhams.SingleOrDefault(s => s.MaSanPham == id );
            if(pro != null)
            {
                GetCart().Add(pro);
            }

            return new HttpStatusCodeResult(204);
        }
        public ActionResult ShowToCart()
        {
            if (Session["Cart"] == null)
                return RedirectToAction("ShowToCart", "ShoppingCart");
            Cart cart = Session["Cart"] as Cart;
            return View(cart);
        }
        public Cart GetCart()
        {
            Cart cart = Session["Cart"] as Cart;
            if(cart == null || Session["Cart"] == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;

            }
            return cart;
        }
        public ActionResult UpdateQuantityCart(FormCollection form)
        {
            Cart cart = Session["Cart"] as Cart;
            string id_Pro = form["id_Product"];
            int quantity = int.Parse(form["quantity"]);
            cart.Update_Quantity(id_Pro, quantity);
            return RedirectToAction("ShowToCart", "ShoppingCart");
        }
    }
}