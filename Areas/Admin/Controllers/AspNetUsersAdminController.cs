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
    [Authorize(Roles = "Admin")]
    public class AspNetUsersAdminController : Controller
    {
        private ChanhThuStoreContext db = new ChanhThuStoreContext();
       
        // GET: Admin/AspNetUsersAdmin
        public ActionResult Index(string sapxep, string loc, string timkiem, int? trang)
        {
            ViewBag.SapXep = sapxep;
            ViewBag.SapXepTen = sapxep == "tên A-Z" ? "tên Z-A" : "tên A-Z";
            ViewBag.SapXepXacThuc = sapxep == "xác thực" ? "chưa xác thực" : "xác thực";
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
            var khachhang = from s in db.AspNetUsers
                             select s;
            if (!String.IsNullOrEmpty(timkiem))
            {
                khachhang = khachhang.Where(s => s.Ten.Contains(timkiem));

            }
            //sắp xếp 
            switch (sapxep)
            {
                case "tên A-Z":
                    khachhang = khachhang.OrderBy(s => s.Ten);
                    break;
                case "tên Z-A":
                    khachhang = khachhang.OrderByDescending(s => s.Ten);
                    break;
                case "xác thực":
                    khachhang = khachhang.OrderByDescending(s => s.EmailConfirmed);
                    break;
                case "chưa xác thực":
                    khachhang = khachhang.OrderBy(s => s.EmailConfirmed);
                    break;
                default:
                    khachhang = khachhang.OrderBy(s => s.Id);
                    break;
            }
            int pageSize = 5;
            int pageNumber = (trang ?? 1);
            return View(khachhang.ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/AspNetUsersAdmin/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUser aspNetUser = db.AspNetUsers.Find(id);
            if (aspNetUser == null)
            {
                return HttpNotFound();
            }
            return View(aspNetUser);
        }

        // GET: Admin/AspNetUsersAdmin/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Admin/AspNetUsersAdmin/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,Ten,UserName,Email,EmailConfirmed,PasswordHash,PhoneNumber,DiaChi,DiemTichLuy,SecurityStamp,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount")] AspNetUser aspNetUser)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.AspNetUsers.Add(aspNetUser);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(aspNetUser);
        //}

        // GET: Admin/AspNetUsersAdmin/Edit/5
        //public ActionResult Edit(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    AspNetUser aspNetUser = db.AspNetUsers.Find(id);
        //    if (aspNetUser == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(aspNetUser);
        //}

        //// POST: Admin/AspNetUsersAdmin/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,Ten,UserName,Email,EmailConfirmed,PasswordHash,PhoneNumber,DiaChi,DiemTichLuy,SecurityStamp,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount")] AspNetUser aspNetUser)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(aspNetUser).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(aspNetUser);
        //}

        // GET: Admin/AspNetUsersAdmin/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUser aspNetUser = db.AspNetUsers.Find(id);
            if (aspNetUser == null)
            {
                return HttpNotFound();
            }
            return View(aspNetUser);
        }

        // POST: Admin/AspNetUsersAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            AspNetUser aspNetUser = db.AspNetUsers.Find(id);
            db.AspNetUsers.Remove(aspNetUser);
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
