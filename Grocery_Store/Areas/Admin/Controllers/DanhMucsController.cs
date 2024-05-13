using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Grocery_Store.Areas.Admin.Data;
using Grocery_Store.Models;

namespace Grocery_Store.Areas.Admin.Controllers
{
    [CustomAuthor(Roles = "Admin")]
    public class DanhMucsController : Controller
    {
        private GroceryStoreDB db = new GroceryStoreDB();

        // GET: Admin/DanhMucs
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(DANHMUC dm)
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
            db.DANHMUCs.Add(dm);
            db.SaveChanges();
            return Json(new { success = true });
        }


        public ActionResult Edit(DANHMUC dm)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });
            }
            DANHMUC danhmuc = db.DANHMUCs.Where(x => x.ID == dm.ID).First();
            danhmuc.TenDanhMuc = dm.TenDanhMuc;
            /*danhmuc.TenDuongDan = dm.TenDuongDan;*/
            db.SaveChanges();

            return Json(new { success = true });
        }
        public ActionResult Delete(int id)
        {
            DANHMUC dm = db.DANHMUCs.Where(x => x.ID == id).First();
            try
            {
                db.DANHMUCs.Remove(dm);
                db.SaveChanges();
                // Lấy giá trị ID lớn nhất từ bảng LOAIMON
                var maxId = db.DANHMUCs.Max(p => (int?)p.ID) ?? 0;
                // Đặt lại giá trị IDENTITY sử dụng giá trị maxId
                db.Database.ExecuteSqlCommand("DBCC CHECKIDENT('DANHMUC', RESEED, @maxId)", new SqlParameter("maxId", maxId));
            }
            catch (Exception e)
            {
                TempData["Error"] = "Lỗi khi xóa: " + e.Message;
                return RedirectToAction("Index");            }
            return RedirectToAction("Index");
        }
        // Thực hiện ajax để lấy dữ liệu cho chỉnh sửa thể loại 
        [HttpPost]
        public ActionResult Ajax_Category(int id)
        {
            DANHMUC danhmuc = db.DANHMUCs.Where(x => x.ID == id).First();
            try
            {
                return Json(new
                {
                    TenDanhMuc = danhmuc.TenDanhMuc,
                    /*TenDuongDan = danhmuc.TenDuongDan*/
                });
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public ActionResult Search_Category(string search, int? page)
        {
            int pageNum = (page ?? 1);
            List<LOAISP> lOAIMAs = db.LOAISPs.Where(x => DbFunctions.Like(x.LoaiSP1, "%" + search + "%")).ToList();
            return View("Index", lOAIMAs.ToList());
        }

    }
}
