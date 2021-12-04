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

        [Required(ErrorMessage = "Vui lòng nhập tên loại sản phẩm!")]
        [StringLength(20, ErrorMessage = "Tên loại sản phẩm không được vượt quá {0} ký tự!")]
        public string TenDanhMucCon { get; set; }

        [StringLength(255, ErrorMessage = "Đường dẫn hình không được vượt quá {0} ký tự!")]
        public string Hinh { get; set; }

        public virtual DanhMuc DanhMuc { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [JsonIgnore]
        public virtual ICollection<SanPham> SanPhams { get; set; }
    }
}
