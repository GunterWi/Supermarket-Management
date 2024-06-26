﻿using Grocery_Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Grocery_Store.Controllers
{
    public class HomesController : Controller
    {
        // GET: Homes
        public ActionResult Index()
        {
            GroceryStoreDB db = new GroceryStoreDB();
            var sanPhams = db.SANPHAMs;
            var topLoaiSanPham = (from sp in db.SANPHAMs
                                  where sp.DangSP
                                  group sp by sp.MaLoai into spGroup
                                  let totalViews = spGroup.Sum(x => x.LuotXem)
                                  orderby totalViews descending
                                  select spGroup.FirstOrDefault().LOAISP).Take(6).ToList(); // just take 6
            ViewBag.sanPhams = (from sp in sanPhams where sp.DangSP == true orderby sp.NgayDangSP descending select sp).Take(6);
            ViewBag.loaiSPs = topLoaiSanPham;
            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult Admin()
        {
            return RedirectToAction("Index", "Dashboards", new { area = "Admin" });
        }
    }
}