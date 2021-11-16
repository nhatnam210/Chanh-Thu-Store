using ChanhThu_Store.Models;
using Microsoft.AspNet.Identity;
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
        [Authorize]
        public ActionResult Index(int? id)
        {
            var userID = User.Identity.GetUserId();
            IQueryable<ChiTietHoaDon> chitiet = null;
            if(userID != null)
            {
                chitiet = from c in db.ChiTietHoaDons
                          join h in db.HoaDons on c.MaHoaDon equals h.MaHoaDon
                          where c.MaHoaDon == id && h.MaKhachHang == userID
                          select c;
                return View(chitiet);
            }

            return null;
            
            
        }
    }
}