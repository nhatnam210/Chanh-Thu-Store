using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ChanhThu_Store.Models;
using PagedList;

namespace ChanhThu_Store.Areas.Admin.Controllers
{
    public class ChiTietHoaDonsAdminController : Controller
    {
        private ChanhThuStoreContext db = new ChanhThuStoreContext();

        // GET: Admin/ChiTietHoaDonsAdmin
        public ActionResult Index(int? id)
        {
            //IQueryable<ChiTietHoaDon> chitiet = null;
            //ViewBag.SapXep = sapxep;
            //ViewBag.SapXepMa = String.IsNullOrEmpty(sapxep) ? "Id" : "";
            //ViewBag.SapXepTen = sapxep == "Ten" ? "Ten_desc" : "Ten";
            //ViewBag.SapXepNgay = sapxep == "Ngay" ? "Ngay_desc" : "Ngay";
            ////phan trang
            //if (timkiem != null)
            //{
            //    trang = 1;
            //}
            //else
            //{
            //    timkiem = loc;
            //}
            //ViewBag.Loc = timkiem;
            ////tìm kiếm
            //var chitiethoadon = from s in db.ChiTietHoaDons
            //             select s;
            //if (!String.IsNullOrEmpty(timkiem))
            //{
            //    chitiethoadon = chitiethoadon.Where(s => (s.MaHoaDon).ToString().Contains(timkiem));
            //    //|| s.author.Contains(timkiem)

            //}
            ////sắp xếp 
            //switch (sapxep)
            //{
            //    case "Id":
            //        chitiethoadon = chitiethoadon.OrderByDescending(s => s.MaHoaDon);
            //        break;
            //    case "Id desc":
            //        chitiethoadon = chitiethoadon.OrderByDescending(s => s.MaHoaDon);
            //        break;

            //    default:
            //        chitiethoadon = chitiethoadon.OrderBy(s => s.MaHoaDon);
            //        break;
            //}
            ////var articles = db.Articles.Include(a => a.Cetegory);
            //int pageSize = 5;
            //int pageNumber = (trang ?? 1);
            var chitiet = from c in db.ChiTietHoaDons
                         where c.MaHoaDon == id
                         select c;
            return View(chitiet.ToList());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
