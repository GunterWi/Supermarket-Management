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
    public class DonhangsController : Controller
    {
        private GroceryStoreDB db = new GroceryStoreDB();

        // GET: Admin/Donhangs
        public ActionResult Index()
        {
            var dONHANGs = db.DONHANGs.Include(d => d.PHUONGXA).Include(d => d.QUANHUYEN).Include(d => d.TAIKHOAN).Include(d => d.TINHTHANH);
            return View(dONHANGs.ToList());
        }

        // GET: Admin/Donhangs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DONHANG dONHANG = db.DONHANGs.Find(id);
            if (dONHANG == null)
            {
                return HttpNotFound();
            }
            return View(dONHANG);
        }

        // GET: Admin/Donhangs/Create
        public ActionResult Create()
        {
            ViewBag.MaPhuong = new SelectList(db.PHUONGXAs, "ID", "Name");
            ViewBag.MaQuan = new SelectList(db.QUANHUYENs, "ID", "Name");
            ViewBag.KhachHang = new SelectList(db.TAIKHOANs, "ID", "HoTen");
            ViewBag.MaTP = new SelectList(db.TINHTHANHs, "ID", "Name");
            return View();
        }

        // POST: Admin/Donhangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,KhachHang,DcGiaoHang,MaPhuong,MaQuan,MaTP,XuatHD,NgayDatHang,NgayGiaoHang,ThanhCong,DvVanChuyen,MaVanChuyen,PhiShip,GhiChuKhach,GhiChuShop")] DONHANG dONHANG)
        {
            if (ModelState.IsValid)
            {
                db.DONHANGs.Add(dONHANG);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaPhuong = new SelectList(db.PHUONGXAs, "ID", "Name", dONHANG.MaPhuong);
            ViewBag.MaQuan = new SelectList(db.QUANHUYENs, "ID", "Name", dONHANG.MaQuan);
            ViewBag.KhachHang = new SelectList(db.TAIKHOANs, "ID", "HoTen", dONHANG.KhachHang);
            ViewBag.MaTP = new SelectList(db.TINHTHANHs, "ID", "Name", dONHANG.MaTP);
            return View(dONHANG);
        }

        // GET: Admin/Donhangs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DONHANG dONHANG = db.DONHANGs.Find(id);
            if (dONHANG == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaPhuong = new SelectList(db.PHUONGXAs, "ID", "Name", dONHANG.MaPhuong);
            ViewBag.MaQuan = new SelectList(db.QUANHUYENs, "ID", "Name", dONHANG.MaQuan);
            ViewBag.KhachHang = new SelectList(db.TAIKHOANs, "ID", "HoTen", dONHANG.KhachHang);
            ViewBag.MaTP = new SelectList(db.TINHTHANHs, "ID", "Name", dONHANG.MaTP);
            return View(dONHANG);
        }

        // POST: Admin/Donhangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,KhachHang,DcGiaoHang,MaPhuong,MaQuan,MaTP,XuatHD,NgayDatHang,NgayGiaoHang,ThanhCong,DvVanChuyen,MaVanChuyen,PhiShip,GhiChuKhach,GhiChuShop")] DONHANG dONHANG)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dONHANG).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaPhuong = new SelectList(db.PHUONGXAs, "ID", "Name", dONHANG.MaPhuong);
            ViewBag.MaQuan = new SelectList(db.QUANHUYENs, "ID", "Name", dONHANG.MaQuan);
            ViewBag.KhachHang = new SelectList(db.TAIKHOANs, "ID", "HoTen", dONHANG.KhachHang);
            ViewBag.MaTP = new SelectList(db.TINHTHANHs, "ID", "Name", dONHANG.MaTP);
            return View(dONHANG);
        }

        // GET: Admin/Donhangs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DONHANG dONHANG = db.DONHANGs.Find(id);
            if (dONHANG == null)
            {
                return HttpNotFound();
            }
            return View(dONHANG);
        }

        // POST: Admin/Donhangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DONHANG dONHANG = db.DONHANGs.Find(id);
            db.DONHANGs.Remove(dONHANG);
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
