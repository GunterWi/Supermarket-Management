namespace Grocery_Store.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DONHANG")]
    public partial class DONHANG
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DONHANG()
        {
            CHITIETDHs = new HashSet<CHITIETDH>();
        }

        public int ID { get; set; }

        public int KhachHang { get; set; }

        [StringLength(70)]
        public string DcGiaoHang { get; set; }

        public int MaPhuong { get; set; }

        public int MaQuan { get; set; }

        public int MaTP { get; set; }

        public bool XuatHD { get; set; }

        public DateTime NgayDatHang { get; set; }

        public DateTime? NgayGiaoHang { get; set; }

        public bool? ThanhCong { get; set; }

        [StringLength(40)]
        public string DvVanChuyen { get; set; }

        [StringLength(20)]
        public string MaVanChuyen { get; set; }

        public int? PhiShip { get; set; }

        [StringLength(253)]
        public string GhiChuKhach { get; set; }

        [StringLength(253)]
        public string GhiChuShop { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CHITIETDH> CHITIETDHs { get; set; }

        public virtual TAIKHOAN TAIKHOAN { get; set; }

        public virtual PHUONGXA PHUONGXA { get; set; }

        public virtual QUANHUYEN QUANHUYEN { get; set; }

        public virtual TINHTHANH TINHTHANH { get; set; }
    }
}
