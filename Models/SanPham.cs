namespace ChanhThu_Store.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SanPham")]
    public partial class SanPham
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SanPham()
        {
            ChiTietHoaDons = new HashSet<ChiTietHoaDon>();
            TuongTacs = new HashSet<TuongTac>();
        }

        [Key]
        [StringLength(10)]
        public string MaSanPham { get; set; }

        [Required]
        [StringLength(6)]
        public string MaDanhMucCon { get; set; }

        [Required]
        [StringLength(6)]
        public string MaNhaSanXuat { get; set; }

        [Required]
        [StringLength(50)]
        public string TenSanPham { get; set; }

        public int Gia { get; set; }

        [Required]
        [StringLength(255)]
        public string HinhChinh { get; set; }

        [StringLength(255)]
        public string Hinh1 { get; set; }

        [StringLength(255)]
        public string Hinh2 { get; set; }

        public string Mota { get; set; }

        public int SoLuongTonKho { get; set; }

        public int SoLuongDaBan { get; set; }

        public int? LuotYeuThich { get; set; }

        [Column(TypeName = "date")]
        public DateTime NgaySanXuat { get; set; }

        [Column(TypeName = "date")]
        public DateTime HanSuDung { get; set; }

        public int Diem { get; set; }

        public bool TinhTrang { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; }

        public virtual DanhMucCon DanhMucCon { get; set; }

        public virtual NhaSanXuat NhaSanXuat { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TuongTac> TuongTacs { get; set; }
    }
}
