using Grocery_Store.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Linq.Expressions;


namespace Grocery_Store.Areas.Admin.Data
{
    public class UniquePathAttribute : ValidationAttribute
    {
        private readonly Type _entityType;
        public UniquePathAttribute(Type entityType)
        {
            _entityType = entityType;
        }
        /*protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            using (var db = new GroceryStoreDB())
            {
                var path = value as string;
                var exists=false;
                if (_entityType.Equals(typeof(SANPHAM)))
                {
                    exists = db.SANPHAMs.Any(e => e.TenDuongDan == path);
                }
                else
                {
                    exists = db.LOAISPs.Any(e => e.TenDuongDan == path);
                }
                if (exists)
                {
                    return new ValidationResult("Tên đường dẫn đã tồn tại.");
                }
            }

            return ValidationResult.Success;
        }*/
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            using (var db = new GroceryStoreDB())
            {
                var path = value as string;
                var entity = validationContext.ObjectInstance;

                var exists = false;

                if (_entityType == typeof(SANPHAM))
                {
                    var sanPham = entity as SANPHAM;
                    if (sanPham != null && sanPham.ID > 0 && sanPham.TenDuongDan == path)
                    {
                        // Trường hợp chỉnh sửa mà không thay đổi TenDuongDan
                        return ValidationResult.Success;
                    }

                    exists = db.SANPHAMs.Any(e => e.TenDuongDan == path);
                }
                else if (_entityType == typeof(LOAISP))
                {
                    var loaiSP = entity as LOAISP;
                    if (loaiSP != null && loaiSP.ID > 0 && loaiSP.TenDuongDan == path)
                    {
                        // Trường hợp chỉnh sửa mà không thay đổi TenDuongDan
                        return ValidationResult.Success;
                    }

                    exists = db.LOAISPs.Any(e => e.TenDuongDan == path);
                }

                if (exists)
                {
                    return new ValidationResult("Tên đường dẫn đã tồn tại.");
                }
            }

            return ValidationResult.Success;
        }


    }
}