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
    public class DanhMucConsController : Controller
    {
        private ChanhThuStoreContext db = new ChanhThuStoreContext();

        // GET: DanhMucCons
        public ActionResult Index()
        {
            var danhMucCons = db.DanhMucCons.Include(d => d.DanhMuc);
            return View(danhMucCons.ToList());
        }

        // GET: DanhMucCons/Details/5
        public ActionResult Details(string id)
        {
            ViewBag.Id = id;
            return View();
        }
        public ActionResult Sapxep(string id,string sapxep)
        {
            ViewBag.SapXep = sapxep;

            IQueryable<SanPham> listSanPham = null;
            var userID = User.Identity.GetUserId();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            listSanPham = from s in db.SanPhams
                          where s.MaDanhMucCon == id && s.SoLuongTonKho > 0
                          orderby s.MaSanPham
                          select s;
            /*Check yêu thích*/
            foreach (SanPham item in listSanPham)
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
                    listSanPham = listSanPham.OrderBy(s => s.MaSanPham);
                    break;
                case "ten-A-Z":
                    listSanPham = listSanPham.OrderBy(s => s.TenSanPham);
                    break;
                case "ten-Z-A":
                    listSanPham = listSanPham.OrderByDescending(s => s.TenSanPham);
                    break;
                case "gia-thap-cao":
                    listSanPham = listSanPham.OrderBy(s => s.Gia);
                    break;
                case "gia-cao-thap":
                    listSanPham = listSanPham.OrderByDescending(s => s.Gia);
                    break;
                default:
                    listSanPham = listSanPham.OrderByDescending(s => s.MaSanPham);
                    break;
            }

          
            return PartialView(listSanPham.ToList());
        }
        public ActionResult DanhSachDMC(string id)
        {
            var listDanhMucCon = db.DanhMucCons
                                .Where(d => d.MaDanhMuc == id)
                                .Select(d => d);

            foreach (var item in listDanhMucCon)
            {
                var soSP = db.SanPhams
                                .Where(s => s.MaDanhMucCon == item.MaDanhMucCon)
                                .Count();
                item.soSP = soSP;
            }
            return PartialView(listDanhMucCon);
        }

        public ActionResult DanhSachDM()
        {
            var listDanhMuc = db.DanhMucs
                                .OrderBy(d=>d.MaDanhMuc)
                                .Select(d => d);

            foreach (var item in listDanhMuc)
            {
                var soDMC = db.DanhMucCons
                                .Where(s => s.MaDanhMuc == item.MaDanhMuc)
                                .Count();
                item.soDMC = soDMC;
            }
            return PartialView(listDanhMuc);
        }


        public ActionResult TenBoSuTap()
        {
            IQueryable<DanhMuc> listDanhMuc = null;

            listDanhMuc = from d in db.DanhMucs
                          orderby d.MaDanhMuc
                          select d;
            return PartialView(listDanhMuc);
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
