namespace ChanhThu_Store.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HoaDon")]
    public partial class HoaDon
    {
        [Key]
        public int MaHoaDon { get; set; }

        [Required]
        [StringLength(128)]
        public string MaKhachHang { get; set; }

        [Column(TypeName = "date")]
        public DateTime NgayLap { get; set; }

        [StringLength(10)]
        public string MaVoucher { get; set; }

        public int TongTien { get; set; }

        [Required]
        [StringLength(30)]
        public string Ten { get; set; }

        [Required]
        public string SDT { get; set; }

        [Required]
        [StringLength(100)]
        public string DiaChi { get; set; }

        [StringLength(256)]
        public string Email { get; set; }

        public int Ship { get; set; }
        public virtual AspNetUser AspNetUser { get; set; }
    }
}
