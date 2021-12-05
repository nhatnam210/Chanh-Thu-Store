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
    public class VouchersController : Controller
    {
        private ChanhThuStoreContext db = new ChanhThuStoreContext();
        DateTime thisDay = DateTime.Today;
        public ActionResult DanhSachVoucher()
        {
            var userID = User.Identity.GetUserId();

            IQueryable<Voucher> listVoucher = null;
            //hiển thị tất cả voucher còn hạn của shop
            listVoucher = from v in db.Vouchers
                          where v.HanSuDung >= thisDay
                          orderby v.DiemDoi ascending
                          select v;

            return View(listVoucher);
        }

        [Authorize]
        public ActionResult DoiVoucher(String idVoucher)
        {
            var userID = User.Identity.GetUserId();
            AspNetUser currentUser = db.AspNetUsers.Find(userID);
            var diemHienTai = currentUser.DiemTichLuy;

            Voucher voucher = db.Vouchers.Find(idVoucher);

            if (voucher != null)
            {
                /*Nếu đủ điểm*/
                if (currentUser.DiemTichLuy >= voucher.DiemDoi && voucher.HanSuDung >= thisDay)
                {
                    ChiTietVoucher updateCTVC = db.ChiTietVouchers.SingleOrDefault(p => p.MaKhachHang == userID && p.MaVoucher == idVoucher);
                    /*Nếu trùng*/
                    if (updateCTVC != null)
                    {
                        /*Cập nhật số lượng*/
                        updateCTVC.SoLuong++;

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
                        ChiTietVoucher chitietVoucher = new ChiTietVoucher()
                        { MaVoucher = voucher.MaVoucher, MaKhachHang = currentUser.Id, SoLuong = 1 };
                        db.ChiTietVouchers.Add(chitietVoucher);

                        diemHienTai -= voucher.DiemDoi;
                        if (diemHienTai <= 0)
                        {
                            diemHienTai = 0;
                        }
                        currentUser.DiemTichLuy = diemHienTai;
                    }
                }

                db.SaveChanges();
            }

            return new HttpStatusCodeResult(204);
            //return Redirect(Request.UrlReferrer.ToString());
            //return RedirectToAction("DanhSachVoucher", "Vouchers");
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

        public PartialViewResult QuangCaoVoucherTop()
        {
            return PartialView(db.Vouchers.ToList());
        }
    }
}
