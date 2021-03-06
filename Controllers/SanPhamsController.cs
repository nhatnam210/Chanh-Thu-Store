using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ChanhThu_Store.Models;
using Microsoft.AspNet.Identity;
using PagedList;

namespace ChanhThu_Store.Controllers
{
    public class SanPhamsController : Controller
    {
        private ChanhThuStoreContext db = new ChanhThuStoreContext();

        // GET: SanPhams
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Sapxep(string sapxep)
        {
            ViewBag.SapXep = sapxep;

            IQueryable<SanPham> listSanPhamMacDinh = null;
            var userID = User.Identity.GetUserId();

            var MaDMCMacDinh = db.DanhMucCons.FirstOrDefault() != null ? db.DanhMucCons.FirstOrDefault().MaDanhMucCon : "";

            listSanPhamMacDinh = db.SanPhams
                                .Where(s => s.MaDanhMucCon == MaDMCMacDinh && s.SoLuongTonKho > 0)
                                .OrderBy(s => s.MaSanPham)
                                .Select(s => s);
            /*Check yêu thích*/
            foreach (SanPham item in listSanPhamMacDinh)
            {
                if (userID != null)
                {
                    item.isLogin = true;

                    TuongTac find = db.TuongTacs.FirstOrDefault(p => p.MaSanPham == item.MaSanPham && p.MaKhachHang == userID);
                    if (find != null)
                        item.isLiked = true;
                }
            }

            //Sắp xếp
            switch (sapxep)
            {
                case "mac-dinh":
                    listSanPhamMacDinh = listSanPhamMacDinh.OrderBy(s => s.MaSanPham);
                    break;
                case "ten-A-Z":
                    listSanPhamMacDinh = listSanPhamMacDinh.OrderBy(s => s.TenSanPham);
                    break;
                case "ten-Z-A":
                    listSanPhamMacDinh = listSanPhamMacDinh.OrderByDescending(s => s.TenSanPham);
                    break;
                case "gia-thap-cao":
                    listSanPhamMacDinh = listSanPhamMacDinh.OrderBy(s => s.Gia);
                    break;
                case "gia-cao-thap":
                    listSanPhamMacDinh = listSanPhamMacDinh.OrderByDescending(s => s.Gia);
                    break;
                default:
                    listSanPhamMacDinh = listSanPhamMacDinh.OrderByDescending(s => s.MaSanPham);
                    break;
            }

            ViewBag.listsp = listSanPhamMacDinh;
            return PartialView(listSanPhamMacDinh.ToList());
        }
        // GET: SanPhams/Details/5
        public ActionResult Details(string id)
        {
            ViewBag.MaSanPham = id;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            SanPham sanpham = db.SanPhams.Find(id);
            if (sanpham == null)
            {
                return RedirectToAction("NotFound","Home");
            }
            var userID = User.Identity.GetUserId();
            var objProduct = db.SanPhams.Where(p => p.MaSanPham == id);

            foreach (SanPham item in objProduct)
            {

                if (userID != null)
                {
                    item.isLogin = true;

                    TuongTac find = db.TuongTacs.FirstOrDefault(p => p.MaSanPham == item.MaSanPham && p.MaKhachHang == userID);
                    if (find != null)
                        item.isLiked = true;
                }
            }

            return View(sanpham);
        }

        [Authorize]
        public ActionResult Binhluan(string masanpham, string noidung = "")
        {
            DateTime thisDay = DateTime.Today;
            var userID = User.Identity.GetUserId();
            SanPham find = db.SanPhams.FirstOrDefault(p => p.MaSanPham == masanpham);
            if (find != null && userID !=null)
            {
                if(noidung.Trim().Length >= 0)
                {
                    var binhluan = new BinhLuan() { MaSanPham = masanpham, MaKhachHang = userID, NoiDungBinhLuan = noidung.Trim(), NgayBinhLuan = thisDay };

                    db.BinhLuans.Add(binhluan);
                    db.SaveChanges();
                }
            }

            //string returnURl = "/cua-hang/san-pham?id=" + ViewBag.MaSanPham;
            //return Redirect(returnURl);
            //return Redirect(Request.UrlReferrer.ToString());
            var loadBinhLuan = db.BinhLuans.Where(p => p.MaSanPham == masanpham)
                                           .OrderByDescending(p => p.NgayBinhLuan)
                                           .OrderByDescending(p => p.MaBinhLuan);

            return PartialView("ShowDanhSachBinhLuan", loadBinhLuan.ToList()); //res
        }

        [AllowAnonymous]
        public ActionResult ShowDanhSachBinhLuan(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            SanPham sanpham = db.SanPhams.Find(id);
            if (sanpham == null)
            {
                return RedirectToAction("NotFound", "Home");
            }
            var listBinhLuan = db.BinhLuans.Where(p => p.MaSanPham == id)
                                           .OrderByDescending(p=> p.NgayBinhLuan)
                                           .OrderByDescending(p=>p.MaBinhLuan);

            return PartialView("ShowDanhSachBinhLuan", listBinhLuan.ToList());
        }

        //public ActionResult LoadTime()
        //{
        //    var text = DateTime.Now.ToString("HH:mm:ss tt");
        //    return Content(text);
        //}

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
