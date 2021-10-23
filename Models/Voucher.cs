namespace ChanhThu_Store.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Voucher")]
    public partial class Voucher
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Voucher()
        {
            AspNetUsers = new HashSet<AspNetUser>();
        }

        [Key]
        [StringLength(10)]
        public string MaVoucher { get; set; }

        [Required]
        [StringLength(50)]
        public string TenVoucher { get; set; }

        public int GiaTriGiam { get; set; }

        public int DiemDoi { get; set; }

        [Column(TypeName = "date")]
        public DateTime HanSuDung { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AspNetUser> AspNetUsers { get; set; }
    }
}
