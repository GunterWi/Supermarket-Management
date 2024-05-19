using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Grocery_Store.Models;
using Newtonsoft.Json.Linq;

namespace Grocery_Store.Controllers
{
    public class UsersController : Controller
    {
        private GroceryStoreDB db = new GroceryStoreDB();

        public ActionResult Login()
        {
            User user = (User)Session.Contents["Account"];
            if (user.daDangNhap)
            {
                Response.Redirect(Url.Action("Index", "Homes"));
            }
            return View();
        }
        [HttpPost]
        public ActionResult Login(string TaiKhoan, string MatKhau)
        {
            User user = (User)Session.Contents["Account"];
            if (user.Account == null)
            {
                user = new User();
                Session.Contents["Account"] = user;
            }
            if (user.daDangNhap)
            {
                return RedirectToAction("Index", "Homes");
            }
            if (user.Login(TaiKhoan, Func.MD5Hash(MatKhau)))
            {
                return RedirectToAction("Index", "Homes");
            }
            TAIKHOAN taiKhoan = db.TAIKHOANs.FirstOrDefault(x => x.TenTK == TaiKhoan);
            if (taiKhoan != null && !taiKhoan.DuocSD)
            {
                ViewBag.msg = "Tài khoản này đã bị khóa";
                return View();
            }
            ViewBag.msg = "Tên tài khoản hoặc mật khẩu không chính xác";
            return View();
        }
        public void Logout()
        {
            User user = (User)Session.Contents["Account"];
            user.Logout();
            Response.Redirect("/");
        }
        [HttpPost]
        public ActionResult Register(TAIKHOAN kh)
        {
            List<TAIKHOAN> kt = db.TAIKHOANs.Where(x => x.TenTK == kh.TenTK).ToList();
            try
            {
                kh.MatKhau = Func.MD5Hash(kh.MatKhau);
                kh.XnMK = Func.MD5Hash(kh.XnMK);
                kh.DuocSD = true;
                kh.NgayCap = DateTime.Now;
                kh.VaiTro = "User";
                db.TAIKHOANs.Add(kh);
                db.SaveChanges();
                User user = (User)Session.Contents["Account"];
                user.Login(kh.TenTK, kh.MatKhau);
                Response.Redirect(Url.Action("Index", "Homes"));
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
            return View("Login");
        }
        public ActionResult ProfileUser()
        {
            User user = (User)Session.Contents["Account"];
            return View(user.Account);
        }
        [HttpPost]
        public ActionResult ProfileUser(int id, HttpPostedFileBase Avatar, string Hoten, string Email, string SDT)
        {
            TAIKHOAN taiKhoan1 = db.TAIKHOANs.FirstOrDefault(x => x.ID == id);
            User user = (User)Session.Contents["Account"];
            if (taiKhoan1 == null)
            {
                Trace.WriteLine("thất bại.");
                return HttpNotFound();
            }

            if (Avatar != null)
            {
                string oldImagePath = SaveNewAvatar(taiKhoan1, Avatar);
                if (oldImagePath != null && System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }

            taiKhoan1.HoTen = Hoten;
            taiKhoan1.Email = Email;
            taiKhoan1.SDT = SDT;

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
            user.Account = taiKhoan1;

            return RedirectToAction("ProfileUser", "Users");
        }

        private string SaveNewAvatar(TAIKHOAN taiKhoan1, HttpPostedFileBase Avatar)
        {
            string oldImagePath = null;

            if (taiKhoan1.Avatar != null)
            {
                oldImagePath = Server.MapPath(taiKhoan1.ANH.Url);
            }

            var imagePath = Server.MapPath("/Asset/Image/profile/");
            var fileName = Path.GetFileName(Avatar.FileName);
            var fullPath = Path.Combine(imagePath, fileName);
            Avatar.SaveAs(fullPath);

            if (oldImagePath == null)
            {
                ANH newAvatar = new ANH() { Url = "/Asset/Image/profile/" + fileName };
                db.ANHs.Add(newAvatar);
                db.SaveChanges();
                taiKhoan1.Avatar = newAvatar.ID;
            }
            else
            {
                db.Database.ExecuteSqlCommand("EXEC Update_url_anh @param1, @param2",
                    new SqlParameter("param1", taiKhoan1.ANH.ID),
                    new SqlParameter("param2", "/Asset/Image/profile/" + fileName));
                db.SaveChanges();
            }

            return oldImagePath;
        }


        [HttpPost]
        public ActionResult ChangeMK(int id, string MatKhau, string xnMK)
        {
            TAIKHOAN taiKhoan1 = db.TAIKHOANs.Where(x => x.ID == id).First();
            if (!MatKhau.Equals(xnMK))
            {
                ViewBag.xnMK = "Xác nhận mật khẩu không chính xác";
                return View(taiKhoan1);
            }
            taiKhoan1.MatKhau = Func.MD5Hash(MatKhau);
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
            return View("ProfileUser", taiKhoan1);
        }
    }
}
