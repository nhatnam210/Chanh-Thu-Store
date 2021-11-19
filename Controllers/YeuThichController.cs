using ChanhThu_Store.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ChanhThu_Store.Controllers
{
    public class YeuThichController : ApiController
    {
        [HttpPost]
        [Authorize]
        public IHttpActionResult Yeuthich(SanPham sanpham)
        {
            ChanhThuStoreContext context = new ChanhThuStoreContext();

            var userID = User.Identity.GetUserId();
            SanPham sanphamthich = context.SanPhams.Find(sanpham.MaSanPham);

            if (userID != null && sanphamthich != null)
            {
                if (context.TuongTacs.Any(p => p.MaKhachHang == userID && p.MaSanPham == sanpham.MaSanPham))
                {
                    //return BadRequest("The attendance already exist!");
                    sanphamthich.LuotYeuThich--;
                    if(sanphamthich.LuotYeuThich < 0)
                    {
                        sanphamthich.LuotYeuThich = 0;
                    }
                    context.TuongTacs.Remove(context.TuongTacs.SingleOrDefault(p => p.MaKhachHang == userID && p.MaSanPham == sanpham.MaSanPham));
                    context.SaveChanges();
                    return Ok("cancel");
                }
                var storage = new TuongTac() { MaSanPham = sanpham.MaSanPham, MaKhachHang = User.Identity.GetUserId(), YeuThich = true };
                
                sanphamthich.LuotYeuThich++;
                context.TuongTacs.Add(storage);
                context.SaveChanges();
                return Ok();
            }
            else
            {
                return null;
            }
        }
    }
}
