using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Grocery_Store.Models
{
    public partial class GroceryStoreDB : DbContext
    {
        public GroceryStoreDB()
            : base("name=GroceryStoreDB")
        {
        }

        public virtual DbSet<ANH> ANHs { get; set; }
        public virtual DbSet<BINHLUAN> BINHLUANs { get; set; }
        public virtual DbSet<CHITIETDH> CHITIETDHs { get; set; }
        public virtual DbSet<DANHMUC> DANHMUCs { get; set; }
        public virtual DbSet<DONHANG> DONHANGs { get; set; }
        public virtual DbSet<LOAISP> LOAISPs { get; set; }
        public virtual DbSet<PHUONGXA> PHUONGXAs { get; set; }
        public virtual DbSet<QUANHUYEN> QUANHUYENs { get; set; }
        public virtual DbSet<SANPHAM> SANPHAMs { get; set; }
        public virtual DbSet<TAIKHOAN> TAIKHOANs { get; set; }
        public virtual DbSet<TINHTHANH> TINHTHANHs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ANH>()
                .HasMany(e => e.SANPHAMs)
                .WithRequired(e => e.ANH)
                .HasForeignKey(e => e.AnhBia)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ANH>()
                .HasMany(e => e.TAIKHOANs)
                .WithOptional(e => e.ANH)
                .HasForeignKey(e => e.Avatar);

            modelBuilder.Entity<ANH>()
                .HasMany(e => e.SANPHAMs1)
                .WithMany(e => e.ANHs)
                .Map(m => m.ToTable("THUVIENANHSP").MapLeftKey("MaANH").MapRightKey("MaSP"));

            modelBuilder.Entity<BINHLUAN>()
                .HasMany(e => e.BINHLUAN1)
                .WithOptional(e => e.BINHLUAN2)
                .HasForeignKey(e => e.PhanHoi);

            modelBuilder.Entity<DANHMUC>()
                .HasMany(e => e.LOAISPs)
                .WithOptional(e => e.DANHMUC)
                .HasForeignKey(e => e.ID_DanhMuc);

            modelBuilder.Entity<DONHANG>()
                .HasMany(e => e.CHITIETDHs)
                .WithRequired(e => e.DONHANG)
                .HasForeignKey(e => e.MaDH)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<LOAISP>()
                .HasMany(e => e.SANPHAMs)
                .WithRequired(e => e.LOAISP)
                .HasForeignKey(e => e.MaLoai)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PHUONGXA>()
                .HasMany(e => e.DONHANGs)
                .WithRequired(e => e.PHUONGXA)
                .HasForeignKey(e => e.MaPhuong)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<QUANHUYEN>()
                .HasMany(e => e.DONHANGs)
                .WithRequired(e => e.QUANHUYEN)
                .HasForeignKey(e => e.MaQuan)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<QUANHUYEN>()
                .HasMany(e => e.PHUONGXAs)
                .WithRequired(e => e.QUANHUYEN1)
                .HasForeignKey(e => e.QuanHuyen)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SANPHAM>()
                .HasMany(e => e.BINHLUANs)
                .WithRequired(e => e.SANPHAM)
                .HasForeignKey(e => e.MaSP)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SANPHAM>()
                .HasMany(e => e.CHITIETDHs)
                .WithRequired(e => e.SANPHAM)
                .HasForeignKey(e => e.MaSP)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TAIKHOAN>()
                .HasMany(e => e.BINHLUANs)
                .WithRequired(e => e.TAIKHOAN)
                .HasForeignKey(e => e.MaTK)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TAIKHOAN>()
                .HasMany(e => e.DONHANGs)
                .WithRequired(e => e.TAIKHOAN)
                .HasForeignKey(e => e.KhachHang)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TINHTHANH>()
                .HasMany(e => e.DONHANGs)
                .WithRequired(e => e.TINHTHANH)
                .HasForeignKey(e => e.MaTP)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TINHTHANH>()
                .HasMany(e => e.QUANHUYENs)
                .WithRequired(e => e.TINHTHANH1)
                .HasForeignKey(e => e.TinhThanh)
                .WillCascadeOnDelete(false);
        }

        public System.Data.Entity.DbSet<Grocery_Store.Models.User> Users { get; set; }
    }
}
