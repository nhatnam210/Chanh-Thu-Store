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
using ChanhThu_Store.Controllers;

namespace ChanhThu_Store.Areas.Admin.Controllers
{
    public class VouchersAdminController : Controller
    {
        DateTime thisDay = DateTime.Today;
        private ChanhThuStoreContext db = new ChanhThuStoreContext();

        // GET: Admin/VouchersAdmin
        public ActionResult Index(string sapxep, string loc, string timkiem, int? trang)
        {
            ViewBag.SapXep = sapxep;
            ViewBag.SapXepMa = String.IsNullOrEmpty(sapxep) ? "mã tăng dần" : "";
            ViewBag.SapXepTen = sapxep == "tên A-Z" ? "tên Z-A" : "tên A-Z";
            ViewBag.SapXepGiaTri = sapxep == "giá trị thấp > cao" ? "giá trị cao > thấp" : "giá trị thấp > cao";
            ViewBag.SapXepHSD = sapxep == "hạn sử dụng tăng dần" ? "hạn sử dụng giảm dần" : "hạn sử dụng tăng dần";
            //phan trang
            if (timkiem != null)
            {
                trang = 1;
            }
            else
            {
                timkiem = loc;
            }
            ViewBag.Loc = timkiem;
            //tìm kiếm
            var voucher = from s in db.Vouchers
                          select s;
            if (!String.IsNullOrEmpty(timkiem))
            {
                timkiem = timkiem.Trim();
                var timkiemUnsign = TimKiemController.ConvertToUnSignNoneSpace(timkiem);
                voucher = voucher.Where(delegate (Voucher s)
                {
                    if (TimKiemController.ConvertToUnSignNoneSpace(s.TenVoucher).IndexOf(timkiemUnsign, StringComparison.CurrentCultureIgnoreCase) >= 0
                    || TimKiemController.ConvertToUnSignNoneSpace(s.MaVoucher.ToString()).IndexOf(timkiemUnsign, StringComparison.CurrentCultureIgnoreCase) >= 0
                    )
                        return true;
                    else
                        return false;
                }).AsQueryable();
            }
            //sắp xếp 
            switch (sapxep)
            {
                case "mã tăng dần":
                    voucher = voucher.OrderBy(s => s.MaVoucher);
                    break;
                case "hạn sử dụng tăng dần":
                    voucher = voucher.OrderBy(s => s.HanSuDung);
                    break;
                case "hạn sử dụng giảm dần":
                    voucher = voucher.OrderByDescending(s => s.HanSuDung);
                    break;
                case "tên A-Z":
                    voucher = voucher.OrderBy(s => s.TenVoucher);
                    break;
                case "tên Z-A":
                    voucher = voucher.OrderByDescending(s => s.TenVoucher);
                    break;
                case "giá trị thấp > cao":
                    voucher = voucher.OrderBy(s => s.GiaTriGiam);
                    break;
                case "giá trị cao > thấp":
                    voucher = voucher.OrderByDescending(s => s.GiaTriGiam);
                    break;
                default:
                    voucher = voucher.OrderBy(s => s.MaVoucher);
                    break;
            }
            //var articles = db.Articles.Include(a => a.Cetegory);
            int pageSize = 5;
            int pageNumber = (trang ?? 1);
            return View(voucher.ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/VouchersAdmin/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Voucher voucher = db.Vouchers.Find(id);
            if (voucher == null)
            {
                return HttpNotFound();
            }
            return View(voucher);
        }

        // GET: Admin/VouchersAdmin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/VouchersAdmin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaVoucher,TenVoucher,GiaTriGiam,DiemDoi,HanSuDung")] Voucher voucher)
        {
            if (ModelState.IsValid)
            {
                db.Vouchers.Add(voucher);
                try
                {
                    db.SaveChanges();
                }
                catch
                {
                    ViewBag.Message = "Mã này đã tồn tại!";
                    //return new HttpStatusCodeResult(204);
                    return Redirect(Request.UrlReferrer.ToString());
                }
               
                return RedirectToAction("Index");
            }

            return View(voucher);
        }

        // GET: Admin/VouchersAdmin/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Voucher voucher = db.Vouchers.Find(id);
            if (voucher == null)
            {
                return HttpNotFound();
            }
            return View(voucher);
        }

        // POST: Admin/VouchersAdmin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaVoucher,TenVoucher,GiaTriGiam,DiemDoi,HanSuDung")] Voucher voucher)
        {
            if (ModelState.IsValid)
            {
                db.Entry(voucher).State = EntityState.Modified;
                db.SaveChanges();

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(voucher);
        }

        // GET: Admin/VouchersAdmin/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Voucher voucher = db.Vouchers.Find(id);
            if (voucher == null)
            {
                return HttpNotFound();
            }
            return View(voucher);
        }

        // POST: Admin/VouchersAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            var userID = User.Identity.GetUserId();

            //kiểm tra voucher này có tồn tại trong ChiTietVoucher
            var voucherUser = db.ChiTietVouchers
                                .Where(v => v.MaVoucher == id )
                                .Select(v => v);

            foreach(var item in voucherUser)
            {
                //voucher còn sử dụng được
                if (item.Voucher.HanSuDung >= thisDay)
                {
                    //cộng bù lại điểm của user trước khi xóa voucher
                    item.AspNetUser.DiemTichLuy += item.Voucher.DiemDoi * item.SoLuong;
                }

                //xóa đi 1 item voucher tương ứng đó trong ChiTietVoucher
                db.ChiTietVouchers.Remove(item);
            }

            Voucher voucher = db.Vouchers.Find(id);
            db.Vouchers.Remove(voucher);
            db.SaveChanges();
            return RedirectToAction("Index");
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
