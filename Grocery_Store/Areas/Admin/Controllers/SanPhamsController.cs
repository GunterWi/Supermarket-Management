using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Diagnostics;
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
    public class SanPhamsController : Controller
    {
        private GroceryStoreDB db = new GroceryStoreDB();

        // GET: Admin/SanPhams
        public ActionResult Index(int? page)
        {
            int pageNum = (page ?? 1);
            var sanPham = db.SANPHAMs;
            List<SANPHAM> sanPhams = (from sp in sanPham orderby sp.ID ascending select sp).ToList();
            return View(sanPhams.ToPagedList(pageNum, 10));
        }
        // Create Product
        public ActionResult Create()
        {
            ViewBag.SANPHAM = new List<SANPHAM>();
            return View();
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult Create(SANPHAM sp, string tva, bool preview = false)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.SANPHAM = new List<SANPHAM>();
                // Lấy danh sách lỗi từ ModelState
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                              .Select(e => e.ErrorMessage)
                                              .ToList();

                // Trả về đối tượng JSON chứa thông tin lỗi
                return Json(new { success = false, errors });
            }
            if (String.IsNullOrEmpty(sp.Dvt) || sp.Dvt.Equals(""))
                sp.Dvt = "Cái";
            sp.NgayDangSP = DateTime.Now;
            sp.NdSP = String.IsNullOrEmpty(sp.NdSP) ? null : sp.NdSP.Replace("\n", "").Replace("\r", "");
            sp.TomTat = String.IsNullOrEmpty(sp.TomTat) ? null : sp.TomTat.Replace("\n", "").Replace("\r", "");
            sp.LuotMua = 0;
            sp.LuotXem = 0;
            db.SANPHAMs.Add(sp);
            try
            {
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return View("Create");
            }
            if (tva != null && tva != "")
            {
                string[] tva2 = tva.Split(',');
                foreach (string t in tva2)
                {
                    int idAnh = Convert.ToInt32(t);
                    db.Database.ExecuteSqlCommand("Insert into THUVIENANHSP (MaAnh,MaSP) values (" + idAnh + "," + sp.ID + ")");
                }
            }
            if (preview)
                return Preview(sp.ID);
            return Json(new { success = true });
        }
        //preview Sản phẩm
        private ActionResult Preview(int id)
        {
            GroceryStoreDB db = new GroceryStoreDB();
            Trace.WriteLine(id);
            var sanPhams = db.SANPHAMs;
            ViewBag.sanPham = sanPhams.Where(x => x.ID == id).FirstOrDefault();
            Trace.WriteLine(sanPhams.Where(x => x.ID == id).FirstOrDefault());
            ViewBag.khuyenMai = (from sp in sanPhams where sp.GiaKM < sp.GiaBan && sp.DangSP select sp).Take(4);
            Trace.WriteLine((from sp in sanPhams where sp.GiaKM < sp.GiaBan && sp.DangSP select sp).Take(4));
            int loaisp = ViewBag.sanPham.MaLoai;
            ViewBag.CungLoai = (from sp in sanPhams where sp.MaLoai == loaisp && sp.DangSP select sp).Take(4);
            Trace.WriteLine((from sp in sanPhams where sp.MaLoai == loaisp && sp.DangSP select sp).Take(4));
            return View("~/Views/Products/Product.cshtml");
        }
        public ActionResult Edit(int id)
        {
            // KHÔNG DÙNG ĐƯỢC MODEL LIST
            /*SANPHAM sanPhams = db.SANPHAMs.Where(x => x.ID == id).First();
            return View(sanPhams);*/
            var sanPhams = db.SANPHAMs;
            ViewBag.SANPHAM = (from sp in sanPhams where sp.ID == id select sp).ToList();
            return View();
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(SANPHAM sp, string tva, bool preview = false)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.SANPHAM = new List<SANPHAM>();
                // Lấy danh sách lỗi từ ModelState
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                              .Select(e => e.ErrorMessage)
                                              .ToList();

                // Trả về đối tượng JSON chứa thông tin lỗi
                return Json(new { success = false, errors });
            }
            SANPHAM sp2 = db.SANPHAMs.Where(x => x.ID == sp.ID).First();
            sp2.MaLoai = sp.MaLoai;
            sp2.TenSP = sp.TenSP;
            sp2.TenDuongDan = sp.TenDuongDan;
            sp2.GiaBan = sp.GiaBan;
            sp2.GiaKM = sp.GiaKM;
            sp2.Dvt = sp.Dvt;
            sp2.SoLuong = sp.SoLuong;
            sp2.AnhBia = sp.AnhBia;
            sp2.DangSP = sp.DangSP;
            sp2.NdSP = String.IsNullOrEmpty(sp.NdSP) ? null : sp.NdSP.Replace("\n", "").Replace("\r", "");
            sp2.TomTat = String.IsNullOrEmpty(sp.TomTat) ? null : sp.TomTat.Replace("\n", "").Replace("\r", "");
            try
            {
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            // xóa thư viện ảnh
            db.Database.ExecuteSqlCommand("delete THUVIENANHSP where MaSP = " + sp.ID);
            // thêm lại thư viện ảnh
            if (tva != null && tva != "")
            {
                string[] tva2 = tva.Split(',');
                foreach (string t in tva2)
                {
                    int idAnh = Convert.ToInt32(t);
                    db.Database.ExecuteSqlCommand("Insert into THUVIENANHSP (MaAnh,MaSP) values (" + idAnh + "," + sp2.ID + ")");
                }
            }
            if (preview)
                return Preview(sp.ID);
            return Json(new { success = true });
        }

        public ActionResult Delete(int id)
        {
            SANPHAM sp = db.SANPHAMs.Where(x => x.ID == id).First();
            try
            {
                db.SANPHAMs.Remove(sp);
                db.SaveChanges();
                var maxId = db.SANPHAMs.Max(p => (int?)p.ID) ?? 0;
                // Đặt lại giá trị IDENTITY sử dụng giá trị maxId
                db.Database.ExecuteSqlCommand("DBCC CHECKIDENT('SANPHAM', RESEED, @maxId)", new SqlParameter("maxId", maxId));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return Redirect(Url.Action("Index", "SanPhams"));
        }
        // Search Product
        public ActionResult Search_Product(string search, int? page)
        {
            int pageNum = (page ?? 1);
            List<SANPHAM> sanPhams = db.SANPHAMs.Where(x => DbFunctions.Like(x.TenSP, "%" + search + "%")).ToList();
            ViewBag.search = search;
            return View("Index", sanPhams.ToPagedList(pageNum, 10));
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
