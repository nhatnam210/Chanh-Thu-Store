using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ChanhThu_Store.Models;

namespace ChanhThu_Store.Areas.Admin.Controllers
{
    public class ThongTinCuaHangsAdminController : Controller
    {
        private ChanhThuStoreContext db = new ChanhThuStoreContext();

        // GET: Admin/ThongTinCuaHangsAdmin
        public ActionResult Index()
        {
            var thongtincuahang = db.ThongTinCuaHangs.ToList();
            if(thongtincuahang.Count() <=0)
            {
                return View();
            }
            return View(db.ThongTinCuaHangs.ToList());
        }

        // GET: Admin/ThongTinCuaHangsAdmin/Details/5
        public ActionResult Details(int id)
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

        // GET: Admin/ThongTinCuaHangsAdmin/Edit/5
        public ActionResult Edit(int id)
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
        public ActionResult Edit([Bind(Include = "Id,TenCuaHang,SDT,Email,DiaChi,NgayThanhLap,ThoiGianMoCua,ThoiGianDongCua,LoiGioiThieu,HinhMinhHoa")] ThongTinCuaHang thongTinCuaHang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(thongTinCuaHang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(thongTinCuaHang);
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
