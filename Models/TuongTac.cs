namespace ChanhThu_Store.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TuongTac")]
    public partial class TuongTac
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
        public string NoiDungBinhLuan { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayBinhLuan { get; set; }

        public bool? YeuThich { get; set; }

        public virtual AspNetUser AspNetUser { get; set; }

        public virtual SanPham SanPham { get; set; }
    }
}
