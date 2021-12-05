using ChanhThu_Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace ChanhThu_Store.Controllers
{
    public class TimKiemController : Controller
    {
        public ChanhThuStoreContext db = new ChanhThuStoreContext();
        // GET: TimKiem
        public ActionResult Index(string sapxep, string loc, string timkiem)
        {
            IQueryable<SanPham> sanphams = null;
            ViewBag.Loc = timkiem;
            //tìm kiếm
            sanphams = from s in db.SanPhams
                           select s;
            if (!String.IsNullOrEmpty(timkiem))
            {
                sanphams = sanphams.Where(s => s.TenSanPham.Contains(timkiem));
                //|| s.author.Contains(searchString)
                //|| s.description.Contains(searchString)
                //|| s.content1.Contains(searchString));
               
            }
            ViewBag.listsp = sanphams;

            return View(sanphams);
        }
    }
}