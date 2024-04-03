namespace Grocery_Store.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CHITIETDH")]
    public partial class CHITIETDH
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaDH { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaSP { get; set; }

        public int SoLuong { get; set; }

        public int GiaBan { get; set; }

        public int? DanhGia { get; set; }

        public virtual DONHANG DONHANG { get; set; }

        public virtual SANPHAM SANPHAM { get; set; }
    }
}
