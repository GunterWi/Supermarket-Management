using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Grocery_Store.Areas.Admin.Data;
using Grocery_Store.Models;
using PagedList;

namespace Grocery_Store.Areas.Admin.Controllers
{
    [CustomAuthor(Roles = "Admin")]
    public class TaiKhoansController : Controller
    {
        private GroceryStoreDB db = new GroceryStoreDB();

        // Quản lý user 
        public ActionResult UserManagement(int? page)
        {
            int pageNum = (page ?? 1);
            List<TAIKHOAN> taiKhoans = db.TAIKHOANs.ToList();
            return View(taiKhoans.ToPagedList(pageNum, 10));
        }
        public ActionResult AddUser()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddUser(TAIKHOAN taiKhoan1, HttpPostedFileBase avatarFile)
        {
            if (!ModelState.IsValid)
            {
                // Lấy danh sách lỗi từ ModelState
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                              .Select(e => e.ErrorMessage)
                                              .ToList();

                // Trả về đối tượng JSON chứa thông tin lỗi
                return Json(new { success = false, errors });
            }
            if (avatarFile != null && avatarFile.ContentLength > 0)
            {
                var imagePath = Server.MapPath("/Asset/Image/profile/");
                var fileName = Path.GetFileName(avatarFile.FileName);
                var fullPath = Path.Combine(imagePath + fileName);
                avatarFile.SaveAs(fullPath);

                ANH newAvatar = new ANH() { Url = "/Asset/Image/profile/" + fileName };
                taiKhoan1.Avatar = newAvatar.ID;
                db.ANHs.Add(newAvatar);
            }
            List<TAIKHOAN> kt = db.TAIKHOANs.Where(x => x.TenTK == taiKhoan1.TenTK).ToList();
            /* kt = db.TAIKHOANs.Where(x => x.GioiTinh == taiKhoan1.GioiTinh).ToList();
             if (kt.Count > 0)
             {
                 ModelState["GioiTinh"].Errors.Add("Giới tính đã tồn tại =)))) ");
                 luu = false;
             }*/
            taiKhoan1.MatKhau = Func.MD5Hash(taiKhoan1.MatKhau);
            taiKhoan1.XnMK = Func.MD5Hash(taiKhoan1.XnMK);
            taiKhoan1.DuocSD = true;
            taiKhoan1.NgayCap = DateTime.Now;
            try
            {
                db.TAIKHOANs.Add(taiKhoan1);
                db.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var validationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.WriteLine($"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}");
                    }
                }
            }
            return Json(new { success = true });

        }
        // cài đặt trạng thái người dùng
        public ActionResult SettingUser(int id, int type)
        {
            // type: 1 khóa tài khoản, 2 thăng làm admin, 3 xóa tài khoản
            TAIKHOAN taiKhoan = db.TAIKHOANs.Where(x => x.ID == id).First();
            if (taiKhoan.TenTK == "Admin")
                return RedirectToAction("UserManagement");
            if (type == 1)
            {
                taiKhoan.DuocSD = taiKhoan.DuocSD ? false : true;
            }
            else if (type == 2)
            {
                taiKhoan.VaiTro = taiKhoan.VaiTro == "Admin" ? "User" : "Admin";
            }
            else if (type == 3)
            {
                db.TAIKHOANs.Remove(taiKhoan);
            }
            try
            {
                db.Configuration.ValidateOnSaveEnabled = false;
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                db.Configuration.ValidateOnSaveEnabled = true;
            }
            return RedirectToAction("UserManagement");
        }
        // thông tin tài khoản
        public ActionResult UserProfile(int id)
        {
            TAIKHOAN taiKhoan = db.TAIKHOANs.Where(x => x.ID == id).First();
            ViewBag.ActiveTab = "activity";
            return View(taiKhoan);
        }
        // sửa thông tin
        [HttpPost]
        public ActionResult UserProfile(TAIKHOAN taiKhoan, bool CapLaiMK)
        {
            TAIKHOAN taiKhoan1 = db.TAIKHOANs.Where(x => x.ID == taiKhoan.ID).First();
            // tài khoản admin sẽ không được phép ai thay đổi trừ chính nó
            if (taiKhoan1.TenTK == "admin")
                return View(taiKhoan1);
            taiKhoan1.HoTen = taiKhoan.HoTen;
            taiKhoan1.Email = taiKhoan.Email;
            taiKhoan1.SDT = taiKhoan.SDT;
            taiKhoan1.GhiChu = taiKhoan.GhiChu;
            //debug
            taiKhoan1.XnMK = taiKhoan1.MatKhau;
            if (CapLaiMK)
            {
                taiKhoan1.MatKhau = Func.MD5Hash("1111");
                taiKhoan1.XnMK = Func.MD5Hash("1111");
            }
            try
            {
                db.SaveChanges();
                ViewBag.ActiveTab = "activity";
                return View(taiKhoan1);
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var validationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.WriteLine($"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}");
                    }
                }
                ViewBag.ActiveTab = "settings";
                TAIKHOAN temp = db.TAIKHOANs.Where(x => x.ID == taiKhoan.ID).First();
                return View(temp);
            }
        }
        // thông tin admin (tài khoản admin cá nhân)
        public ActionResult ProfileAdmin()
        {
            User user = (User)Session.Contents["Account"];
            return View(user.Account);
        }
        [HttpPost]
        public ActionResult ProfileAdmin(TAIKHOAN taiKhoan, string xnMK)
        {
            TAIKHOAN taiKhoan1 = db.TAIKHOANs.Where(x => x.ID == taiKhoan.ID).First();
            if (!taiKhoan.MatKhau.Equals(xnMK))
            {
                ViewBag.xnMK = "Xác nhận mật khẩu không chính xác";
                return View(taiKhoan1);
            }
            bool luu = true;
            List<TAIKHOAN> kt = db.TAIKHOANs.Where(x => x.Email == taiKhoan.Email && x.ID != taiKhoan.ID).ToList();
            if (kt.Count > 0)
            {
                ModelState["Email"].Errors.Add("Email đã tồn tại");
                luu = false;
            }
            kt = db.TAIKHOANs.Where(x => x.SDT == taiKhoan.SDT && x.ID != taiKhoan.ID).ToList();
            if (kt.Count > 0)
            {
                ModelState["SDT"].Errors.Add("Số điện thoại đã tồn tại");
                luu = false;
            }
            if (!luu)
                return View(taiKhoan1);
            taiKhoan1.HoTen = taiKhoan.HoTen;
            taiKhoan1.Email = taiKhoan.Email;
            taiKhoan1.SDT = taiKhoan.SDT;
            taiKhoan1.GhiChu = taiKhoan.GhiChu;
            taiKhoan1.MatKhau = Func.MD5Hash(taiKhoan.MatKhau);
            try
            {
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return View(taiKhoan1);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
