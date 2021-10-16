namespace ChanhThu_Store.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Banner")]
    public partial class Banner
    {
        [Key]
        [StringLength(255)]
        public string HinhBanner1 { get; set; }

        [StringLength(255)]
        public string HinhBanner2 { get; set; }
    }
}
