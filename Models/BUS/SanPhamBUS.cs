
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

        //public static IEnumerable<SanPham> DanhSachSanPhamTheoDanhMucConMacDinh()
        //{
        //    var db = new ChanhThuStorePoCoDB();
        //    return db.Query<SanPham>("select * from SanPham where MaDanhMucCon = (select top 1 MaDanhMucCon from DanhMucCon)");
        //}
        //public static IEnumerable<SanPham> DanhSachSanPhamTheoDanhMucCon(string id)
        //{
        //    var db = new ChanhThuStorePoCoDB();
        //    return db.Query<SanPham>("select * from SanPham where MaDanhMucCon = '"+ id +"' ");
        //}

        //public static IEnumerable<SanPham> DanhSachSanPhamTheoDanhMuc(String id)
        //{
        //    var db = new ChanhThuStorePoCoDB();
        //    return db.Query<SanPham>("select * from SanPham where MaDanhMucCon in (select MaDanhMucCon from DanhMucCon where MaDanhMuc = '"+ id +"')");
        //}

       
        public static IEnumerable<SanPham> Top3Sanphammoi()
        {
            var db = new ChanhThuStorePoCoDB();
            return db.Query<SanPham>("select top 3 * from Sanpham order by Masanpham desc");
        }
    }
}