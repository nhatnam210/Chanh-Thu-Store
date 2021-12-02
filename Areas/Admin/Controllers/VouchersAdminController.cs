using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ChanhThu_Store.Models;
using PagedList;

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
            ViewBag.SapXepMa = String.IsNullOrEmpty(sapxep) ? "Id" : "";
            ViewBag.SapXepTen = sapxep == "ten" ? "ten-giam-dan" : "ten";
            ViewBag.SapXepGiaTri = sapxep == "gia-tri-tang-dan" ? "gia-tri-giam-dan" : "gia-tri-tang-dan";
            ViewBag.SapXepHSD = sapxep == "han-su-dung-tang-dan" ? "han-su-dung-giam-dan" : "han-su-dung-tang-dan";
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
                voucher = voucher.Where(s => s.TenVoucher.Contains(timkiem)
                || s.MaVoucher.Contains(timkiem)
                );
                //|| s.author.Contains(timkiem)

            }
            //sắp xếp 
            switch (sapxep)
            {
                case "Id":
                    voucher = voucher.OrderBy(s => s.MaVoucher);
                    break;
                case "han-su-dung-tang-dan":
                    voucher = voucher.OrderBy(s => s.HanSuDung);
                    break;
                case "han-su-dung-ngan-dan":
                    voucher = voucher.OrderByDescending(s => s.HanSuDung);
                    break;
                case "ten":
                    voucher = voucher.OrderBy(s => s.TenVoucher);
                    break;
                case "ten-giam-dan":
                    voucher = voucher.OrderByDescending(s => s.TenVoucher);
                    break;
                case "gia-tri-tang-dan":
                    voucher = voucher.OrderBy(s => s.GiaTriGiam);
                    break;
                case "gia-tri-giam-dan":
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

                /*thay đổi lại tình trạng của voucher tất cả user*/
                IQueryable<Voucher> listVoucherAfterUpdate = null;

                //danh sách voucher sau khi update
                listVoucherAfterUpdate = db.Vouchers.Select(vc => vc);

                if (listVoucherAfterUpdate.Count() > 0)
                {
                    //Cập nhật tình trạng mới cho bảng ChiTietVoucher
                    foreach (var itemVC in listVoucherAfterUpdate)
                    {
                        IQueryable<ChiTietVoucher> listChiTietVoucherAfterUpdate = null;
                        //danh sách ChiTietVoucher tương ứng theo MaVoucher
                        listChiTietVoucherAfterUpdate = from ctvc in db.ChiTietVouchers
                                                        where ctvc.MaVoucher == itemVC.MaVoucher
                                                        select ctvc;
                        //Nếu có MaVoucher tương ứng trong bảng ChiTietVoucher
                        if (listChiTietVoucherAfterUpdate.Count() > 0)
                        {
                            foreach (var itemCTVC in listChiTietVoucherAfterUpdate)
                            {
                                if (itemVC.HanSuDung >= thisDay)
                                {
                                    itemCTVC.TinhTrang = true;
                                }
                                else
                                {
                                    itemCTVC.TinhTrang = false;
                                }
                            }
                        }
                    }
                }
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
