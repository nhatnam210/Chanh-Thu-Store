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
        public ActionResult Index(string sapxep, string loc, string timkiem, int? trang)
        {

            ViewBag.Sapxep = sapxep;


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
            var articles = from s in db.SanPhams
                           select s;
            if (!String.IsNullOrEmpty(timkiem))
            {
                articles = articles.Where(s => s.TenSanPham.Contains(timkiem));
                //|| s.author.Contains(searchString)
                //|| s.description.Contains(searchString)
                //|| s.content1.Contains(searchString));
            }
            //sắp xếp 
            switch (sapxep)
            {

                default:
                    articles = articles.OrderBy(s => s.MaSanPham);
                    break;
            }
            //var articles = db.Articles.Include(a => a.Cetegory);
            int pageSize = 6;
            int pageNumber = (trang ?? 1);
            return View(articles.ToPagedList(pageNumber, pageSize));
        }
    }
}