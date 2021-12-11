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
            //Cập nhật tình trạng voucher của tất cả user khi về trang chủ
            IQueryable<ChiTietVoucher> listChiTietVoucherFalse = null;
            //danh sách voucher hết hạn của tất cả user
            listChiTietVoucherFalse = from ctvc in db.ChiTietVouchers
                                 join vc in db.Vouchers on ctvc.MaVoucher equals vc.MaVoucher
                                 where vc.HanSuDung < thisDay
                                 select ctvc;

            //Cập nhật tình trạng hoặc xóa
            foreach (var item in listChiTietVoucherFalse)
            {
                DateTime hansudung = Convert.ToDateTime(item.Voucher.HanSuDung);
                DateTime ngayhientai = Convert.ToDateTime(thisDay);
                int chenhlech = (ngayhientai - hansudung).Days;
                //Xóa voucher trong CTVC nếu hết hạn quá 30 ngày của tất cả user
                if (chenhlech > 30)
                {
                    db.ChiTietVouchers.Remove(db.ChiTietVouchers
                        .SingleOrDefault(p => p.MaKhachHang == item.MaKhachHang && p.MaVoucher == item.MaVoucher));
                }
            }

            db.SaveChanges();
            return View();
        }
        public ActionResult NotFound()
        {
            return View();
        }
        public ActionResult Map()
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
                           orderby b.SoLuongDaBan descending,b.LuotYeuThich descending
                           select b).Take(4);
                
            return listBanChay;
        }

        public PartialViewResult SanPhamBanChay()
        {
            var userID = User.Identity.GetUserId();
            var listBanChay = ListBanChay();

            /*Check yêu thích*/
            foreach (SanPham item in listBanChay)
            {
                if (userID != null)
                {
                    item.isLogin = true;

                    TuongTac find = db.TuongTacs.FirstOrDefault(p => p.MaSanPham == item.MaSanPham && p.MaKhachHang == userID);
                    if (find != null)
                        item.isLiked = true;
                }
            }

            return PartialView(listBanChay.ToList());
        }

        public PartialViewResult SanPhamNoiBat()
        {
            IQueryable<SanPham> listNoiBat = null;
            var userID = User.Identity.GetUserId();

            listNoiBat = (from n in db.SanPhams
                          where n.LuotYeuThich > 0
                          orderby n.LuotYeuThich descending, n.SoLuongDaBan descending
                          select n).Except(ListBanChay()).Take(4);

            /*Check yêu thích*/
            foreach (SanPham item in listNoiBat)
            {
                if (userID != null)
                {
                    item.isLogin = true;

                    TuongTac find = db.TuongTacs.FirstOrDefault(p => p.MaSanPham == item.MaSanPham && p.MaKhachHang == userID);
                    if (find != null)
                        item.isLiked = true;
                }
            }


            return PartialView(listNoiBat.ToList());
        }

        public PartialViewResult SanPhamMoiNhat()
        {
            IQueryable<SanPham> listMoiNhat = null;

            listMoiNhat = (from m in db.SanPhams
                          orderby m.MaSanPham descending
                          select m).Take(3);

            return PartialView(listMoiNhat);
        }

        public PartialViewResult Footer_Info()
        {
            var thongTinCuaHang = db.ThongTinCuaHangs.FirstOrDefault();
            return PartialView(thongTinCuaHang);
        }

        public PartialViewResult SDTTop()
        {
            var thongTinCuaHang = db.ThongTinCuaHangs.FirstOrDefault();
            return PartialView(thongTinCuaHang);
        }

        public PartialViewResult TenCuaHangSliderHome()
        {
            var thongTinCuaHang = db.ThongTinCuaHangs.FirstOrDefault();
            return PartialView(thongTinCuaHang);
        }
    }
}