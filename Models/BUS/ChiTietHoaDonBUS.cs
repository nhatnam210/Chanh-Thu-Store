using ChanhThuStorePoCo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChanhThu_Store.Models.BUS
{
    public class ChiTietHoaDonBUS
    {
        public static IEnumerable<ChiTietHoaDon> ChiTietHoaDon(int id)
        {
            var db = new ChanhThuStorePoCoDB();
            return db.Query<ChiTietHoaDon>("select * from ChiTietHoaDon where MaHoaDon = '" + id + "' ");
        }
    }
}