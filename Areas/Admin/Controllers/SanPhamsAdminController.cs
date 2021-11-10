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
    [Authorize(Roles = "Admin")]
    public class SanPhamsAdminController : Controller
    {
        private ChanhThuStoreContext db = new ChanhThuStoreContext();
        
        // GET: Admin/SanPhamsAdmin
        public ActionResult Index(string sapxep, string loc, string timkiem, int? trang)
        {
            ViewBag.Sapxep = sapxep;
            ViewBag.SapxepMa = String.IsNullOrEmpty(sapxep) ? "Id" : "";
            ViewBag.SapxepTen = sapxep == "Ten" ? "Ten_desc" : "Ten";

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
            var sanpham = from s in db.SanPhams
                          select s;
            if (!String.IsNullOrEmpty(timkiem))
            {
                sanpham = sanpham.Where(s => s.TenSanPham.Contains(timkiem));
                //|| s.author.Contains(timkiem)

            }
            //sắp xếp 
            switch (sapxep)
            {
                case "Id":
                    sanpham = sanpham.OrderByDescending(s => s.MaSanPham);
                    break;
                case "Ten":
                    sanpham = sanpham.OrderBy(s => s.TenSanPham);
                    break;
                case "Ten_desc":
                    sanpham = sanpham.OrderByDescending(s => s.TenSanPham);
                    break;
                default:
                    sanpham = sanpham.OrderBy(s => s.MaSanPham);
                    break;
            }
            //var articles = db.Articles.Include(a => a.Cetegory);
            int pageSize = 5;
            int pageNumber = (trang ?? 1);
            return View(sanpham.ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/SanPhamsAdmin/Details/5
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

        // GET: Admin/SanPhamsAdmin/Create
        public ActionResult Create()
        {
            ViewBag.MaDanhMucCon = new SelectList(db.DanhMucCons, "MaDanhMucCon", "MaDanhMuc");
            ViewBag.MaNhaSanXuat = new SelectList(db.NhaSanXuats, "MaNhaSanXuat", "TenNhaSanXuat");
            return View();
        }

        // POST: Admin/SanPhamsAdmin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaSanPham,MaDanhMucCon,MaNhaSanXuat,TenSanPham,Gia,HinhChinh,Hinh1,Hinh2,Mota,SoLuongTonKho,SoLuongDaBan,LuotYeuThich,NgaySanXuat,HanSuDung,Diem,TinhTrang")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                db.SanPhams.Add(sanPham);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaDanhMucCon = new SelectList(db.DanhMucCons, "MaDanhMucCon", "MaDanhMuc", sanPham.MaDanhMucCon);
            ViewBag.MaNhaSanXuat = new SelectList(db.NhaSanXuats, "MaNhaSanXuat", "TenNhaSanXuat", sanPham.MaNhaSanXuat);
            return View(sanPham);
        }

        // GET: Admin/SanPhamsAdmin/Edit/5
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
            ViewBag.MaDanhMucCon = new SelectList(db.DanhMucCons, "MaDanhMucCon", "MaDanhMuc", sanPham.MaDanhMucCon);
            ViewBag.MaNhaSanXuat = new SelectList(db.NhaSanXuats, "MaNhaSanXuat", "TenNhaSanXuat", sanPham.MaNhaSanXuat);
            return View(sanPham);
        }

        // POST: Admin/SanPhamsAdmin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaSanPham,MaDanhMucCon,MaNhaSanXuat,TenSanPham,Gia,HinhChinh,Hinh1,Hinh2,Mota,SoLuongTonKho,SoLuongDaBan,LuotYeuThich,NgaySanXuat,HanSuDung,Diem,TinhTrang")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sanPham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaDanhMucCon = new SelectList(db.DanhMucCons, "MaDanhMucCon", "MaDanhMuc", sanPham.MaDanhMucCon);
            ViewBag.MaNhaSanXuat = new SelectList(db.NhaSanXuats, "MaNhaSanXuat", "TenNhaSanXuat", sanPham.MaNhaSanXuat);
            return View(sanPham);
        }

        // GET: Admin/SanPhamsAdmin/Delete/5
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

        // POST: Admin/SanPhamsAdmin/Delete/5
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
