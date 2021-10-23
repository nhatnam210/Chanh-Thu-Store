using ChanhThu_Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChanhThu_Store.Models.DAO
{
    public class HoaDonDAO
    {
        private ChanhThuStoreContext db = new ChanhThuStoreContext();
        public int Insert(HoaDon hoadon)
        {
            db.HoaDons.Add(hoadon);
            db.SaveChanges();
            return hoadon.MaHoaDon;
        }
    }
}