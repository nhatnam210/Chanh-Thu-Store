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
    public class ThongTinCuaHangsController : Controller
    {
        private ChanhThuStoreContext db = new ChanhThuStoreContext();

        // GET: Admin/ThongTinCuaHangs
        public ActionResult Index()
        {
            return View(db.ThongTinCuaHangs.ToList());
        }
       

        // GET: Admin/ThongTinCuaHangs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/ThongTinCuaHangs/Create
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

        // GET: Admin/ThongTinCuaHangs/Edit/5
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

        // POST: Admin/ThongTinCuaHangs/Edit/5
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
