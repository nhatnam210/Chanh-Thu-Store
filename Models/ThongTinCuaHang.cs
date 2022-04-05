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

        [Required(ErrorMessage = "Vui lòng nhập tên cửa hàng!")]
        [StringLength(30, ErrorMessage = "Tên cửa hàng không được vượt quá {1} ký tự!")]
        public string TenCuaHang { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số điện thoại!")]
        [StringLength(11, ErrorMessage = "Số điện thọai không được vượt quá {1} ký tự!")]
        public string SDT { get; set; }

        [StringLength(50, ErrorMessage = "Email không được vượt quá {1} ký tự!")]
        public string Email { get; set; }

        [StringLength(200, ErrorMessage = "Địa chỉ không được vượt quá {1} ký tự!")]
        public string DiaChi { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập ngày thành lập!")]
        [Column(TypeName = "date")]
        public DateTime NgayThanhLap { get; set; }

        [StringLength(20, ErrorMessage = "Thời gian mở cửa không được vượt quá {1} ký tự!")]
        public string ThoiGianMoCua { get; set; }

        [StringLength(20, ErrorMessage = "Thời gian đóng cửa không được vượt quá {1} ký tự!")]
        public string ThoiGianDongCua { get; set; }

        public string LoiGioiThieu { get; set; }

        [StringLength(255, ErrorMessage = "Đường dẫn hình không được vượt quá {1} ký tự!")]
        public string HinhMinhHoa { get; set; }
    }
}
