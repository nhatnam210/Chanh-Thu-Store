using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChanhThu_Store.Models.DAO
{
    public class SanPhamModel
    {
        public SanPham ViewDetail (string id)
        {
             ChanhThuStoreContext db = new ChanhThuStoreContext();

            return db.SanPhams.Find(id);
        }
    }
}