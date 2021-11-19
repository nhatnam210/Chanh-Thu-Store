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
        public ActionResult DanhSachVoucher()
        {
            DateTime thisDay = DateTime.Today;
            var userID = User.Identity.GetUserId();

            IQueryable<Voucher> listVoucher = null;

            listVoucher = from v in db.Vouchers
                          where v.HanSuDung >= thisDay
                          orderby v.DiemDoi ascending
                          select v;

            return View(listVoucher);
        }

        [Authorize]
        public ActionResult DoiVoucher(String idVoucher)
        {
            DateTime thisDay = DateTime.Today;
            var userID = User.Identity.GetUserId();
            AspNetUser currentUser = db.AspNetUsers.Find(userID);
            var diemHienTai = currentUser.DiemTichLuy;

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
                    //db.Entry(updateCTVC).State = EntityState.Modified;

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
            return RedirectToAction("DanhSachVoucher", "Vouchers");
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
