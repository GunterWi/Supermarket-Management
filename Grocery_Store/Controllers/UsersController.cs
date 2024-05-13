using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
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
        public ActionResult Register(TAIKHOAN kh, string xnMatKhau)
        {
            if (!xnMatKhau.Equals(kh.MatKhau))
            {
                ViewData["LoiMK"] = "Xác thực mật khẩu không chính xác";
                return View("Login");
            }
            List<TAIKHOAN> kt = db.TAIKHOANs.Where(x => x.TenTK == kh.TenTK).ToList();
            bool luu = true;
            if (kt.Count > 0)
            {
                ModelState["TenTK"].Errors.Add("Tên tài khoản đã tồn tại");
                luu = false;
            }

            kt = db.TAIKHOANs.Where(x => x.Email == kh.Email).ToList();
            if (kt.Count > 0)
            {
                ModelState["Email"].Errors.Add("Email đã tồn tại");
                luu = false;
            }
            kt = db.TAIKHOANs.Where(x => x.SDT == kh.SDT).ToList();
            if (kt.Count > 0)
            {
                ModelState["SDT"].Errors.Add("Số điện thoại đã tồn tại");
                luu = false;
            }
            if (luu)
            {
                try
                {
                    kh.MatKhau = Func.MD5Hash(kh.MatKhau);
                    kh.DuocSD = true;
                    kh.NgayCap = DateTime.Now;
                    kh.VaiTro = "user";
                    db.TAIKHOANs.Add(kh);
                    db.SaveChanges();
                    User user = (User)Session.Contents["Account"];
                    user.Login(kh.TenTK, kh.MatKhau);
                    Response.Redirect(Url.Action("Index", "Homes"));
                }
                catch (Exception e)
                {
                    ViewData["LoiMK"] = e.Message;
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

            string oldImagePath = null;

            if (Avatar != null)
            {
                // Nếu có ảnh cũ, lưu đường dẫn để xóa sau khi cập nhật cơ sở dữ liệu thành công
                if (!string.IsNullOrEmpty(taiKhoan1.ANH.Url))
                {
                    oldImagePath = Server.MapPath(taiKhoan1.ANH.Url);
                }
                // Lưu tệp ảnh mới
                var imagePath = Server.MapPath("/Asset/Image/profile/");
                var fileName = Path.GetFileName(Avatar.FileName);
                var fullPath = Path.Combine(imagePath, fileName);
                Avatar.SaveAs(fullPath);

                // Tạo đối tượng ảnh mới và thêm vào cơ sở dữ liệu
                if (oldImagePath == null)
                {
                    ANH newAvatar = new ANH() { Url = "/Asset/Image/profile/" + fileName };
                    db.ANHs.Add(newAvatar);
                    db.SaveChanges();
                    taiKhoan1.Avatar = newAvatar.ID;
                }
                // Nếu có ảnh cũ và cập nhật cơ sở dữ liệu thành công, xóa ảnh cũ
                if (oldImagePath != null && System.IO.File.Exists(oldImagePath))
                {
                    db.Database.ExecuteSqlCommand("EXEC Update_url_anh @param1, @param2", new SqlParameter("param1", taiKhoan1.ANH.ID), new SqlParameter("param2", "/Asset/Image/profile/" + fileName));
                    db.SaveChanges();
                    System.IO.File.Delete(oldImagePath);
                }

            }
            // Cập nhật thông tin khác của tài khoản
            taiKhoan1.HoTen = Hoten;
            taiKhoan1.Email = Email;
            taiKhoan1.SDT = SDT;

            try
            {
                // Cập nhật thông tin tài khoản trong cơ sở dữ liệu
                user.Account = taiKhoan1;
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            //return View(taiKhoan1);
            return RedirectToAction("ProfileUser", "Users");
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
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return View("ProfileUser", taiKhoan1);
        }
    }
}
