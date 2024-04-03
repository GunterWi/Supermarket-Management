namespace Grocery_Store.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SANPHAM")]
    public partial class SANPHAM
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SANPHAM()
        {
            BINHLUANs = new HashSet<BINHLUAN>();
            CHITIETDHs = new HashSet<CHITIETDH>();
            ANHs = new HashSet<ANH>();
        }

        public int ID { get; set; }

        public int MaLoai { get; set; }

        [Required]
        [StringLength(40)]
        public string TenSP { get; set; }

        [Required]
        [StringLength(40)]
        public string TenDuongDan { get; set; }

        [StringLength(100)]
        public string TomTat { get; set; }

        public DateTime NgayDangSP { get; set; }

        public int GiaBan { get; set; }

        public int? GiaKM { get; set; }

        [Required]
        [StringLength(10)]
        public string Dvt { get; set; }

        public int SoLuong { get; set; }

        public int AnhBia { get; set; }

        public string NdSP { get; set; }

        public int LuotXem { get; set; }

        public int LuotMua { get; set; }

        public bool DangSP { get; set; }

        public virtual ANH ANH { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BINHLUAN> BINHLUANs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CHITIETDH> CHITIETDHs { get; set; }

        public virtual LOAISP LOAISP { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ANH> ANHs { get; set; }
    }
}
