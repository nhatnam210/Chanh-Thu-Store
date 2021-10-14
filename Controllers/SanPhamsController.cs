using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ChanhThu_Store.Models;

namespace WebApplication1.Controllers
{
    public class SanPhamsController : Controller
    {
        private ChanhThuStoreContext db = new ChanhThuStoreContext();

        // GET: SanPhams
        public ActionResult Index()
        {
            
            return View();
        }

        // GET: SanPhams/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        
        // GET: SanPhams/Create
        public ActionResult Create()
        {
            ViewBag.MaDanhMuc = new SelectList(db.DanhMucs, "MaDanhMuc", "TenDanhMuc");
            ViewBag.MaDanhMucCon = new SelectList(db.DanhMucCons, "MaDanhMucCon", "MaDanhMuc");
            ViewBag.MaNhaSanXuat = new SelectList(db.NhaSanXuats, "MaNhaSanXuat", "TenNhaSanXuat");
            return View();
        }

        // POST: SanPhams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaSanPham,MaDanhMuc,MaDanhMucCon,MaNhaSanXuat,TenSanPham,Gia,HinhChinh,Hinh1,Hinh2,Mota,SoLuongTonKho,SoLuongDaBan,LuotYeuThich,NgaySanXuat,HanSuDung,Diem,TinhTrang")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                db.SanPhams.Add(sanPham);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaDanhMuc = new SelectList(db.DanhMucs, "MaDanhMuc", "TenDanhMuc", sanPham.MaDanhMuc);
            ViewBag.MaDanhMucCon = new SelectList(db.DanhMucCons, "MaDanhMucCon", "MaDanhMuc", sanPham.MaDanhMucCon);
            ViewBag.MaNhaSanXuat = new SelectList(db.NhaSanXuats, "MaNhaSanXuat", "TenNhaSanXuat", sanPham.MaNhaSanXuat);
            return View(sanPham);
        }

        // GET: SanPhams/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaDanhMuc = new SelectList(db.DanhMucs, "MaDanhMuc", "TenDanhMuc", sanPham.MaDanhMuc);
            ViewBag.MaDanhMucCon = new SelectList(db.DanhMucCons, "MaDanhMucCon", "MaDanhMuc", sanPham.MaDanhMucCon);
            ViewBag.MaNhaSanXuat = new SelectList(db.NhaSanXuats, "MaNhaSanXuat", "TenNhaSanXuat", sanPham.MaNhaSanXuat);
            return View(sanPham);
        }

        // POST: SanPhams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaSanPham,MaDanhMuc,MaDanhMucCon,MaNhaSanXuat,TenSanPham,Gia,HinhChinh,Hinh1,Hinh2,Mota,SoLuongTonKho,SoLuongDaBan,LuotYeuThich,NgaySanXuat,HanSuDung,Diem,TinhTrang")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sanPham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaDanhMuc = new SelectList(db.DanhMucs, "MaDanhMuc", "TenDanhMuc", sanPham.MaDanhMuc);
            ViewBag.MaDanhMucCon = new SelectList(db.DanhMucCons, "MaDanhMucCon", "MaDanhMuc", sanPham.MaDanhMucCon);
            ViewBag.MaNhaSanXuat = new SelectList(db.NhaSanXuats, "MaNhaSanXuat", "TenNhaSanXuat", sanPham.MaNhaSanXuat);
            return View(sanPham);
        }

        // GET: SanPhams/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // POST: SanPhams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            SanPham sanPham = db.SanPhams.Find(id);
            db.SanPhams.Remove(sanPham);
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
