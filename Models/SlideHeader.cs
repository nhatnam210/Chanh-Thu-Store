namespace ChanhThu_Store.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SlideHeader")]
    public partial class SlideHeader
    {
        [Key]
        public int MaSliderHeader { get; set; }

        [StringLength(255, ErrorMessage = "Đường dẫn hình không được vượt quá {0} ký tự!")]
        public string HinhSlideHeader { get; set; }

        [StringLength(255, ErrorMessage = "Mô tả không được vượt quá {0} ký tự!")]
        public string MoTaSliderHeader { get; set; }
    }
}
