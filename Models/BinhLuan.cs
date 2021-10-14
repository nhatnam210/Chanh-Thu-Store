namespace ChanhThu_Store.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BinhLuan")]
    public partial class BinhLuan
    {
        [Key]
        [Column(Order = 0)]
        public string MaKhachHang { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string MaSanPham { get; set; }

        [Required]
        [StringLength(255)]
        public string NoiDung { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayBinhLuan { get; set; }

        public virtual KhachHang KhachHang { get; set; }

        public virtual SanPham SanPham { get; set; }
    }
}
