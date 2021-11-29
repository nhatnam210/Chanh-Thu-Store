using ChanhThu_Store.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChanhThu_Store.Controllers
{
    public class HomeController : Controller
    {
        private ChanhThuStoreContext db = new ChanhThuStoreContext();
        private const string CartSession = "CartSession";
        DateTime thisDay = DateTime.Today;

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult NotFound()
        {
            return View();
        }

        [ChildActionOnly]
        public PartialViewResult HeaderCart()
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return PartialView(list);
        }

        public IQueryable<SanPham> ListBanChay()
        {
            IQueryable<SanPham> listBanChay = null;

            listBanChay = (from b in db.SanPhams
                           orderby b.SoLuongDaBan descending
                           select b).Take(4);

            //listBanChay = db.ChiTietHoaDons
            //    .Where(c => c.HoaDon.NgayLap.Month == thisDay.Month)
            //    .GroupBy(c => c.HoaDon.NgayLap.Month)
            //    .Select(s => new SanPham
            //    {
            //        SoLuongDaBan = s.Sum(c => c.Soluong),
            //    })
            //    .OrderByDescending(s => s.SoLuongDaBan)
            //    .Take(4);
                
            return listBanChay;
        }

        public PartialViewResult SanPhamBanChay()
        {
            return PartialView(ListBanChay());
        }

        public PartialViewResult SanPhamNoiBat()
        {
            IQueryable<SanPham> listNoiBat = null;

            listNoiBat = (from n in db.SanPhams
                          orderby n.LuotYeuThich descending
                          select n).Except(ListBanChay()).Take(4);

            return PartialView(listNoiBat);
        }

        public PartialViewResult SanPhamMoiNhat()
        {
            IQueryable<SanPham> listMoiNhat = null;

            listMoiNhat = (from m in db.SanPhams
                          orderby m.MaSanPham descending
                          select m).Take(3);

            return PartialView(listMoiNhat);
        }
    }
}