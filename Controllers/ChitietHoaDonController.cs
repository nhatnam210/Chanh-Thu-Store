using ChanhThu_Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class ChitietHoaDonController : Controller
    {
        private ChanhThuStoreContext db = new ChanhThuStoreContext();
        // GET: ChitietHoaDon
        public ActionResult Index(int? id)
        {
            IQueryable<ChiTietHoaDon> chitiet = null;
            chitiet = from c in db.ChiTietHoaDons
                      where c.MaHoaDon == id
                      select c;
            return View(chitiet);
        }
    }
}