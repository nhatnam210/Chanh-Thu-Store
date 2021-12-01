using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ChanhThu_Store.Models;

namespace WebApplication1.Areas.Admin.Controllers
{
    public class ThongTinCuaHangsAdminController : Controller
    {
        private ChanhThuStoreContext db = new ChanhThuStoreContext();

        // GET: Admin/ThongTinCuaHangsAdmin
        public ActionResult Index()
        {
            var listThongTin = db.ThongTinCuaHangs.ToList();
            if(listThongTin.Count() > 0)
            {
                return View(listThongTin);
            }
            return View();
        }

        // GET: Admin/ThongTinCuaHangsAdmin/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThongTinCuaHang thongTinCuaHang = db.ThongTinCuaHangs.Find(id);
            if (thongTinCuaHang == null)
            {
                return HttpNotFound();
            }
            return View(thongTinCuaHang);
        }

        // GET: Admin/ThongTinCuaHangsAdmin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/ThongTinCuaHangsAdmin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TenCuaHang,SDT,Email,DiaChi,NgayThanhLap,ThoiGianMoCua,ThoiGianDongCua,LoiGioiThieu,HinhMinhHoa")] ThongTinCuaHang thongTinCuaHang)
        {
            if (ModelState.IsValid)
            {
                db.ThongTinCuaHangs.Add(thongTinCuaHang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(thongTinCuaHang);
        }

        // GET: Admin/ThongTinCuaHangsAdmin/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThongTinCuaHang thongTinCuaHang = db.ThongTinCuaHangs.Find(id);
            if (thongTinCuaHang == null)
            {
                return HttpNotFound();
            }
            return View(thongTinCuaHang);
        }

        // POST: Admin/ThongTinCuaHangsAdmin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TenCuaHang,SDT,Email,DiaChi,NgayThanhLap,ThoiGianMoCua,ThoiGianDongCua,LoiGioiThieu,HinhMinhHoa")] ThongTinCuaHang thongTinCuaHang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(thongTinCuaHang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(thongTinCuaHang);
        }

        // GET: Admin/ThongTinCuaHangsAdmin/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThongTinCuaHang thongTinCuaHang = db.ThongTinCuaHangs.Find(id);
            if (thongTinCuaHang == null)
            {
                return HttpNotFound();
            }
            return View(thongTinCuaHang);
        }

        // POST: Admin/ThongTinCuaHangsAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ThongTinCuaHang thongTinCuaHang = db.ThongTinCuaHangs.Find(id);
            db.ThongTinCuaHangs.Remove(thongTinCuaHang);
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
