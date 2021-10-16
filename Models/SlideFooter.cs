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
        [StringLength(255)]
        public string HinhSlideFooter1 { get; set; }

        [StringLength(255)]
        public string HinhSlideFooter2 { get; set; }

        [StringLength(255)]
        public string HinhSlideFooter3 { get; set; }

        [StringLength(255)]
        public string HinhSlideFooter4 { get; set; }

        [StringLength(255)]
        public string HinhSlideFooter5 { get; set; }
    }
}
