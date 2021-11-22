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
        public int MaBinhLuan { get; set; }

        [Required]
        [StringLength(128)]
        public string MaKhachHang { get; set; }

        [Required]
        [StringLength(10)]
        public string MaSanPham { get; set; }

        [Required]
        public string NoiDungBinhLuan { get; set; }

        [Column(TypeName = "date")]
        public DateTime NgayBinhLuan { get; set; }

        public virtual AspNetUser AspNetUser { get; set; }

        public virtual SanPham SanPham { get; set; }
    }
}
