namespace ChanhThu_Store.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ThongTinCuaHang")]
    public partial class ThongTinCuaHang
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(30)]
        public string TenCuHang { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string SDT { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(100)]
        public string DiaChi { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayThanhLap { get; set; }

        [StringLength(20)]
        public string ThoiGianMoCua { get; set; }

        [StringLength(20)]
        public string ThoiGianDongCua { get; set; }

        [StringLength(255)]
        public string LoiGioiThieu { get; set; }

        [StringLength(255)]
        public string HinhMinhHoa { get; set; }
    }
}
