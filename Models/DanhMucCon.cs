namespace ChanhThu_Store.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DanhMucCon")]
    public partial class DanhMucCon
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DanhMucCon()
        {
            SanPhams = new HashSet<SanPham>();
        }

        [Key]
        [StringLength(6)]
        public string MaDanhMucCon { get; set; }

        [Required]
        [StringLength(5)]
        public string MaDanhMuc { get; set; }

        [Required]
        [StringLength(20)]
        public string TenDanhMucCon { get; set; }

        [StringLength(255)]
        public string Hinh { get; set; }

        public virtual DanhMuc DanhMuc { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [JsonIgnore]
        public virtual ICollection<SanPham> SanPhams { get; set; }
    }
}
