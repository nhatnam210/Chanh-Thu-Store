using ChanhThu_Store.Models;
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
    public class NhaSanXuatsController : Controller
    {
        private ChanhThuStoreContext db = new ChanhThuStoreContext();
        
        // GET: Admin/NhaSanXuats
        public ActionResult Index(string sapxep, string loc, string timkiem, int? trang)
        {
            ViewBag.Sapxep = sapxep;
            ViewBag.SapxepMa = String.IsNullOrEmpty(sapxep) ? "Id" : "";
            ViewBag.SapxepTen = sapxep == "Ten" ? "Ten_desc" : "Ten";

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
            var nhasanxuat = from s in db.NhaSanXuats
                           select s;
            if (!String.IsNullOrEmpty(timkiem))
            {
                nhasanxuat = nhasanxuat.Where(s => s.TenNhaSanXuat.Contains(timkiem));
                //|| s.author.Contains(timkiem)

            }
            //sắp xếp 
            switch (sapxep)
            {
                case "Id":
                    nhasanxuat = nhasanxuat.OrderByDescending(s => s.MaNhaSanXuat);
                    break;
                case "Ten":
                    nhasanxuat = nhasanxuat.OrderBy(s => s.TenNhaSanXuat);
                    break;
                case "Ten_desc":
                    nhasanxuat = nhasanxuat.OrderByDescending(s => s.TenNhaSanXuat);
                    break;
                default:
                    nhasanxuat = nhasanxuat.OrderBy(s => s.MaNhaSanXuat);
                    break;
            }
            //var articles = db.Articles.Include(a => a.Cetegory);
            int pageSize = 5;
            int pageNumber = (trang ?? 1);
            return View(nhasanxuat.ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/NhaSanXuats/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhaSanXuat nhaSanXuat = db.NhaSanXuats.Find(id);
            if (nhaSanXuat == null)
            {
                return HttpNotFound();
            }
            return View(nhaSanXuat);
        }

        // GET: Admin/NhaSanXuats/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/NhaSanXuats/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaNhaSanXuat,TenNhaSanXuat")] NhaSanXuat nhaSanXuat)
        {
            if (ModelState.IsValid)
            {
                db.NhaSanXuats.Add(nhaSanXuat);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nhaSanXuat);
        }

        // GET: Admin/NhaSanXuats/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhaSanXuat nhaSanXuat = db.NhaSanXuats.Find(id);
            if (nhaSanXuat == null)
            {
                return HttpNotFound();
            }
            return View(nhaSanXuat);
        }

        // POST: Admin/NhaSanXuats/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaNhaSanXuat,TenNhaSanXuat")] NhaSanXuat nhaSanXuat)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nhaSanXuat).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nhaSanXuat);
        }

        // GET: Admin/NhaSanXuats/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhaSanXuat nhaSanXuat = db.NhaSanXuats.Find(id);
            if (nhaSanXuat == null)
            {
                return HttpNotFound();
            }
            return View(nhaSanXuat);
        }

        // POST: Admin/NhaSanXuats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            NhaSanXuat nhaSanXuat = db.NhaSanXuats.Find(id);
            db.NhaSanXuats.Remove(nhaSanXuat);
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
