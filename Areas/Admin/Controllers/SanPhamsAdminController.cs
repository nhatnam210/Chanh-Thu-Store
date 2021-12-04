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
            ViewBag.SapXep = sapxep;
            ViewBag.SapXepMa = String.IsNullOrEmpty(sapxep) ? "mã tăng dần" : "";
            ViewBag.SapXepTen = sapxep == "tên A-Z" ? "tên Z-A" : "tên A-Z";
            ViewBag.SapXepTonKho = sapxep == "tồn kho thấp > cao" ? "tồn kho cao > thấp" : "tồn kho thấp > cao";
            ViewBag.SapXepLoai = sapxep == "loại A-Z" ? "loại Z-A" : "loại A-Z";
            ViewBag.SapXepGia = sapxep == "giá thấp > cao" ? "giá cao > thấp" : "giá thấp > cao";
            ViewBag.SapXepTinhTrang= sapxep == "còn hàng" ? "hết hàng" : "còn hàng";
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
                sanpham = sanpham.Where(s => s.TenSanPham.Contains(timkiem) 
                || s.DanhMucCon.TenDanhMucCon.Contains(timkiem)
                || s.MaSanPham.Contains(timkiem));
                //|| s.author.Contains(timkiem)

            }
            //sắp xếp 
            switch (sapxep)
            {
                case "mã tăng dần":
                    sanpham = sanpham.OrderBy(s => s.MaSanPham);
                    break;
                case "tên A-Z":
                    sanpham = sanpham.OrderBy(s => s.TenSanPham);
                    break;
                case "tên Z-A":
                    sanpham = sanpham.OrderByDescending(s => s.TenSanPham);
                    break;
                case "tồn kho thấp > cao":
                    sanpham = sanpham.OrderBy(s => s.SoLuongTonKho);
                    break;
                case "tồn kho cao > thấp":
                    sanpham = sanpham.OrderByDescending(s => s.SoLuongTonKho);
                    break;
                case "loại A-Z":
                    sanpham = sanpham.OrderBy(s => s.DanhMucCon.TenDanhMucCon);
                    break;
                case "loại Z-A":
                    sanpham = sanpham.OrderByDescending(s => s.DanhMucCon.TenDanhMucCon);
                    break;
                case "giá thấp > cao":
                    sanpham = sanpham.OrderBy(s => s.Gia);
                    break;
                case "giá cao > thấp":
                    sanpham = sanpham.OrderByDescending(s => s.Gia);
                    break;
                case "hết hàng":
                    sanpham = sanpham.OrderBy(s => s.TinhTrang);
                    break;
                case "còn hàng":
                    sanpham = sanpham.OrderByDescending(s => s.TinhTrang);
                    break;
                default:
                    sanpham = sanpham.OrderByDescending(s => s.MaSanPham);
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
            
            ViewBag.MaDanhMucCon = new SelectList(db.DanhMucCons, "MaDanhMucCon", "TenDanhMucCon");
            ViewBag.MaNhaSanXuat = new SelectList(db.NhaSanXuats, "MaNhaSanXuat", "TenNhaSanXuat");
            return View();
        }

        //POST: Admin/SanPhamsAdmin/Create
        //To protect from overposting attacks, enable the specific properties you want to bind to, for 
         //more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaSanPham,MaDanhMucCon,MaNhaSanXuat,TenSanPham,Gia,HinhChinh,Hinh1,Hinh2,Mota,SoLuongTonKho,SoLuongDaBan,LuotYeuThich,NgaySanXuat,HanSuDung,Diem,TinhTrang")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                //Lấy chuỗi MaSanPham của phần tử cuối bảng
                string maSPCuoi = db.SanPhams
                                      .OrderByDescending(d => d.MaSanPham)
                                      .First().MaSanPham;

                //Cắt lấy phần chữ số và ép kiểu
                int laySoCuoi = Convert.ToInt32(maSPCuoi.Substring(2));

                //Tăng 1 đơn vị
                int soMoi = ++laySoCuoi;
                if (soMoi <= 9)
                {
                    sanPham.MaSanPham = "SP0" + soMoi.ToString();
                }
                else
                {
                    sanPham.MaSanPham = "SP" + soMoi.ToString();
                }

                sanPham.LuotYeuThich = 0;

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
            ViewBag.MaDanhMucCon = new SelectList(db.DanhMucCons, "MaDanhMucCon", "TenDanhMucCon");
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

            //Xóa bình luận
            var binhLuan = db.BinhLuans
                            .Where(b => b.MaSanPham == id)
                            .Select(b => b);
            db.BinhLuans.RemoveRange(binhLuan);

            //Xóa Yêu thích
            var yeuThich = db.TuongTacs
                           .Where(t => t.MaSanPham == id)
                           .Select(t => t);
            db.TuongTacs.RemoveRange(yeuThich);

            //Xóa CTHD và HD
            var chiTietHoaDon = db.ChiTietHoaDons
                           .Where(c => c.MaSanPham == id)
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
