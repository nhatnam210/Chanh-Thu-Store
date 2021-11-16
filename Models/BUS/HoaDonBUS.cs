using ChanhThuStorePoCo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChanhThu_Store.Models.BUS
{
    public class HoaDonBUS
    {
        public static IEnumerable<HoaDon> DanhSachHoaDon(string id)
        {
            var db = new ChanhThuStorePoCoDB();
            return db.Query<HoaDon>("select * from Hoadon where MaKhachHang =  '" + id + "'");
        }
    }
}