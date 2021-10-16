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
        [StringLength(255)]
        public string HinhSlideHeader1 { get; set; }

        [StringLength(255)]
        public string HinhSlideHeader2 { get; set; }

        [StringLength(255)]
        public string HinhSlideHeader3 { get; set; }
    }
}
