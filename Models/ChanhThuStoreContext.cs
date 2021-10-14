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

        public virtual DbSet<BinhLuan> BinhLuans { get; set; }
        public virtual DbSet<ChiTietHoaDon> ChiTietHoaDons { get; set; }
        public virtual DbSet<DanhMuc> DanhMucs { get; set; }
        public virtual DbSet<DanhMucCon> DanhMucCons { get; set; }
        public virtual DbSet<HoaDon> HoaDons { get; set; }
        public virtual DbSet<KhachHang> KhachHangs { get; set; }
        public virtual DbSet<NhaSanXuat> NhaSanXuats { get; set; }
        public virtual DbSet<SanPham> SanPhams { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<Voucher> Vouchers { get; set; }
        public virtual DbSet<ThongTinCuaHang> ThongTinCuaHangs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChiTietHoaDon>()
                .Property(e => e.MaHoaDon)
                .IsUnicode(false);

            modelBuilder.Entity<DanhMuc>()
                .HasMany(e => e.DanhMucCons)
                .WithRequired(e => e.DanhMuc)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DanhMuc>()
                .HasMany(e => e.SanPhams)
                .WithRequired(e => e.DanhMuc)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DanhMucCon>()
                .HasMany(e => e.SanPhams)
                .WithRequired(e => e.DanhMucCon)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HoaDon>()
                .Property(e => e.MaHoaDon)
                .IsUnicode(false);

            modelBuilder.Entity<HoaDon>()
                .HasMany(e => e.ChiTietHoaDons)
                .WithRequired(e => e.HoaDon)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<KhachHang>()
                .Property(e => e.SDT)
                .IsUnicode(false);

            modelBuilder.Entity<KhachHang>()
                .HasMany(e => e.BinhLuans)
                .WithRequired(e => e.KhachHang)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<KhachHang>()
                .HasMany(e => e.HoaDons)
                .WithRequired(e => e.KhachHang)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<KhachHang>()
                .HasMany(e => e.Vouchers)
                .WithMany(e => e.KhachHangs)
                .Map(m => m.ToTable("ChiTietVoucher").MapLeftKey("MaKhachhang").MapRightKey("MaVoucher"));

            modelBuilder.Entity<KhachHang>()
                .HasMany(e => e.SanPhams)
                .WithMany(e => e.KhachHangs)
                .Map(m => m.ToTable("SanPhamYeuThich").MapLeftKey("MaKhachHang").MapRightKey("MaSanPham"));

            modelBuilder.Entity<NhaSanXuat>()
                .HasMany(e => e.SanPhams)
                .WithRequired(e => e.NhaSanXuat)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SanPham>()
                .Property(e => e.TinhTrang)
                .IsFixedLength();

            modelBuilder.Entity<SanPham>()
                .HasMany(e => e.BinhLuans)
                .WithRequired(e => e.SanPham)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SanPham>()
                .HasMany(e => e.ChiTietHoaDons)
                .WithRequired(e => e.SanPham)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ThongTinCuaHang>()
                .Property(e => e.SDT)
                .IsUnicode(false);
        }
    }
}
