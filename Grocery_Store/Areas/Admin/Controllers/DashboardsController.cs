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
    public class DashboardsController : Controller
    {
        private GroceryStoreDB db = new GroceryStoreDB();

        // GET: Admin/Dashboards
        public ActionResult Index()
        {
            IEnumerable<TINHTHANH> ta = db.TINHTHANHs;
            ViewBag.tinh = from t in ta where t.ID == 1 select t;
            ViewBag.NewOrder = db.DONHANGs.Where(x => x.MaVanChuyen == null && x.ThanhCong == null).ToList().Count;
            ViewBag.DonThanhCong = db.DONHANGs.Where(x => x.ThanhCong == true).ToList().Count;
            ViewBag.SoNguoiDung = db.TAIKHOANs.ToList().Count;
            var donHangTrong7Ngay = db.Database.SqlQuery<OrderSummary>("EXEC GetTotalSumIn7days").ToList();
            ViewBag.DonHangTrongTuan = donHangTrong7Ngay;
            List<IGrouping<int, DONHANG>> donTheoThang = db.DONHANGs.ToList()
                                                                    .Where(dh => dh.NgayDatHang.Year == DateTime.Now.Year) // Chỉ lấy đơn hàng trong năm hiện tại
                                                                    .GroupBy(dh => dh.NgayDatHang.Month) // Nhóm theo tháng
                                                                    .OrderBy(g => g.Key) // Sắp xếp theo tháng
                                                                    .ToList();
            ViewBag.DonHangTheoThang = donTheoThang;
            return View();
        }
    }
}
