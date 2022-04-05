using ChanhThuStorePoCo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChanhThu_Store.Models.BUS
{
    public class SliderBUS
    {
        public static IEnumerable<SlideHeader> DanhSachSliderHeader()
        {
            var db = new ChanhThuStorePoCoDB();
            return db.Query<SlideHeader>("Select * from SlideHeader");
        }
        public static IEnumerable<SlideFooter> DanhSachSliderFooter()
        {
            var db = new ChanhThuStorePoCoDB();
            return db.Query<SlideFooter>("Select * from SlideFooter");
        }
    }
}