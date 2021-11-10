using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ChanhThu_Store.Models;

namespace ChanhThu_Store.Areas.Admin.Controllers
{
    public class SlideHeadersAdminController : Controller
    {
        private ChanhThuStoreContext db = new ChanhThuStoreContext();

        // GET: Admin/SlideHeadersAdmin
        public ActionResult Index()
        {
            return View(db.SlideHeaders.ToList());
        }

        // GET: Admin/SlideHeadersAdmin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SlideHeader slideHeader = db.SlideHeaders.Find(id);
            if (slideHeader == null)
            {
                return HttpNotFound();
            }
            return View(slideHeader);
        }

        // GET: Admin/SlideHeadersAdmin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/SlideHeadersAdmin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaSliderHeader,HinhSlideHeader,MoTaSliderHeader")] SlideHeader slideHeader)
        {
            if (ModelState.IsValid)
            {
                db.SlideHeaders.Add(slideHeader);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(slideHeader);
        }

        // GET: Admin/SlideHeadersAdmin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SlideHeader slideHeader = db.SlideHeaders.Find(id);
            if (slideHeader == null)
            {
                return HttpNotFound();
            }
            return View(slideHeader);
        }

        // POST: Admin/SlideHeadersAdmin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaSliderHeader,HinhSlideHeader,MoTaSliderHeader")] SlideHeader slideHeader)
        {
            if (ModelState.IsValid)
            {
                db.Entry(slideHeader).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(slideHeader);
        }

        // GET: Admin/SlideHeadersAdmin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SlideHeader slideHeader = db.SlideHeaders.Find(id);
            if (slideHeader == null)
            {
                return HttpNotFound();
            }
            return View(slideHeader);
        }

        // POST: Admin/SlideHeadersAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SlideHeader slideHeader = db.SlideHeaders.Find(id);
            db.SlideHeaders.Remove(slideHeader);
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
