
using ChanhThuStorePoCo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChanhThu_Store.Models.BUS
{
    public class SanPhamBUS
    {
        public static IEnumerable<SanPham> DanhSach()
        {
            var db = new ChanhThuStorePoCoDB();
            return db.Query<SanPham>("Select * from SanPham where TinhTrang = 1");
        }
      
        public static IEnumerable<SanPham> DanhSachSanPhamtheoDanhMucMacDinh()
        {
            var db = new ChanhThuStorePoCoDB();
            return db.Query<SanPham>("select * from SanPham where MaDanhMuc = (select top 1 MaDanhMuc from DanhMuc)");
        }

        public static IEnumerable<SanPham> DanhSachSanPhamtheoDanhMuc(String id)
        {
            var db = new ChanhThuStorePoCoDB();
            return db.Query<SanPham>("select * from SanPham where MaDanhMuc = '" + id + "' ");
        }

        public static IEnumerable<SanPham> DanhSachSanPhamtheoDanhMucCon(string id)
        {
            var db = new ChanhThuStorePoCoDB();
            return db.Query<SanPham>("select * from SanPham where MaDanhMucCon = '" + id + "' ");
        }
    }
}