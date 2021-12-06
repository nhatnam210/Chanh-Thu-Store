using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ChanhThu_Store.Models;
using PagedList;
using ChanhThu_Store.Controllers;


namespace ChanhThu_Store.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class HoaDonsAdminController : Controller
    {
        private ChanhThuStoreContext db = new ChanhThuStoreContext();
        
        // GET: Admin/HoaDonsAdmin
        public ActionResult Index(string sapxep, string loc, string timkiem, int? trang)
        {
            ViewBag.SapXep = sapxep;
            ViewBag.SapXepMa = String.IsNullOrEmpty(sapxep) ? "mã tăng dần" : "";
            ViewBag.SapXepTen = sapxep == "tên A-Z" ? "tên Z-A" : "tên A-Z";
            ViewBag.SapXepNgay = sapxep == "ngày tăng dần" ? "ngày giảm dần" : "ngày tăng dần";
            ViewBag.SapXepTongTien = sapxep == "tổng tiền thấp > cao" ? "tổng tiền cao > thấp" : "tổng tiền thấp > cao";
            //phan trang
            if (timkiem != null)
            {
                trang = 1;
            }
            else
            {
                timkiem = loc;
            }
            ViewBag.Loc = timkiem;
            //tìm kiếm
            var hoadon = from s in db.HoaDons
                             select s;
            if (!String.IsNullOrEmpty(timkiem))
            {
                //CultureInfo VN = new CultureInfo("vi-VN"); 
                //hoadon = hoadon.Where(s => s.MaHoaDon.ToString().Contains(timkiem)
                //|| s.Ten.Contains(timkiem)
                //|| s.SDT.Contains(timkiem)
                //|| s.Email.Contains(timkiem)
                //|| s.NgayLap.ToString().Contains(timkiem)
                //);
                ////|| s.author.Contains(timkiem)
                timkiem = timkiem.Trim();
                var timkiemUnsign = TimKiemController.ConvertToUnSignNoneSpace(timkiem);
                hoadon = hoadon.Where(delegate (HoaDon s)
                {
                    if (TimKiemController.ConvertToUnSignNoneSpace(s.MaHoaDon.ToString()).IndexOf(timkiemUnsign, StringComparison.CurrentCultureIgnoreCase) >= 0
                    || TimKiemController.ConvertToUnSignNoneSpace(s.Ten).IndexOf(timkiemUnsign, StringComparison.CurrentCultureIgnoreCase) >= 0
                    || TimKiemController.ConvertToUnSignNoneSpace(s.SDT).IndexOf(timkiemUnsign, StringComparison.CurrentCultureIgnoreCase) >= 0
                    || TimKiemController.ConvertToUnSignNoneSpace(s.Email).IndexOf(timkiemUnsign, StringComparison.CurrentCultureIgnoreCase) >= 0
                    || TimKiemController.ConvertToUnSignNoneSpace(s.NgayLap.ToString()).IndexOf(timkiemUnsign, StringComparison.CurrentCultureIgnoreCase) >= 0
                    )
                        return true;
                    else
                        return false;
                }).AsQueryable();
            }
            //sắp xếp 
            switch (sapxep)
            {
                case "mã tăng dần":
                    hoadon = hoadon.OrderBy(s => s.MaHoaDon);
                    break;
                case "tên A-Z":
                    hoadon = hoadon.OrderBy(s => s.Ten);
                    break;
                case "tên Z-A":
                    hoadon = hoadon.OrderByDescending(s => s.Ten);
                    break;
                case "ngày tăng dần":
                    hoadon = hoadon.OrderBy(s => s.NgayLap);
                    break;
                case "ngày giảm dần":
                    hoadon = hoadon.OrderByDescending(s => s.NgayLap);
                    break;
                case "tổng tiền thấp > cao":
                    hoadon = hoadon.OrderBy(s => s.TongTien);
                    break;
                case "tổng tiền cao > thấp":
                    hoadon = hoadon.OrderByDescending(s => s.TongTien);
                    break;
                default:
                    hoadon = hoadon.OrderByDescending(s => s.NgayLap).OrderByDescending(s => s.MaHoaDon);
                    break;
            }
            //var articles = db.Articles.Include(a => a.Cetegory);
            int pageSize = 5;
            int pageNumber = (trang ?? 1);
            return View(hoadon.ToPagedList(pageNumber, pageSize));
        }

        //// GET: Admin/HoaDonsAdmin/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    HoaDon hoaDon = db.HoaDons.Find(id);
        //    if (hoaDon == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(hoaDon);
        //}

        public ActionResult InHoaDon(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoaDon mahoadon = db.HoaDons.Find(id);
            if (mahoadon == null)
            {
                return HttpNotFound();
            }
            return View(mahoadon);
        }

        public ActionResult BillChiTietHoaDon(int? id)
        {
            IQueryable<ChiTietHoaDon> listChiTietHoaDon = null;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            listChiTietHoaDon = from h in db.ChiTietHoaDons
                                where h.MaHoaDon == id
                                select h;

            return PartialView("BillChiTietHoaDon", listChiTietHoaDon);
        }

        // GET: Admin/HoaDonsAdmin/Create
        //public ActionResult Create()
        //{
        //    ViewBag.MaKhachHang = new SelectList(db.AspNetUsers, "Id", "Ten");
        //    return View();
        //}

        //// POST: Admin/HoaDonsAdmin/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "MaHoaDon,MaKhachHang,NgayLap,MaVoucher,TongTien,Ten,SDT,DiaChi,Email")] HoaDon hoaDon)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.HoaDons.Add(hoaDon);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.MaKhachHang = new SelectList(db.AspNetUsers, "Id", "Ten", hoaDon.MaKhachHang);
        //    return View(hoaDon);
        //}

        //// GET: Admin/HoaDonsAdmin/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    HoaDon hoaDon = db.HoaDons.Find(id);
        //    if (hoaDon == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.MaKhachHang = new SelectList(db.AspNetUsers, "Id", "Ten", hoaDon.MaKhachHang);
        //    return View(hoaDon);
        //}

        //// POST: Admin/HoaDonsAdmin/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "MaHoaDon,MaKhachHang,NgayLap,MaVoucher,TongTien,Ten,SDT,DiaChi,Email")] HoaDon hoaDon)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(hoaDon).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.MaKhachHang = new SelectList(db.AspNetUsers, "Id", "Ten", hoaDon.MaKhachHang);
        //    return View(hoaDon);
        //}

        // GET: Admin/HoaDonsAdmin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoaDon hoaDon = db.HoaDons.Find(id);
            if (hoaDon == null)
            {
                return HttpNotFound();
            }
            return View(hoaDon);
        }

        // POST: Admin/HoaDonsAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //tìm những item cùng MaHoaDon trong ChiTietHoaDon để xóa hết
            db.ChiTietHoaDons.RemoveRange(db.ChiTietHoaDons
                                        .Where(c => c.MaHoaDon == id)
                                        .Select(c => c));

            HoaDon hoaDon = db.HoaDons.Find(id);
            db.HoaDons.Remove(hoaDon);
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
