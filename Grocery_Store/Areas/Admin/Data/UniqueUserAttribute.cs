using Grocery_Store.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Core.Metadata.Edm;
using System.IO;
using System.Linq;
using System.Web;
using System.Data.Entity;


namespace Grocery_Store.Areas.Admin.Data
{
    public class UniqueUserAttribute : ValidationAttribute
    {
        private readonly string _propertyName;

        public UniqueUserAttribute(string propertyName)
        {
            _propertyName = propertyName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            using (GroceryStoreDB db = new GroceryStoreDB())
            {
                var change = false;
                var entity = validationContext.ObjectInstance as TAIKHOAN;
                if (value != null)
                {
                    if (validationContext.DisplayName.Equals("TenTK"))
                    {
                        change = db.TAIKHOANs.Any(e => e.TenTK == value.ToString() && e.ID != entity.ID);
                    }
                    if (validationContext.DisplayName.Equals("Email"))
                    {
                        change = db.TAIKHOANs.Any(e => e.Email == value.ToString() && e.ID != entity.ID);
                    }
                    if (validationContext.DisplayName.Equals("SDT"))
                    {
                        change = db.TAIKHOANs.Any(e => e.SDT == value.ToString() && e.ID != entity.ID);
                    }
                    if (change)
                    {
                        if (_propertyName.Equals("TenTK"))
                        {
                            return new ValidationResult($"Tên tài khoản đã tồn tại");
                        }
                        return new ValidationResult($"{_propertyName} đã tồn tại");
                    }
                }
            }
            return ValidationResult.Success;
        }

    }

}