using ChanhThu_Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Text.RegularExpressions;
using System.Text;
using Microsoft.AspNet.Identity;

namespace ChanhThu_Store.Controllers
{
    public class TimKiemController : Controller
    {
        public ChanhThuStoreContext db = new ChanhThuStoreContext();

        // GET: TimKiem
        public ActionResult Index(string tukhoa, string sapxep)
        {
            IQueryable<SanPham> sanpham = null;
            var userID = User.Identity.GetUserId();
            ViewBag.TimKiem = tukhoa;
            ViewBag.SapXep = sapxep;

            sanpham = db.SanPhams.Select(s => s);

            if (!String.IsNullOrEmpty(tukhoa) && tukhoa.Trim().Length > 0)
            {
                tukhoa = tukhoa.Trim();
                var tuKhoaUnsign = ConvertToUnSignNoneSpace(tukhoa);
                    sanpham = sanpham.Where(delegate (SanPham s)
                    {
                        if (ConvertToUnSignNoneSpace(s.TenSanPham).IndexOf(tuKhoaUnsign, StringComparison.CurrentCultureIgnoreCase) >= 0
                            || ConvertToUnSignNoneSpace(s.Mota).IndexOf(tuKhoaUnsign, StringComparison.CurrentCultureIgnoreCase) >= 0
                            || ConvertToUnSignNoneSpace(s.DanhMucCon.TenDanhMucCon).IndexOf(tuKhoaUnsign, StringComparison.CurrentCultureIgnoreCase) >= 0
                            || ConvertToUnSignNoneSpace(s.DanhMucCon.DanhMuc.TenDanhMuc).IndexOf(tuKhoaUnsign, StringComparison.CurrentCultureIgnoreCase) >= 0)
                            return true;
                        else
                            return false;
                    }).AsQueryable();
            }
            //trường hợp không nhập tìm kiếm
            else
            {
                sanpham = sanpham.Take(0);
            }

            /*Check yêu thích*/
            foreach (SanPham item in sanpham)
            {
                if (userID != null)
                {
                    item.isLogin = true;

                    TuongTac find = db.TuongTacs.FirstOrDefault(p => p.MaSanPham == item.MaSanPham && p.MaKhachHang == userID);
                    if (find != null)
                        item.isLiked = true;
                }
            }

            //Sắp xếp
            switch (sapxep)
            {
                case "mac-dinh":
                    sanpham = sanpham.OrderBy(s => s.MaSanPham);
                    break;
                case "ten-A-Z":
                    sanpham = sanpham.OrderBy(s => s.TenSanPham);
                    break;
                case "ten-Z-A":
                    sanpham = sanpham.OrderByDescending(s => s.TenSanPham);
                    break;
                case "gia-thap-cao":
                    sanpham = sanpham.OrderBy(s => s.Gia);
                    break;
                case "gia-cao-thap":
                    sanpham = sanpham.OrderByDescending(s => s.Gia);
                    break;
                default:
                    sanpham = sanpham.OrderByDescending(s => s.MaSanPham);
                    break;
            }
            //int pageSize = 9;
            //int pageNumber = (trang ?? 1);

            ViewBag.listsp = sanpham;
            //return View(sanpham.ToPagedList(pageNumber, pageSize));
            return View(sanpham.ToList());
        }

        //hàm đổi tiếng việt có dấu sang không dấu và loại bỏ tất cả khoảng trắng trong chuỗi
        public static string ConvertToUnSignNoneSpace(string input)
        {
            input = input.Trim();
            for (int i = 0x20; i < 0x30; i++)
            {
                input = input.Replace(((char)i).ToString(), " ");
            }
            Regex regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");
            string str = input.Normalize(NormalizationForm.FormD);
            string str2 = regex.Replace(str, string.Empty).Replace('đ', 'd').Replace('Đ', 'D');
            while (str2.IndexOf("?") >= 0)
            {
                str2 = str2.Remove(str2.IndexOf("?"), 1);
            }
            str2 = Regex.Replace(str2, @"\s+", "");
            return str2;
        }

        //hàm đổi tiếng việt có dấu sang không dấu vã giữ nguyên các khoảng trắng trong chuỗi
        public static string ConvertToUnSign(string input)
        {
            input = input.Trim();
            for (int i = 0x20; i < 0x30; i++)
            {
                input = input.Replace(((char)i).ToString(), " ");
            }
            Regex regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");
            string str = input.Normalize(NormalizationForm.FormD);
            string str2 = regex.Replace(str, string.Empty).Replace('đ', 'd').Replace('Đ', 'D');
            while (str2.IndexOf("?") >= 0)
            {
                str2 = str2.Remove(str2.IndexOf("?"), 1);
            }

            return str2;
        }
    }
}