using ChanhThu_Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChanhThu_Store.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class MainAdminController : Controller
    {
        private ChanhThuStoreContext db = new ChanhThuStoreContext();
        // GET: Admin/MainAdmin
        public ActionResult Index()
        {
            return View(db.ThongTinCuaHangs.FirstOrDefault());
        }

        public PartialViewResult _Menu_Admin()
        {
            return PartialView(db.ThongTinCuaHangs.FirstOrDefault());
        }
    }
}
