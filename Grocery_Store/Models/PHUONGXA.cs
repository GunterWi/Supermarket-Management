namespace Grocery_Store.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PHUONGXA")]
    public partial class PHUONGXA
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PHUONGXA()
        {
            DONHANGs = new HashSet<DONHANG>();
        }

        public int ID { get; set; }

        [StringLength(30)]
        public string Name { get; set; }

        public int QuanHuyen { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DONHANG> DONHANGs { get; set; }

        public virtual QUANHUYEN QUANHUYEN1 { get; set; }
    }
}
