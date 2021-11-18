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
        [ChildActionOnly]
        public PartialViewResult _Header_Cart()
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return PartialView(list);
        }
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult DanhSachVoucher()
        {
            DateTime thisDay = DateTime.Today;
            var userID = User.Identity.GetUserId();

            IQueryable<Voucher> listVoucher = null;
            //IQueryable<Voucher> listVoucherByUser = null;

                listVoucher = from v in db.Vouchers
                          where v.HanSuDung >= thisDay
                          orderby v.MaVoucher 
                          select v;

            //AspNetUser currentUser = db.AspNetUsers.Find(userID);

            //foreach (var item in listVoucher)
            //{
            //    if (currentUser.DiemTichLuy >= item.DiemDoi)
            //    {
            //        if (item.HanSuDung >= thisDay)
            //        {
            //            var chitietVoucher = new ChiTietVoucher() { MaVoucher = item.MaVoucher, MaKhachHang = currentUser.Id, TinhTrang = true };
            //            db.ChiTietVouchers.Add(chitietVoucher);
            //            //var diemHienTai = currentUser.DiemTichLuy;
            //            //diemHienTai -= item.DiemDoi;
            //            //    if(diemHienTai <= 0)
            //            //    {
            //            //        diemHienTai = 0;
            //            //    }
            //            //currentUser.DiemTichLuy = diemHienTai;
            //        }
            //        else
            //        {
            //            var chitietVoucher = new ChiTietVoucher() { MaVoucher = item.MaVoucher, MaKhachHang = currentUser.Id, TinhTrang = false };
            //            db.ChiTietVouchers.Add(chitietVoucher);
            //            //var diemHienTai = currentUser.DiemTichLuy;
            //            //diemHienTai -= item.DiemDoi;
            //            //if (diemHienTai <= 0)
            //            //{
            //            //    diemHienTai = 0;
            //            //}
            //            //currentUser.DiemTichLuy = diemHienTai;
            //        }
            //    }
            //}

            //db.SaveChanges();

            //listVoucherByUser = from vou in db.Vouchers
            //                    join chi in db.ChiTietVouchers on vou.MaVoucher equals chi.MaVoucher
            //                    where vou.MaVoucher == chi.MaVoucher && chi.MaKhachHang == userID && chi.TinhTrang == true
            //                    select vou;

            return View(listVoucher);
        }

        [Authorize]
        public ActionResult DoiVoucher(String idVoucher)
        {
            DateTime thisDay = DateTime.Today;
            var userID = User.Identity.GetUserId();
            AspNetUser currentUser = db.AspNetUsers.Find(userID);

            Voucher voucher = db.Vouchers.Find(idVoucher);

            /*Nếu đủ điểm*/
            if (currentUser.DiemTichLuy >= voucher.DiemDoi)
            {
                /*Nếu trùng*/
                if (db.ChiTietVouchers.Any(p => p.MaKhachHang == userID && p.MaVoucher == idVoucher))
                {
                    var updateCTVC = db.ChiTietVouchers.SingleOrDefault(p => p.MaKhachHang == userID && p.MaVoucher == idVoucher);
                    /*Cập nhật số lượng*/
                    updateCTVC.SoLuong++;
                    db.Entry(updateCTVC).State = EntityState.Modified;

                    var diemHienTai = currentUser.DiemTichLuy;
                    diemHienTai -= voucher.DiemDoi;
                    if (diemHienTai <= 0)
                    {
                        diemHienTai = 0;
                    }
                    currentUser.DiemTichLuy = diemHienTai;
                }
                /*Nếu không trùng*/
                else
                {
                    if (voucher.HanSuDung >= thisDay)
                    {
                        var chitietVoucher = new ChiTietVoucher()
                        { MaVoucher = voucher.MaVoucher, MaKhachHang = currentUser.Id, TinhTrang = true, SoLuong = 1 };
                        db.ChiTietVouchers.Add(chitietVoucher);
                        var diemHienTai = currentUser.DiemTichLuy;
                        diemHienTai -= voucher.DiemDoi;
                        if (diemHienTai <= 0)
                        {
                            diemHienTai = 0;
                        }
                        currentUser.DiemTichLuy = diemHienTai;
                    }
                }
            }

            db.SaveChanges();
            return RedirectToAction("DanhSachVoucher", "Home");
        }

        public ActionResult LayDiemUser()
        {

            var userID = User.Identity.GetUserId();
            AspNetUser currentUser = new AspNetUser();
            if (userID != null)
            {
                currentUser = db.AspNetUsers.Find(userID);
            }

            return PartialView("LayDiemUser", currentUser);
        }
    }
}