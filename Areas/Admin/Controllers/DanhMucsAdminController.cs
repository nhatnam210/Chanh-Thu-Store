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
    public class DanhMucsAdminController : Controller
    {
        private ChanhThuStoreContext db = new ChanhThuStoreContext();
        
        // GET: Admin/DanhMucsAdmin
        public ActionResult Index(string sapxep, string loc, string timkiem, int? trang)
        {
            ViewBag.Sapxep = sapxep;
            ViewBag.SapxepMa = String.IsNullOrEmpty(sapxep) ? "mã tăng dần" : "";
            ViewBag.SapxepTen = sapxep == "tên A-Z" ? "tên Z-A" : "tên A-Z";

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
            var danhmuc = from s in db.DanhMucs
                             select s;
            if (!String.IsNullOrEmpty(timkiem))
            {
                danhmuc = danhmuc.Where(s => s.TenDanhMuc.Contains(timkiem));
                //|| s.author.Contains(timkiem)

            }
            //sắp xếp 
            switch (sapxep)
            {
                case "mã tăng dần":
                    danhmuc = danhmuc.OrderByDescending(s => s.MaDanhMuc);
                    break;
                case "tên A-Z":
                    danhmuc = danhmuc.OrderBy(s => s.TenDanhMuc);
                    break;
                case "tên Z-A":
                    danhmuc = danhmuc.OrderByDescending(s => s.TenDanhMuc);
                    break;
                default:
                    danhmuc = danhmuc.OrderBy(s => s.MaDanhMuc);
                    break;
            }
            //var articles = db.Articles.Include(a => a.Cetegory);
            int pageSize = 5;
            int pageNumber = (trang ?? 1);
            return View(danhmuc.ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/DanhMucsAdmin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/DanhMucsAdmin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaDanhMuc,TenDanhMuc")] DanhMuc danhMuc)
        {
            if (ModelState.IsValid)
            {
                //Lấy chuỗi MaDanhMuc của phần tử cuối bảng
                string maDMCuoi = db.DanhMucs
                                      .OrderByDescending(d => d.MaDanhMuc)
                                      .First().MaDanhMuc;

                //Cắt lấy phần chữ số và ép kiểu
                int laySoCuoi = Convert.ToInt32(maDMCuoi.Substring(2));

                //Tăng 1 đơn vị
                int soMoi = ++laySoCuoi;
                if (soMoi <= 9)
                {
                    danhMuc.MaDanhMuc = "DM0" + soMoi.ToString();
                }
                else
                {
                    danhMuc.MaDanhMuc = "DM" + soMoi.ToString();
                }

                db.DanhMucs.Add(danhMuc);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(danhMuc);
        }

        // GET: Admin/DanhMucsAdmin/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DanhMuc danhMuc = db.DanhMucs.Find(id);
            if (danhMuc == null)
            {
                return HttpNotFound();
            }
            return View(danhMuc);
        }

        // POST: Admin/DanhMucsAdmin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaDanhMuc,TenDanhMuc")] DanhMuc danhMuc)
        {
            if (ModelState.IsValid)
            {
                db.Entry(danhMuc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(danhMuc);
        }

        // GET: Admin/DanhMucsAdmin/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DanhMuc danhMuc = db.DanhMucs.Find(id);
            if (danhMuc == null)
            {
                return HttpNotFound();
            }
            return View(danhMuc);
        }

        // POST: Admin/DanhMucsAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            var danhMucCon = db.DanhMucCons
                          .Where(d => d.MaDanhMuc == id)
                          .Select(d => d);

            foreach (var itemDMC in danhMucCon)
            {
                var sanPham = db.SanPhams
                              .Where(s => s.MaDanhMucCon == itemDMC.MaDanhMucCon)
                              .Select(s => s);

                //xóa tất cả sản phẩm thuộc danh mục con của danh mục, bao gồm bình luận, tương tác
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

                    //xóa sản phẩm
                    db.SanPhams.Remove(itemSP);
                }
                //xóa danh mục con
                db.DanhMucCons.Remove(itemDMC);
            }
            DanhMuc danhMuc = db.DanhMucs.Find(id);
            db.DanhMucs.Remove(danhMuc);
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
