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
            ChiTietVouchers = new HashSet<ChiTietVoucher>();
        }

        [Key]
        [StringLength(10)]
        public string MaVoucher { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Tên voucher không được vượt quá {1} ký tự!")]
        public string TenVoucher { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập giá trị giảm!")]
        public int GiaTriGiam { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập điểm đổi!")]
        public int DiemDoi { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập hạn sử dụng!")]
        [Column(TypeName = "date")]
        public DateTime HanSuDung { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietVoucher> ChiTietVouchers { get; set; }
    }
}
