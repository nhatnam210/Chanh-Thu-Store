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

        public ActionResult SanPhamTheoDMC(string id)
        {
            IQueryable<SanPham> listSanPham = null;
            var userID = User.Identity.GetUserId();

            listSanPham = from s in db.SanPhams
                   where s.MaDanhMucCon == id
                   orderby s.MaSanPham
                   select s;
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

            return PartialView("SanPhamTheoDMC", listSanPham);
        }

        public ActionResult _BoSuTap_DM()
        { 
            IQueryable<DanhMuc> listDanhMuc = null;

             listDanhMuc = from d in db.DanhMucs
                          orderby d.MaDanhMuc
                          select d;
            return PartialView("_BoSuTap_DM", listDanhMuc);
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
