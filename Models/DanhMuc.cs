namespace ChanhThu_Store.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DanhMuc")]
    public partial class DanhMuc
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DanhMuc()
        {
            DanhMucCons = new HashSet<DanhMucCon>();
        }

        [Key]
        [StringLength(5)]
        public string MaDanhMuc { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên danh mục!")]
        [StringLength(20, ErrorMessage = "Tên danh mục không được vượt quá {0} ký tự!")]
        public string TenDanhMuc { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DanhMucCon> DanhMucCons { get; set; }
        public int soDMC = 0;
    }
}
