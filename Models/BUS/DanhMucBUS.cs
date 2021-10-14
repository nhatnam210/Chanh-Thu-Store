using ChanhThuStorePoCo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChanhThu_Store.Models.BUS
{
    public class DanhMucBUS
    {
        public static IEnumerable<DanhMuc> DanhSach()
        {
            var db = new ChanhThuStorePoCoDB();
            return db.Query<DanhMuc>("select * from DanhMuc ");
        }

        public static IEnumerable<DanhMucCon> DanhMucCon(String id)
        {
            var db = new ChanhThuStorePoCoDB();
            return db.Query<DanhMucCon>("select * from DanhMucCon where MaDanhMuc = '" + id + "'");
        }

        public static IEnumerable<DanhMucCon> DanhSachDMC()
        {
            var db = new ChanhThuStorePoCoDB();
            return db.Query<DanhMucCon>("select * from DanhMucCon ");
        }
    }
}