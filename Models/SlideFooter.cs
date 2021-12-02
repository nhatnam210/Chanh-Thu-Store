namespace ChanhThu_Store.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SlideFooter")]
    public partial class SlideFooter
    {
        [Key]
        public int MaSliderFoot { get; set; }

        [StringLength(255, ErrorMessage = "Đường dẫn hình không được vượt quá {0} ký tự!")]
        public string HinhSlideFooter { get; set; }
    }
}
