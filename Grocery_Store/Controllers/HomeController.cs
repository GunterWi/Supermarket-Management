using Grocery_Store.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Grocery_Store.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            GroceryStoreDB db = new GroceryStoreDB();
            var sanPhams = db.SANPHAMs;
            var topLoaiSanPham = (from sp in db.SANPHAMs
                                  where sp.DangSP
                                  group sp by sp.MaLoai into spGroup
                                  let totalViews = spGroup.Sum(x => x.LuotXem)
                                  orderby totalViews descending
                                  select spGroup.FirstOrDefault().LOAISP).Take(6).ToList();
            ViewBag.sanPhams = (from sp in sanPhams where sp.DangSP == true orderby sp.NgayDangSP descending select sp).Take(6);
            ViewBag.loaiSPs = topLoaiSanPham;
            return View();
        }
        public ActionResult Admin()
        {
            return RedirectToAction("Index", "Dashboards", new { area = "Admin" });
        }
    }
}