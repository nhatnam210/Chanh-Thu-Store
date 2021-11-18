namespace ChanhThu_Store.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChiTietVoucher")]
    public partial class ChiTietVoucher
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(10)]
        public string MaVoucher { get; set; }

        [Key]
        [Column(Order = 1)]
        public string MaKhachHang { get; set; }

        public bool? TinhTrang { get; set; }

        public int? SoLuong { get; set; }

        public virtual AspNetUser AspNetUser { get; set; }

        public virtual Voucher Voucher { get; set; }
    }
}
