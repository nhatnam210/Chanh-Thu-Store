using ChanhThu_Store.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChanhThu_Store.Controllers
{
    public class ChitietHoaDonController : Controller
    {
        private readonly ChanhThuStoreContext db = new ChanhThuStoreContext();
        // GET: ChitietHoaDon
        [Authorize]
        public ActionResult Index(int? id)
        {
            var userID = User.Identity.GetUserId();
            IQueryable<ChiTietHoaDon> chitiet = null;
            var diemTong = 0;
            if(userID != null)
            {
                chitiet = from c in db.ChiTietHoaDons
                          join h in db.HoaDons on c.MaHoaDon equals h.MaHoaDon
                          where c.MaHoaDon == id && h.MaKhachHang == userID
                          select c;
                foreach(var item in chitiet)
                {
                    diemTong += item.Soluong * item.SanPham.Diem;
                }

                ViewBag.DiemTong = diemTong;
                return View(chitiet);
            }

            return null;
            
            
        }
    }
}