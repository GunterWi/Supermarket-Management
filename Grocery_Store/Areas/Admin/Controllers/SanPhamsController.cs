using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Grocery_Store.Areas.Admin.Data;
using Grocery_Store.Models;

namespace Grocery_Store.Areas.Admin.Controllers
{
    [CustomAuthor(Roles = "admin")]
    public class SanPhamsController : Controller
    {
        private GroceryStoreDB db = new GroceryStoreDB();

        // GET: Admin/SanPhams
        public ActionResult Index()
        {
            var sANPHAMs = db.SANPHAMs.Include(s => s.ANH).Include(s => s.LOAISP);
            return View(sANPHAMs.ToList());
        }

        // GET: Admin/SanPhams/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SANPHAM sANPHAM = db.SANPHAMs.Find(id);
            if (sANPHAM == null)
            {
                return HttpNotFound();
            }
            return View(sANPHAM);
        }

        // GET: Admin/SanPhams/Create
        public ActionResult Create()
        {
            ViewBag.AnhBia = new SelectList(db.ANHs, "ID", "Url");
            ViewBag.MaLoai = new SelectList(db.LOAISPs, "ID", "LoaiSP1");
            return View();
        }

        // POST: Admin/SanPhams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,MaLoai,TenSP,TenDuongDan,TomTat,NgayDangSP,GiaBan,GiaKM,Dvt,SoLuong,AnhBia,NdSP,LuotXem,LuotMua,DangSP")] SANPHAM sANPHAM)
        {
            if (ModelState.IsValid)
            {
                db.SANPHAMs.Add(sANPHAM);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AnhBia = new SelectList(db.ANHs, "ID", "Url", sANPHAM.AnhBia);
            ViewBag.MaLoai = new SelectList(db.LOAISPs, "ID", "LoaiSP1", sANPHAM.MaLoai);
            return View(sANPHAM);
        }

        // GET: Admin/SanPhams/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SANPHAM sANPHAM = db.SANPHAMs.Find(id);
            if (sANPHAM == null)
            {
                return HttpNotFound();
            }
            ViewBag.AnhBia = new SelectList(db.ANHs, "ID", "Url", sANPHAM.AnhBia);
            ViewBag.MaLoai = new SelectList(db.LOAISPs, "ID", "LoaiSP1", sANPHAM.MaLoai);
            return View(sANPHAM);
        }

        // POST: Admin/SanPhams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,MaLoai,TenSP,TenDuongDan,TomTat,NgayDangSP,GiaBan,GiaKM,Dvt,SoLuong,AnhBia,NdSP,LuotXem,LuotMua,DangSP")] SANPHAM sANPHAM)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sANPHAM).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AnhBia = new SelectList(db.ANHs, "ID", "Url", sANPHAM.AnhBia);
            ViewBag.MaLoai = new SelectList(db.LOAISPs, "ID", "LoaiSP1", sANPHAM.MaLoai);
            return View(sANPHAM);
        }

        // GET: Admin/SanPhams/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SANPHAM sANPHAM = db.SANPHAMs.Find(id);
            if (sANPHAM == null)
            {
                return HttpNotFound();
            }
            return View(sANPHAM);
        }

        // POST: Admin/SanPhams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SANPHAM sANPHAM = db.SANPHAMs.Find(id);
            db.SANPHAMs.Remove(sANPHAM);
            db.SaveChanges();
            return RedirectToAction("Index");
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
