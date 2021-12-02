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
    public class DanhMucConsAdminController : Controller
    {
        private ChanhThuStoreContext db = new ChanhThuStoreContext();

        // GET: Admin/DanhMucConsAdmin
        public ActionResult Index(string sapxep, string loc, string timkiem, int? trang)
        {
            ViewBag.SapXep = sapxep;
            ViewBag.SapXepMa = String.IsNullOrEmpty(sapxep) ? "ma-loai-giam-dan" : "";
            ViewBag.SapXepTen = sapxep == "ten-loai-tang-dan" ? "ten-loai-giam-dan" : "ten-loai-tang-dan";
            ViewBag.SapXepTenDanhMuc = sapxep == "ten-danh-muc-tang-dan" ? "ten-danh-muc-giam-dan" : "ten-danh-muc-tang-dan";

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
            var danhmuccon = from s in db.DanhMucCons
                             select s;
            if (danhmuccon.Count() > 0)
            {
                if (!String.IsNullOrEmpty(timkiem))
                {
                    danhmuccon = danhmuccon.Where(s => s.TenDanhMucCon.Contains(timkiem)
                    || s.DanhMuc.TenDanhMuc.Contains(timkiem)
                    );
                    //|| s.author.Contains(timkiem)

                }
                //sắp xếp 
                switch (sapxep)
                {
                    case "ma-loai-giam-dan":
                        danhmuccon = danhmuccon.OrderByDescending(s => s.MaDanhMucCon);
                        break;
                    case "ten-loai-tang-dan":
                        danhmuccon = danhmuccon.OrderBy(s => s.TenDanhMucCon);
                        break;
                    case "ten-loai-giam-dan":
                        danhmuccon = danhmuccon.OrderByDescending(s => s.TenDanhMucCon);
                        break;
                    case "ten-danh-muc-tang-dan":
                        danhmuccon = danhmuccon.OrderBy(s => s.DanhMuc.TenDanhMuc);
                        break;
                    case "ten-danh-muc-giam-dan":
                        danhmuccon = danhmuccon.OrderByDescending(s => s.DanhMuc.TenDanhMuc);
                        break;
                    default:
                        danhmuccon = danhmuccon.OrderByDescending(s => s.MaDanhMucCon);
                        break;
                }

                int pageSize = 5;
                int pageNumber = (trang ?? 1);
                return View(danhmuccon.ToPagedList(pageNumber, pageSize));
            }
            return View();
        }

        // GET: Admin/DanhMucConsAdmin/Create
        public ActionResult Create()
        {
            ViewBag.MaDanhMuc = new SelectList(db.DanhMucs, "MaDanhMuc", "TenDanhMuc");
            return View();
        }

        // POST: Admin/DanhMucConsAdmin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaDanhMucCon,MaDanhMuc,TenDanhMucCon,Hinh")] DanhMucCon danhMucCon)
        {
            if (ModelState.IsValid)
            {
                //Lấy chuỗi MaDanhMucCon của phần tử cuối bảng
                string maDMCCuoi = db.DanhMucCons
                                      .OrderByDescending(d => d.MaDanhMucCon)
                                      .First().MaDanhMucCon;

                //Cắt lấy phần chữ số và ép kiểu
                int laySoCuoi = Convert.ToInt32(maDMCCuoi.Substring(3));

                //Tăng 1 đơn vị
                int soMoi = ++laySoCuoi;
                if (soMoi <= 9)
                {
                    danhMucCon.MaDanhMucCon = "LSP0" + soMoi.ToString();
                }
                else
                {
                    danhMucCon.MaDanhMucCon = "LSP" + soMoi.ToString();
                }

                db.DanhMucCons.Add(danhMucCon);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaDanhMuc = new SelectList(db.DanhMucs, "MaDanhMuc", "TenDanhMuc", danhMucCon.MaDanhMuc);
            return View(danhMucCon);
        }

        // GET: Admin/DanhMucConsAdmin/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DanhMucCon danhMucCon = db.DanhMucCons.Find(id);
            if (danhMucCon == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaDanhMuc = new SelectList(db.DanhMucs, "MaDanhMuc", "TenDanhMuc", danhMucCon.MaDanhMuc);
            return View(danhMucCon);
        }

        // POST: Admin/DanhMucConsAdmin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaDanhMucCon,MaDanhMuc,TenDanhMucCon,Hinh")] DanhMucCon danhMucCon)
        {
            if (ModelState.IsValid)
            {
                db.Entry(danhMucCon).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaDanhMuc = new SelectList(db.DanhMucs, "MaDanhMuc", "TenDanhMuc", danhMucCon.MaDanhMuc);
            return View(danhMucCon);
        }

        // GET: Admin/DanhMucConsAdmin/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DanhMucCon danhMucCon = db.DanhMucCons.Find(id);
            if (danhMucCon == null)
            {
                return HttpNotFound();
            }
            return View(danhMucCon);
        }

        // POST: Admin/DanhMucConsAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            var sanPham = db.SanPhams
                          .Where(s => s.MaDanhMucCon == id)
                          .Select(s => s);

            //xóa tất cả sản phẩm thuộc danh mục con, bao gồm bình luận, tương tác
            foreach (var itemSP in sanPham)
            {
                //Xóa bình luận
                var binhLuan = db.BinhLuans
                                .Where(b => b.MaSanPham == itemSP.MaSanPham)
                                .Select(b => b);
                foreach (var itemBL in binhLuan)
                {
                    db.BinhLuans.Remove(itemBL);
                }
                //Xóa Yêu thích
                var yeuThich = db.TuongTacs
                               .Where(t => t.MaSanPham == itemSP.MaSanPham)
                               .Select(t => t);

                foreach (var itemYT in yeuThich)
                {
                    db.TuongTacs.Remove(itemYT);
                }

                db.SanPhams.Remove(itemSP);
            }

            DanhMucCon danhMucCon = db.DanhMucCons.Find(id);
            db.DanhMucCons.Remove(danhMucCon);
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
