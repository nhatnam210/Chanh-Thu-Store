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
        public ActionResult Details(string id, int? trang, string sapxep)
        {
            IQueryable<SanPham> listSanPham = null;
            var userID = User.Identity.GetUserId();

            ViewBag.SapXepHienTai = sapxep;
            ViewBag.SapXepTen = "ten-A-Z";
            ViewBag.SapXepTenGiam = "ten-Z-A";
            ViewBag.SapXepGia = "gia-tang-dan";
            ViewBag.SapXepGiaGiam = "gia-tang-dan";

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            listSanPham = from s in db.SanPhams
                          where s.MaDanhMucCon == id
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

            //sắp xếp 
            switch (sapxep)
            {
                case "ten-A-Z":
                    listSanPham = listSanPham.OrderBy(s => s.TenSanPham);
                    break;
                case "ten-Z-A":
                    listSanPham = listSanPham.OrderByDescending(s => s.TenSanPham);
                    break;
                case "gia-tang-dan":
                    listSanPham = listSanPham.OrderBy(s => s.Gia);
                    break;
                case "gia-giam-dan":
                    listSanPham = listSanPham.OrderByDescending(s => s.Gia);
                    break;
                default:
                    listSanPham = listSanPham.OrderByDescending(s => s.MaSanPham);
                    break;
            }

            int pageNumber = (trang ?? 1);
            int pageSize = 9;

            var showListSanPham = listSanPham.ToPagedList(pageNumber, pageSize);
            return View(showListSanPham);

        }

        //public ActionResult SanPhamTheoDMC(string id, int? trang, string sapxep)
        //{
        //    ViewBag.Sapxep = sapxep;

        //    //ViewBag.SapxepTen = sapxep == "Ten";
        //    //ViewBag.SapxepTenGiam = sapxep == "Ten_desc";
        //    //ViewBag.SapxepGia = sapxep == "Gia";
        //    //ViewBag.SapxepGiaGiam = sapxep == "Gia_desc";
        //    IQueryable<SanPham> listSanPham = null;
        //    var userID = User.Identity.GetUserId();

        //    listSanPham = from s in db.SanPhams
        //                  where s.MaDanhMucCon == id
        //                  orderby s.MaSanPham
        //                  select s;
        //    foreach (SanPham item in listSanPham)
        //    {
        //        if (userID != null)
        //        {
        //            item.isLogin = true;

        //            TuongTac find = db.TuongTacs.FirstOrDefault(p => p.MaSanPham == item.MaSanPham && p.MaKhachHang == userID);
        //            if (find != null)
        //                item.isLiked = true;
        //        }
        //    }
        //    //sắp xếp 
        //    switch (sapxep)
        //    {
        //        case "Ten":
        //            listSanPham = listSanPham.OrderBy(s => s.TenSanPham);
        //            break;
        //        case "TenGiam":
        //            listSanPham = listSanPham.OrderByDescending(s => s.TenSanPham);
        //            break;
        //        case "Gia":
        //            listSanPham = listSanPham.OrderBy(s => s.Gia);
        //            break;
        //        case "GiaGiam":
        //            listSanPham = listSanPham.OrderByDescending(s => s.Gia);
        //            break;
        //        default:
        //            listSanPham = listSanPham.OrderBy(s => s.MaSanPham);
        //            break;
        //    }
        //    //var articles = db.Articles.Include(a => a.Cetegory);
        //    int pageSize = 5;
        //    int pageNumber = (trang ?? 1);
        //    return PartialView("SanPhamTheoDMC", listSanPham.ToPagedList(pageNumber, pageSize));
        //}

        public ActionResult _BoSuTap_DM()
        {
            IQueryable<DanhMuc> listDanhMuc = null;

            listDanhMuc = from d in db.DanhMucs
                          orderby d.MaDanhMuc
                          select d;
            return PartialView("_BoSuTap_DM", listDanhMuc);
        }

        public ActionResult DemSP_DMC(String id)
        {
            IQueryable<SanPham> listSanPham = null;

            listSanPham = (from s in db.SanPhams
                           where s.MaDanhMucCon == id
                           select s).Distinct();
            return PartialView("DemSP_DMC", listSanPham);
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
