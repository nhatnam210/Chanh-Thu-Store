using ChanhThu_Store.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class TimKiemController : Controller
    {
        public ChanhThuStoreContext db = new ChanhThuStoreContext();
        // GET: TimKiem
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
            var sanpham = from s in db.SanPhams
                             select s;
            if (!String.IsNullOrEmpty(timkiem))
            {
                sanpham = sanpham.Where(s => s.TenSanPham.Contains(timkiem));
                //|| s.author.Contains(timkiem)

            }
            //sắp xếp 
            switch (sapxep)
            {
                
                default:
                    sanpham = sanpham.OrderBy(s => s.MaDanhMucCon);
                    break;
            }
            //var articles = db.Articles.Include(a => a.Cetegory);
            int pageSize = 1;
            int pageNumber = (trang ?? 1);
            return View(sanpham.ToPagedList(pageNumber, pageSize));
        }
    }
}