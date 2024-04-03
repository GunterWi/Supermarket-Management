using Grocery_Store.Models;
using System;
using System.Collections.Generic;
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
            var monAns = db.SANPHAMs;
            var loaiMons = db.LOAISPs;
            ViewBag.monAns = (from sp in monAns where sp.DangSP == true orderby sp.NgayDangSP descending select sp).Take(6);
            ViewBag.loaiMons = loaiMons;
            return View();
        }
        public ActionResult Admin()
        {
            return RedirectToAction("Index", "Dashboards63131236", new { area = "Admin" });
        }
    }
}