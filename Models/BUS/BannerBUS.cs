using ChanhThuStorePoCo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChanhThu_Store.Models.BUS
{
    public class BannerBUS
    {
        public static IEnumerable<Banner> DanhSachBanner()
        {
            var db = new ChanhThuStorePoCoDB();
            return db.Query<Banner>("Select * from Banner");
        }
    }
}