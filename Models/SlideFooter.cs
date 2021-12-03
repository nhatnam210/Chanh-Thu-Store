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
        public int Id { get; set; }

        [StringLength(255, ErrorMessage = "Đường dẫn hình không được vượt quá {1} ký tự!")]
        public string HinhSlideFooter { get; set; }
    }
}
