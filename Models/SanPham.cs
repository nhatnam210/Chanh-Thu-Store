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
            BinhLuans = new HashSet<BinhLuan>();
            ChiTietHoaDons = new HashSet<ChiTietHoaDon>();
            TuongTacs = new HashSet<TuongTac>();
        }

        [Key]
        [StringLength(10)]
        public string MaSanPham { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn danh mục con!")]
        [StringLength(6)]
        public string MaDanhMucCon { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn nhà sản xuất!")]
        [StringLength(6)]
        public string MaNhaSanXuat { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên sản phẩm!")]
        [StringLength(200)]
        public string TenSanPham { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập giá sản phẩm!")]
        public int Gia { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn hình cho sản phẩm!")]
        [StringLength(255, ErrorMessage = "Đường dẫn hình không được vượt quá {0} ký tự!")]
        public string HinhChinh { get; set; }

        [StringLength(255, ErrorMessage = "Đường dẫn hình không được vượt quá {0} ký tự!")]
        public string Hinh1 { get; set; }

        [StringLength(255, ErrorMessage = "Đường dẫn hình không được vượt quá {0} ký tự!")]
        public string Hinh2 { get; set; }

        public string Mota { get; set; }

        [StringLength(20, ErrorMessage = "Đơn vị tính không được vượt quá {0} ký tự!")]
        public string DonViTinh { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số lượng tồn kho!")]
        public int SoLuongTonKho { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số lượng đã bán!")]
        public int? SoLuongDaBan { get; set; }

        public int? LuotYeuThich { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn ngày sản xuất!")]
        [Column(TypeName = "date")]
        public DateTime NgaySanXuat { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn hạn sử dụng!")]
        [Column(TypeName = "date")]
        public DateTime HanSuDung { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập điểm!")]
        public int Diem { get; set; }

        public bool TinhTrang { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BinhLuan> BinhLuans { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; }

        public virtual DanhMucCon DanhMucCon { get; set; }

        public virtual NhaSanXuat NhaSanXuat { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TuongTac> TuongTacs { get; set; }
        public bool isLogin = false;
        public bool isLiked = false;
    }
}
