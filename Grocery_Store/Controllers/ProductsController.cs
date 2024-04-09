using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Grocery_Store.Models;
using PagedList;

namespace Grocery_Store.Controllers
{
    public class ProductsController : Controller
    {
        private GroceryStoreDB db = new GroceryStoreDB();

        public ActionResult Index(int? page)
        {
            int pageNum = page ?? 1;
            List<SANPHAM> SANPHAMs = db.SANPHAMs.Where(x => x.DangSP).ToList();
            return View(SANPHAMs.ToPagedList(pageNum, 12));
        }
        // Nội dung sản phẩm
        public ActionResult Product(string id)
        {
            var sanPhams = db.SANPHAMs;
            ViewBag.sanPham = (from sp in sanPhams where sp.TenDuongDan.Equals(id) && sp.DangSP select sp).FirstOrDefault();
            ViewBag.khuyenMai = (from sp in sanPhams where sp.GiaKM < sp.GiaBan && sp.DangSP select sp).Take(4);
            int loaisp = ViewBag.sanPham.MaLoai;
            ViewBag.CungLoai = (from sp in sanPhams where sp.MaLoai == loaisp && sp.DangSP select sp).Take(4);
            SANPHAM sanPham = ViewBag.sanPham;
            sanPham.LuotXem++;
            db.SaveChanges();
            return View();
        }
        // Giỏ hàng 
        public ActionResult Cart()
        {
            List<GioHang> gioHangs = GioHang.GetGioHang();
            return View(gioHangs);
        }
        // Thanh toán 
        public ActionResult Checkout()
        {
            User user = (User)Session.Contents["Account"];
            if (!user.daDangNhap)
            {
                TempData["AlertMessage"] = "Hãy đăng nhập vào tài khoản của bạn.";
                return Redirect(Url.Action("Login", "Users"));
            }
            List<GioHang> gioHangs = GioHang.GetGioHang();
            if (gioHangs.Count <= 0)
                return RedirectToAction("Cart");
            return View();
        }
        // ajax sử dụng ajax để lấy dữ liệu tỉnh thành, quận huyện, phường xã
        [HttpPost]
        public ActionResult TinhThanh(int id, int type)
        {
            List<string> value = new List<string>();
            List<int> ID = new List<int>();
            if (type == 1)
            {
                foreach (QUANHUYEN phuongXa in db.QUANHUYENs.Where(x => x.TinhThanh == id).OrderBy(x => x.Name))
                {
                    value.Add(phuongXa.Name);
                    ID.Add(phuongXa.ID);
                }
            }
            else if (type == 2)
            {
                foreach (PHUONGXA phuongXa in db.PHUONGXAs.Where(x => x.QuanHuyen == id).OrderBy(x => x.Name))
                {
                    value.Add(phuongXa.Name);
                    ID.Add(phuongXa.ID);
                }
            }
            return Json(new
            {
                Value = value.ToArray(),
                ID = ID.ToArray()
            });
        }
        [HttpPost]
        public ActionResult Checkout(DONHANG donHang)
        {
            User user = (User)Session.Contents["Account"];
            if (!user.daDangNhap)
                return Redirect(Url.Action("Login", "Users"));
            List<GioHang> gioHangs = GioHang.GetGioHang();
            if (gioHangs.Count <= 0)
                return RedirectToAction("Cart");
            using (DbContextTransaction transaction = db.Database.BeginTransaction())
            {
                try
                {
                    donHang.NgayDatHang = DateTime.Now;
                    donHang.KhachHang = user.id ?? 0;
                    db.DONHANGs.Add(donHang);
                    db.SaveChanges();
                    // đếm số đơn hợp lệ được thêm vào
                    int count = 0;
                    foreach (GioHang gioHang in gioHangs)
                    {
                        // yêu cầu số lượng sản phẩm phải lớn hơn 0
                        if (gioHang.soLuong <= 0) continue;
                        CHITIETDH chiTietDH = new CHITIETDH()
                        {
                            MaDH = donHang.ID,
                            MaSP = gioHang.sanPham.ID,
                            SoLuong = gioHang.soLuong,
                            GiaBan = gioHang.tongCong,
                        };
                        db.SANPHAMs.Where(x => x.ID == gioHang.sanPham.ID).First().SoLuong -= gioHang.soLuong;
                        db.SANPHAMs.Where(x => x.ID == gioHang.sanPham.ID).First().LuotMua += gioHang.soLuong;
                        db.CHITIETDHs.Add(chiTietDH);
                        count++;
                    }
                    // nếu trong chi tiết đơn hàng không có nào hợp lên thì xóa cả đơn hàng, tránh đơn hàng rỗng
                    if (count <= 0)
                    {
                        transaction.Rollback();
                        return Redirect("/");
                    }
                    db.SaveChanges();
                    transaction.Commit();
                    //reset GioHang
                    Response.Cookies["GioHang"].Value = "";
                    Response.Cookies["GioHang"].Expires = DateTime.Now.AddDays(-1);
                    //update session
                    user.RefreshUserData();
                    Session.Contents["Account"] = user;
                    //TempData["AlertMessage"] = "Bạn đã đặt hàng thành công";
                    return Redirect(Url.Action("ProfileUser", "Users", new { tab = "timeline" }));
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    Console.WriteLine(e.Message);
                    ViewBag.msg = "Lỗi không thể đặt hàng";
                    return View();
                }

            }

            return Redirect("/");
        }
        // sản phẩm theo thể loại 
        public ActionResult Category(string id, int? page)
        {
            int pageNum = page ?? 1;
            List<SANPHAM> SANPHAMs = db.SANPHAMs.Where(x => x.DangSP && x.LOAISP.TenDuongDan == id).ToList();
            return View("Index", SANPHAMs.ToPagedList(pageNum, 12));
        }
        // Tìm kiếm sản phẩm
        public ActionResult Search(string search, int? page)
        {
            if (page.Equals(null))
            {
                page = 1;
            }
            // loại bỏ dấu tiếng việt
            search = Func.convertToUnSign3(search);
            string[] key = search.Split(' ');
            List<SANPHAM> SANPHAMs = new List<SANPHAM>();
            // lọc từng key ra theo dấu cách
            foreach (String k in key)
            {
                foreach (SANPHAM s in db.SANPHAMs)
                {
                    if (Func.convertToUnSign3(s.TenSP).IndexOf(k) != -1) // nếu có
                        SANPHAMs.Add(s);
                }
            }
            ViewBag.search = search;
            int pageNum = page ?? 1;
            return View("Index", SANPHAMs.ToPagedList(pageNum, 12));

        }
        // Quản lý đơn hàng của khách
        public ActionResult OrderManagement(int id)
        {
            DONHANG donHang = db.DONHANGs.Where(x => x.ID == id).First();
            return View(donHang);
        }
        [HttpPost]
        public ActionResult OrderManagement(int sao, int MaSP, int maDH)
        {
            User user = (User)Session.Contents["Account"];
            if (!user.daDangNhap) return null;
            db.CHITIETDHs.Where(x => x.MaSP == MaSP && x.MaDH == maDH).First().DanhGia = sao;
            db.SaveChanges();
            return Json(new { msg = "Đánh giá thành công" });
        }
        public ActionResult Comment(BINHLUAN binhLuan, int MaSP)
        {
            User user = (User)Session.Contents["Account"];
            if (!user.daDangNhap)
            {
                TempData["AlertMessage"] = "Hãy đăng nhập vào tài khoản của bạn.";
                return Redirect(Url.Action("Login", "Users"));
            }
            binhLuan.MaTK = user.Account.ID;
            binhLuan.MaSP = MaSP;
            if (binhLuan.NoiDung.IndexOf('@') == -1)
                binhLuan.PhanHoi = null;

            if (binhLuan.PhanHoi != null && binhLuan.NoiDung.IndexOf('@') != -1)
            {
                string ten = binhLuan.NoiDung.Split('‍')[1]; // ký tự ẩn
                string tenPhanHoi = db.BINHLUANs.Where(x => x.ID == binhLuan.PhanHoi).First().TAIKHOAN.HoTen;
                if (!ten.Equals(tenPhanHoi))
                {
                    binhLuan.PhanHoi = null;
                }
                else
                {
                    ten = "<a href = ''><strong>" + ten + "</strong></a>";
                    binhLuan.NoiDung = ten + binhLuan.NoiDung.Split('‍')[2]; // ký tự ẩn
                }
            }
            db.BINHLUANs.Add(binhLuan);
            db.SaveChanges();
            string tenDuongDan = db.SANPHAMs.Where(x => x.ID == binhLuan.MaSP).First().TenDuongDan;
            return Redirect(Url.Action("Product", "Products", routeValues: new { id = tenDuongDan }));
        }
        // lọc giá sản phẩm
        public ActionResult price_filter(string price, int? page)
        {
            string[] price1 = price.Split(',');
            int min = Convert.ToInt32(price1[0]);
            int max = Convert.ToInt32(price1[1]);
            int pageNum = page ?? 1;
            List<SANPHAM> SANPHAMs = db.SANPHAMs.Where(x => x.DangSP && x.GiaBan >= min && x.GiaBan <= max).ToList();
            ViewBag.price = price;
            return View("Index", SANPHAMs.ToPagedList(pageNum, 12));
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
