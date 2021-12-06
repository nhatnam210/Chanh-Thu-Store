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
using ChanhThu_Store.Controllers;

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
            ViewBag.SapXepMa = String.IsNullOrEmpty(sapxep) ? "mã tăng dần" : "";
            ViewBag.SapXepTen = sapxep == "tên A-Z" ? "tên Z-A" : "tên A-Z";
            ViewBag.SapXepTenDanhMuc = sapxep == "danh mục A-Z" ? "danh mục Z-A" : "danh mục A-Z";

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
                    timkiem = timkiem.Trim();
                    var timkiemUnsign = TimKiemController.ConvertToUnSignNoneSpace(timkiem);
                    danhmuccon = danhmuccon.Where(delegate (DanhMucCon s)
                    {
                        if (TimKiemController.ConvertToUnSignNoneSpace(s.TenDanhMucCon).IndexOf(timkiemUnsign, StringComparison.CurrentCultureIgnoreCase) >= 0
                        || TimKiemController.ConvertToUnSignNoneSpace(s.DanhMuc.TenDanhMuc).IndexOf(timkiemUnsign, StringComparison.CurrentCultureIgnoreCase) >= 0)
                            return true;
                        else
                            return false;
                    }).AsQueryable();
                }
                //sắp xếp 
                switch (sapxep)
                {
                    case "mã tăng dần":
                        danhmuccon = danhmuccon.OrderByDescending(s => s.MaDanhMucCon);
                        break;
                    case "tên A-Z":
                        danhmuccon = danhmuccon.OrderBy(s => s.TenDanhMucCon);
                        break;
                    case "tên Z-A":
                        danhmuccon = danhmuccon.OrderByDescending(s => s.TenDanhMucCon);
                        break;
                    case "danh mục A-Z":
                        danhmuccon = danhmuccon.OrderBy(s => s.DanhMuc.TenDanhMuc);
                        break;
                    case "danh mục Z-A":
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
                db.BinhLuans.RemoveRange(binhLuan);

                //Xóa yêu thích
                var yeuThich = db.TuongTacs
                               .Where(t => t.MaSanPham == itemSP.MaSanPham)
                               .Select(t => t);
                db.TuongTacs.RemoveRange(yeuThich);

                //Xóa CTHD và HD
                var chiTietHoaDon = db.ChiTietHoaDons
                               .Where(c => c.MaSanPham == itemSP.MaSanPham)
                               .Select(c => c);
                foreach (var itemCTHD in chiTietHoaDon)
                {
                    //tìm những item khác có cùng MaHoaDon trong ChiTietHoaDon để xóa hết
                    db.ChiTietHoaDons.RemoveRange(db.ChiTietHoaDons
                                                .Where(c2 => c2.MaHoaDon == itemCTHD.MaHoaDon)
                                                .Select(c2 => c2));

                    //xóa luôn hóa đơn có tương ứng
                    db.HoaDons.Remove(db.HoaDons.SingleOrDefault(hd => hd.MaHoaDon == itemCTHD.MaHoaDon));
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
