namespace Grocery_Store.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TAIKHOAN")]
    public partial class TAIKHOAN
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TAIKHOAN()
        {
            BINHLUANs = new HashSet<BINHLUAN>();
            DONHANGs = new HashSet<DONHANG>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(30)]
        public string HoTen { get; set; }

        [Required]
        [StringLength(30)]
        public string TenTK { get; set; }

        [Required]
        [StringLength(50)]
        public string MatKhau { get; set; }

        public DateTime NgayCap { get; set; }

        public bool GioiTinh { get; set; }

        [StringLength(15)]
        public string SDT { get; set; }

        [StringLength(30)]
        public string Email { get; set; }

        public bool DuocSD { get; set; }

        public string GhiChu { get; set; }

        public int? Avatar { get; set; }

        [Required]
        [StringLength(20)]
        public string VaiTro { get; set; }

        public virtual ANH ANH { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BINHLUAN> BINHLUANs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DONHANG> DONHANGs { get; set; }
    }
}
