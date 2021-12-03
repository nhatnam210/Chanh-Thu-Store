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
    public class SlideFootersAdminController : Controller
    {
        private ChanhThuStoreContext db = new ChanhThuStoreContext();

        // GET: Admin/SlideFootersAdmin
        public ActionResult Index()
        {
            return View(db.SlideFooters.ToList());
        }


        // GET: Admin/SlideFootersAdmin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/SlideFootersAdmin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaSliderFoot,HinhSlideFooter")] SlideFooter slideFooter)
        {
            if (ModelState.IsValid)
            {
                db.SlideFooters.Add(slideFooter);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(slideFooter);
        }

        // GET: Admin/SlideFootersAdmin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SlideFooter slideFooter = db.SlideFooters.Find(id);
            if (slideFooter == null)
            {
                return HttpNotFound();
            }
            return View(slideFooter);
        }

        // POST: Admin/SlideFootersAdmin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,HinhSlideFooter")] SlideFooter slideFooter)
        {
            if (ModelState.IsValid)
            {
                db.Entry(slideFooter).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(slideFooter);
        }

        // GET: Admin/SlideFootersAdmin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SlideFooter slideFooter = db.SlideFooters.Find(id);
            if (slideFooter == null)
            {
                return HttpNotFound();
            }
            return View(slideFooter);
        }

        // POST: Admin/SlideFootersAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SlideFooter slideFooter = db.SlideFooters.Find(id);
            db.SlideFooters.Remove(slideFooter);
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
