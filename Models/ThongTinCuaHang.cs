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
        public int Id { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "Tên cửa hàng không được vượt quá {0} ký tự!")]
        public string TenCuaHang { get; set; }

        [Required]
        [StringLength(10, ErrorMessage = "Số điện thạoi không được vượt quá {0} ký tự!")]
        public string SDT { get; set; }

        [StringLength(50, ErrorMessage = "Email không được vượt quá {0} ký tự!")]
        public string Email { get; set; }

        [StringLength(200, ErrorMessage = "Địa chỉ không được vượt quá {0} ký tự!")]
        public string DiaChi { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayThanhLap { get; set; }

        [StringLength(20, ErrorMessage = "Thời gian mở cửa không được vượt quá {0} ký tự!")]
        public string ThoiGianMoCua { get; set; }

        [StringLength(20, ErrorMessage = "Thời gian đóng cửa không được vượt quá {0} ký tự!")]
        public string ThoiGianDongCua { get; set; }

        public string LoiGioiThieu { get; set; }

        [StringLength(255, ErrorMessage = "Đường dẫn hình không được vượt quá {0} ký tự!")]
        public string HinhMinhHoa { get; set; }
    }
}
