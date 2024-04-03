namespace Grocery_Store.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("QUANHUYEN")]
    public partial class QUANHUYEN
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public QUANHUYEN()
        {
            DONHANGs = new HashSet<DONHANG>();
            PHUONGXAs = new HashSet<PHUONGXA>();
        }

        public int ID { get; set; }

        [StringLength(30)]
        public string Name { get; set; }

        public int TinhThanh { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DONHANG> DONHANGs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PHUONGXA> PHUONGXAs { get; set; }

        public virtual TINHTHANH TINHTHANH1 { get; set; }
    }
}
