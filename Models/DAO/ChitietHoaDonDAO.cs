using ChanhThu_Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChanhThu_Store.Models.DAO
{
    public class ChitietHoaDonDAO
    {
        private ChanhThuStoreContext db = new ChanhThuStoreContext();
        public bool Insert(ChiTietHoaDon chitiet)
        {
            try
            {
                db.ChiTietHoaDons.Add(chitiet);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}