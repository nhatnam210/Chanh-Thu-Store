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

namespace ChanhThu_Store.Controllers
{
    public class SanPhamsController : Controller
    {
        private ChanhThuStoreContext db = new ChanhThuStoreContext();

        // GET: SanPhams
        public ActionResult Index()
        {
            IQueryable<DanhMucCon> listDanhMucCon = null;

            listDanhMucCon = from s in db.DanhMucCons
                          orderby s.MaDanhMucCon
                          select s;

            return View(listDanhMucCon.FirstOrDefault());
        }

        // GET: SanPhams/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            SanPham sanpham = db.SanPhams.Find(id);
            if (sanpham == null)
            {
                return HttpNotFound();
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
            if (find != null)
            {
                if(noidung.Trim().Length >= 0)
                {
                    var binhluan = new BinhLuan() { MaSanPham = masanpham, MaKhachHang = userID, NoiDungBinhLuan = noidung.Trim(), NgayBinhLuan = thisDay };

                    db.BinhLuans.Add(binhluan);
                    db.SaveChanges();
                }
            }

            return Redirect(Request.UrlReferrer.ToString());
        }
        public ActionResult ShowDanhSachBinhLuan(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            SanPham sanpham = db.SanPhams.Find(id);
            if (sanpham == null)
            {
                return HttpNotFound();
            }
            var objBinhLuan = db.BinhLuans.Where(p => p.MaSanPham == id);


            return PartialView("ShowDanhSachBinhLuan", objBinhLuan);
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
