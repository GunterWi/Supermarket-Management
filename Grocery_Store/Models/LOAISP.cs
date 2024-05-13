namespace Grocery_Store.Models
{
    using Grocery_Store.Areas.Admin.Data;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

    [Table("LOAISP")]
    public partial class LOAISP
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LOAISP()
        {
            SANPHAMs = new HashSet<SANPHAM>();
        }

        [HiddenInput]
        public int ID { get; set; }

        [Column("LoaiSP")]
        [StringLength(30)]
        [Required(ErrorMessage = "Tên thể loại không được để trống")]
        public string LoaiSP1 { get; set; }

        [StringLength(30)]
        [Required(ErrorMessage = "Tên đường dẫn không được để trống")]
        [UniquePath(typeof(LOAISP))]
        public string TenDuongDan { get; set; }

        [StringLength(50)]
        public string GhiChu { get; set; }

        public int? ID_DanhMuc { get; set; }

        public virtual DANHMUC DANHMUC { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SANPHAM> SANPHAMs { get; set; }
    }
}
