using ChanhThu_Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChanhThu_Store.Controllers
{
    public class AboutUsController : Controller
    {
        private const string CartSession = "CartSession";
        // GET: AboutUs
        public ActionResult Index()
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
            
        }
    }
}