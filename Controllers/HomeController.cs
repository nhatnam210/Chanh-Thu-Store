using ChanhThu_Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChanhThu_Store.Controllers
{
    public class HomeController : Controller
    {
        private const string CartSession = "CartSession";
        [ChildActionOnly]
        public PartialViewResult _Header_Cart()
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return PartialView(list);
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        //public ActionResult DoiMaVoucher()
        //{
        //    //var userID = System.Web.HttpContext.Current.User.Identity.GetUserId();
        //    //AspNetUser aspuser = db.AspNetUsers.Find(user);
        //    //if (aspuser == null)
        //    //{
        //    //    return HttpNotFound();
        //    //}
        //    var userID = User.Identity.GetUserId();
        //    Voucher listVoucherDoi = from v in db.Vouchers
        //                             join u in db.AspNetUsers on
        //                             where v.MaVoucher = "VC"
        //                             select v


        //    return PartialView("DoiMaVoucher", listVoucherDoi);
        //}
    }
}