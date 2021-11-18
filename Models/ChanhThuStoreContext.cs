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

        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
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
            modelBuilder.Entity<AspNetRole>()
                .HasMany(e => e.AspNetUsers)
                .WithMany(e => e.AspNetRoles)
                .Map(m => m.ToTable("AspNetUserRoles").MapLeftKey("RoleId").MapRightKey("UserId"));

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AspNetUserClaims)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AspNetUserLogins)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.ChiTietVouchers)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.MaKhachHang)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.HoaDons)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.MaKhachHang)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.TuongTacs)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.MaKhachHang)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ThongTinCuaHang>()
                .Property(e => e.SDT)
                .IsUnicode(false);
        }
    }
}
