using ChanhThu_Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChanhThu_Store.Controllers
{
    public class ContactUsController : Controller
    {
        private ChanhThuStoreContext db = new ChanhThuStoreContext();
        // GET: ContactUs
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult ThongTinLienHe()
        {
            var thongTinCuaHang = db.ThongTinCuaHangs.FirstOrDefault();
            return PartialView(thongTinCuaHang);
        }

    }
}