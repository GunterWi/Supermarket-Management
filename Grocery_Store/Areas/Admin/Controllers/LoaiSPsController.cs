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
    [CustomAuthor(Roles = "admin")]
    public class LoaiSPsController : Controller
    {
        private GroceryStoreDB db = new GroceryStoreDB();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Create(LOAISP loaiSP)
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

            // Lưu dữ liệu nếu không có lỗi
            db.LOAISPs.Add(loaiSP);
            db.SaveChanges();

            return Json(new { success = true });
        }
        [HttpPost]
        public ActionResult Edit(LOAISP loaiSP)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });
            }

            // Lưu dữ liệu nếu không có lỗi
            LOAISP loaiSP1 = db.LOAISPs.Where(x => x.ID == loaiSP.ID).First();
            loaiSP1.LoaiSP1 = loaiSP.LoaiSP1;
            loaiSP1.TenDuongDan = loaiSP.TenDuongDan;
            loaiSP1.ID_DanhMuc = loaiSP.ID_DanhMuc;
            loaiSP1.GhiChu = loaiSP.GhiChu;

            db.SaveChanges();

            return Json(new { success = true });
        }
        public ActionResult Delete(int id)
        {
            LOAISP loai = db.LOAISPs.Where(x => x.ID == id).First();
            try
            {
                db.LOAISPs.Remove(loai);
                db.SaveChanges();
                // Lấy giá trị ID lớn nhất từ bảng LOAISP
                var maxId = db.LOAISPs.Max(p => (int?)p.ID) ?? 0;
                // Đặt lại giá trị IDENTITY sử dụng giá trị maxId
                db.Database.ExecuteSqlCommand("DBCC CHECKIDENT('LOAISP', RESEED, @maxId)", new SqlParameter("maxId", maxId));
            }
            catch (Exception e)
            {
                TempData["Error"] = "Lỗi khi xóa: " + e.Message;
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        // Thực hiện ajax để lấy dữ liệu cho chỉnh sửa thể loại 
        [HttpPost]
        public ActionResult Ajax_Category(int id)
        {
            LOAISP loaiSP = db.LOAISPs.Where(x => x.ID == id).First();
            try
            {
                return Json(new
                {
                    TenLoai = loaiSP.LoaiSP1,
                    TenDuongDan = loaiSP.TenDuongDan,
                    ID_DanhMuc = loaiSP.ID_DanhMuc,
                    GhiChu = loaiSP.GhiChu
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
