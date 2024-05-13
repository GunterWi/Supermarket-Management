namespace Grocery_Store.Models
{
    using Grocery_Store.Areas.Admin.Data;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

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
        [HiddenInput]
        public int ID { get; set; }

        public int MaLoai { get; set; }

        [Required(ErrorMessage = "Tên sản phẩm không được để trống")]
        [StringLength(40)]
        public string TenSP { get; set; }

        [Required(ErrorMessage = "Tên đường dẫn không được để trống")]
        [StringLength(40)]
        [UniquePath(typeof(SANPHAM))]
        public string TenDuongDan { get; set; }

        [StringLength(100)]
        public string TomTat { get; set; }

        public DateTime NgayDangSP { get; set; }

        [Required(ErrorMessage = "Giá bán không được để trống")]
        public int GiaBan { get; set; }

        public int? GiaKM { get; set; }

        [StringLength(10)]
        public string Dvt { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Số lượng không được nhỏ hơn 0")]
        [Required(ErrorMessage = "Số lượng không được để trống")]
        public int SoLuong { get; set; }

        [Required(ErrorMessage = "Ảnh bìa không được để trống")]
        public int AnhBia { get; set; }

        [Required(ErrorMessage = "Nội dung sản phẩm không được để trống")]
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
