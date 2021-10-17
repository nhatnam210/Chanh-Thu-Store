using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace ChanhThu_Store.Models
{
    public partial class ChanhThuStoreContext : DbContext
    {
        public ChanhThuStoreContext()
            : base("name=ChanhThuStoreContext")
        {
        }

        public virtual DbSet<Banner> Banners { get; set; }
        public virtual DbSet<ChiTietHoaDon> ChiTietHoaDons { get; set; }
        public virtual DbSet<ChiTietVoucher> ChiTietVouchers { get; set; }
        public virtual DbSet<DanhMuc> DanhMucs { get; set; }
        public virtual DbSet<DanhMucCon> DanhMucCons { get; set; }
        public virtual DbSet<HoaDon> HoaDons { get; set; }
        public virtual DbSet<NhaSanXuat> NhaSanXuats { get; set; }
        public virtual DbSet<SanPham> SanPhams { get; set; }
        public virtual DbSet<SlideFooter> SlideFooters { get; set; }
        public virtual DbSet<SlideHeader> SlideHeaders { get; set; }
        public virtual DbSet<ThongTinCuaHang> ThongTinCuaHangs { get; set; }
        public virtual DbSet<TuongTac> TuongTacs { get; set; }
        public virtual DbSet<Voucher> Vouchers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ThongTinCuaHang>()
                .Property(e => e.SDT)
                .IsUnicode(false);
        }
    }
}
