using ChanhThu_Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Text.RegularExpressions;
using System.Text;

namespace ChanhThu_Store.Controllers
{
    public class TimKiemController : Controller
    {
        public ChanhThuStoreContext db = new ChanhThuStoreContext();

        // GET: TimKiem
        public ActionResult Index(string timkiem)
        {
            IQueryable<SanPham> sanpham = null;
            ViewBag.Loc = timkiem;

            sanpham = db.SanPhams.Select(s => s);

            if (!String.IsNullOrEmpty(timkiem))
            {
                timkiem = timkiem.Trim();
                var timkiemUnsign = ConvertToUnSignNoneSpace(timkiem);
                    sanpham = sanpham.Where(delegate (SanPham s)
                    {
                        if (ConvertToUnSignNoneSpace(s.TenSanPham).IndexOf(timkiemUnsign, StringComparison.CurrentCultureIgnoreCase) >= 0
                            || ConvertToUnSignNoneSpace(s.Mota).IndexOf(timkiemUnsign, StringComparison.CurrentCultureIgnoreCase) >= 0
                            || ConvertToUnSignNoneSpace(s.DanhMucCon.TenDanhMucCon).IndexOf(timkiemUnsign, StringComparison.CurrentCultureIgnoreCase) >= 0
                            || ConvertToUnSignNoneSpace(s.DanhMucCon.DanhMuc.TenDanhMuc).IndexOf(timkiemUnsign, StringComparison.CurrentCultureIgnoreCase) >= 0)
                            return true;
                        else
                            return false;
                    }).AsQueryable();
            }
            //trường hợp không nhập tìm kiếm
            else
            {
                sanpham = sanpham.Where(s => s.TenSanPham.Contains(timkiem));
            }
            ViewBag.listsp = sanpham;

            return View(sanpham);
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