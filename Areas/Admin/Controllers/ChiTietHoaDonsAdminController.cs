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
            IQueryable<ChiTietHoaDon> chitiet = null;
            //ViewBag.Sapxep = sapxep;
            //ViewBag.SapxepMa = String.IsNullOrEmpty(sapxep) ? "Id" : "";
            //ViewBag.SapxepTen = sapxep == "Ten" ? "Ten_desc" : "Ten";
            //ViewBag.SapxepNgay = sapxep == "Ngay" ? "Ngay_desc" : "Ngay";
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
            chitiet = from c in db.ChiTietHoaDons
                         where c.MaHoaDon == id
                         select c;
            return View(chitiet);
        }

        // GET: Admin/ChiTietHoaDonsAdmin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietHoaDon chiTietHoaDon = db.ChiTietHoaDons.Find(id);
            if (chiTietHoaDon == null)
            {
                return HttpNotFound();
            }
            return View(chiTietHoaDon);
        }

        // GET: Admin/ChiTietHoaDonsAdmin/Create
        public ActionResult Create()
        {
            ViewBag.MaHoaDon = new SelectList(db.HoaDons, "MaHoaDon", "MaKhachHang");
            ViewBag.MaSanPham = new SelectList(db.SanPhams, "MaSanPham", "MaDanhMucCon");
            return View();
        }

        // POST: Admin/ChiTietHoaDonsAdmin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaHoaDon,MaSanPham,Soluong,DonGia")] ChiTietHoaDon chiTietHoaDon)
        {
            if (ModelState.IsValid)
            {
                db.ChiTietHoaDons.Add(chiTietHoaDon);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaHoaDon = new SelectList(db.HoaDons, "MaHoaDon", "MaKhachHang", chiTietHoaDon.MaHoaDon);
            ViewBag.MaSanPham = new SelectList(db.SanPhams, "MaSanPham", "MaDanhMucCon", chiTietHoaDon.MaSanPham);
            return View(chiTietHoaDon);
        }

        // GET: Admin/ChiTietHoaDonsAdmin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietHoaDon chiTietHoaDon = db.ChiTietHoaDons.Find(id);
            if (chiTietHoaDon == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaHoaDon = new SelectList(db.HoaDons, "MaHoaDon", "MaKhachHang", chiTietHoaDon.MaHoaDon);
            ViewBag.MaSanPham = new SelectList(db.SanPhams, "MaSanPham", "MaDanhMucCon", chiTietHoaDon.MaSanPham);
            return View(chiTietHoaDon);
        }

        // POST: Admin/ChiTietHoaDonsAdmin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaHoaDon,MaSanPham,Soluong,DonGia")] ChiTietHoaDon chiTietHoaDon)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chiTietHoaDon).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaHoaDon = new SelectList(db.HoaDons, "MaHoaDon", "MaKhachHang", chiTietHoaDon.MaHoaDon);
            ViewBag.MaSanPham = new SelectList(db.SanPhams, "MaSanPham", "MaDanhMucCon", chiTietHoaDon.MaSanPham);
            return View(chiTietHoaDon);
        }

        // GET: Admin/ChiTietHoaDonsAdmin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietHoaDon chiTietHoaDon = db.ChiTietHoaDons.Find(id);
            if (chiTietHoaDon == null)
            {
                return HttpNotFound();
            }
            return View(chiTietHoaDon);
        }

        // POST: Admin/ChiTietHoaDonsAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ChiTietHoaDon chiTietHoaDon = db.ChiTietHoaDons.Find(id);
            db.ChiTietHoaDons.Remove(chiTietHoaDon);
            db.SaveChanges();
            return RedirectToAction("Index");
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
