using ChanhThu_Store.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace ChanhThu_Store.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class NhaSanXuatsController : Controller
    {
        private readonly ChanhThuStoreContext db = new ChanhThuStoreContext();
        
        // GET: Admin/NhaSanXuats
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
            var nhasanxuat = from s in db.NhaSanXuats
                           select s;
            if (!String.IsNullOrEmpty(timkiem))
            {
                nhasanxuat = nhasanxuat.Where(s => s.TenNhaSanXuat.Contains(timkiem));
                //|| s.author.Contains(timkiem)

            }
            //sắp xếp 
            switch (sapxep)
            {
                case "mã tăng dần":
                    nhasanxuat = nhasanxuat.OrderByDescending(s => s.MaNhaSanXuat);
                    break;
                case "tên A-Z":
                    nhasanxuat = nhasanxuat.OrderBy(s => s.TenNhaSanXuat);
                    break;
                case "tên Z-A":
                    nhasanxuat = nhasanxuat.OrderByDescending(s => s.TenNhaSanXuat);
                    break;
                default:
                    nhasanxuat = nhasanxuat.OrderBy(s => s.MaNhaSanXuat);
                    break;
            }
            //var articles = db.Articles.Include(a => a.Cetegory);
            int pageSize = 5;
            int pageNumber = (trang ?? 1);
            return View(nhasanxuat.ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/NhaSanXuats/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/NhaSanXuats/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaNhaSanXuat,TenNhaSanXuat")] NhaSanXuat nhaSanXuat)
        {
            if (ModelState.IsValid)
            {
                //Lấy chuỗi MaNhaSanXuat của phần tử cuối bảng
                string maNSXCuoi = db.NhaSanXuats
                                      .OrderByDescending(d => d.MaNhaSanXuat)
                                      .First().MaNhaSanXuat;

                //Cắt lấy phần chữ số và ép kiểu
                int laySoCuoi = Convert.ToInt32(maNSXCuoi.Substring(3));

                //Tăng 1 đơn vị
                int soMoi = ++laySoCuoi;
                if (soMoi <= 9)
                {
                    nhaSanXuat.MaNhaSanXuat = "NSX0" + soMoi.ToString();
                }
                else
                {
                    nhaSanXuat.MaNhaSanXuat = "NSX" + soMoi.ToString();
                }


                db.NhaSanXuats.Add(nhaSanXuat);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nhaSanXuat);
        }

        // GET: Admin/NhaSanXuats/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhaSanXuat nhaSanXuat = db.NhaSanXuats.Find(id);
            if (nhaSanXuat == null)
            {
                return HttpNotFound();
            }
            return View(nhaSanXuat);
        }

        // POST: Admin/NhaSanXuats/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaNhaSanXuat,TenNhaSanXuat")] NhaSanXuat nhaSanXuat)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nhaSanXuat).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nhaSanXuat);
        }

        // GET: Admin/NhaSanXuats/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhaSanXuat nhaSanXuat = db.NhaSanXuats.Find(id);
            if (nhaSanXuat == null)
            {
                return HttpNotFound();
            }
            return View(nhaSanXuat);
        }

        // POST: Admin/NhaSanXuats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            NhaSanXuat nhaSanXuat = db.NhaSanXuats.Find(id);

            var sanPham = db.SanPhams
                           .Where(s => s.MaNhaSanXuat == id)
                           .Select(s => s);

            //xóa tất cả sản phẩm thuộc nhà sản xuất, bao gồm bình luận, tương tác
            foreach(var itemSP in sanPham)
            {
                //Xóa bình luận
                var binhLuan = db.BinhLuans
                                .Where(b => b.MaSanPham == itemSP.MaSanPham)
                                .Select(b => b);
                foreach(var itemBL in binhLuan)
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

                db.SanPhams.Remove(itemSP);
            }

            db.NhaSanXuats.Remove(nhaSanXuat);
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
