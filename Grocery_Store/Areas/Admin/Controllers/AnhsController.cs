using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Grocery_Store.Areas.Admin.Data;
using Grocery_Store.Models;

namespace Grocery_Store.Areas.Admin.Controllers
{
    [CustomAuthor(Roles = "Admin")]
    public class AnhsController : Controller
    {
        private GroceryStoreDB db = new GroceryStoreDB();

        // GET: Admin/Anhs
        public ActionResult Media(string msg)
        {
            try
            {
                string viTri = Server.MapPath("~/Asset/Image");
                DirectoryInfo directory = new DirectoryInfo(viTri);
                DirectoryInfo[] directories = directory.GetDirectories();
                List<string> folder = new List<string>();
                ViewBag.msg = msg;
                foreach (DirectoryInfo s in directories)
                {
                    folder.Add(s.Name);
                }
                ViewBag.folder = folder;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return View();
        }
        // Danh sách Media Hình ảnh
        public ActionResult Media_Images(string folder)
        {
            List<ANH> a = db.ANHs.ToList();
            ViewBag.anhs = a.Where(x => x.Url.Split('/')[3] == folder).ToList();
            ViewBag.folder = folder;
            return View();
        }
        // Tạo Folder
        public ActionResult Media_Create_Folder(string folderName)
        {
            try
            {
                string viTri = Server.MapPath("~/Asset/Image/" + folderName);
                DirectoryInfo directory = new DirectoryInfo(viTri);
                if (!directory.Exists)
                    directory.Create();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return RedirectToAction("Media");

        }
        // Đổi tên Folder
        public ActionResult Media_Rename_Folder(string folderOldName, string folderNewName)
        {
            try
            {
                string viTri = Server.MapPath("~/Asset/Image/" + folderOldName);
                Trace.WriteLine(folderOldName);
                string vitri2 = Server.MapPath("~/Asset/Image/" + folderNewName);
                DirectoryInfo directory = new DirectoryInfo(viTri);
                FileInfo[] files = directory.GetFiles();
                DirectoryInfo directory1 = new DirectoryInfo(vitri2);
                if (directory1.Exists)
                {
                    return RedirectToAction("Media");
                }
                directory1.Create();
                foreach (FileInfo file in files)
                {
                    file.MoveTo(vitri2 + "/" + file.Name);
                }
                directory.Delete();
                //db.rename_path_anh(folderOldName, folderNewName);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return RedirectToAction("Media");
        }
        // xóa Folder 
        public ActionResult Media_Delete_Folder(string folderDeleteName)
        {
            try
            {
                string viTri = Server.MapPath("~/Asset/Image/" + folderDeleteName);
                DirectoryInfo directory = new DirectoryInfo(viTri);
                //db.remov_file_anh(folderDeleteName);
                directory.Delete(true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return RedirectToAction("Media", routeValues: new { msg = "Xóa không thành công, thư mục có thể chứa hình ảnh đang được sử dụng" });
            }
            return RedirectToAction("Media");
        }
        // thêm media
        [HttpPost]
        public ActionResult Media_Add(string folder, HttpPostedFileBase[] anh)
        {
            string path = Server.MapPath("~/Asset/Image/" + folder + "/");
            string imageUrl = string.Empty;
            int imageId = 0;

            foreach (HttpPostedFileBase image in anh)
            {
                if (image != null)
                {
                    string imageName = Path.GetFileName(image.FileName);
                    string PathSave = Path.Combine(path + imageName);
                    image.SaveAs(PathSave);
                    var newImage = new ANH() { Url = "/Asset/Image/" + folder + "/" + imageName };
                    db.ANHs.Add(newImage);
                    try
                    {
                        db.SaveChanges();
                        imageUrl = newImage.Url;
                        imageId = newImage.ID;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
            return Json(new { success = true, imageUrl = imageUrl, imageId = imageId });
        }

        // Xóa Media 
        [HttpPost]
        public ActionResult Delete_Image(string anhs, string folder)
        {
            string[] anh = anhs.Split(',');
            foreach (string a in anh)
            {
                int idAnh = Convert.ToInt32(a);
                ANH anh2 = db.ANHs.Where(x => x.ID == idAnh).First();
                string viTri = Server.MapPath("~" + anh2.Url);
                FileInfo file = new FileInfo(viTri);
                try
                {
                    db.ANHs.Remove(anh2);
                    db.SaveChanges();
                    file.Delete();
                }
                catch
                {
                    ViewBag.msg = "Xóa không thành công, hình ảnh có thể đang được sử dụng";
                    List<ANH> a2 = db.ANHs.ToList();
                    ViewBag.anhs = a2.Where(x => x.Url.Split('/')[3] == folder).ToList();
                    ViewBag.folder = folder;
                    return View("Media_Images");
                }
            }
            return RedirectToAction("Media");
        }
    }
}
