namespace Grocery_Store.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BINHLUAN")]
    public partial class BINHLUAN
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BINHLUAN()
        {
            BINHLUAN1 = new HashSet<BINHLUAN>();
        }

        public int ID { get; set; }

        public int MaSP { get; set; }

        public int MaTK { get; set; }

        public string NoiDung { get; set; }

        public int? PhanHoi { get; set; }

        public virtual SANPHAM SANPHAM { get; set; }

        public virtual TAIKHOAN TAIKHOAN { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BINHLUAN> BINHLUAN1 { get; set; }

        public virtual BINHLUAN BINHLUAN2 { get; set; }
    }
}
