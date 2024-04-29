using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Grocery_Store.Models;

namespace Grocery_Store.Areas.Admin.Controllers
{
    public class LoaiSPsController : Controller
    {
        private GroceryStoreDB db = new GroceryStoreDB();

        // GET: Admin/LoaiSPs
        public ActionResult Index()
        {
            var lOAISPs = db.LOAISPs.Include(l => l.DANHMUC);
            return View(lOAISPs.ToList());
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
