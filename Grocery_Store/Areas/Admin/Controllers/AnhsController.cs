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
    [CustomAuthor(Roles = "Admin")]
    public class AnhsController : Controller
    {
        private GroceryStoreDB db = new GroceryStoreDB();

        // GET: Admin/Anhs
        public ActionResult Index()
        {
            return View(db.ANHs.ToList());
        }

        // GET: Admin/Anhs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ANH aNH = db.ANHs.Find(id);
            if (aNH == null)
            {
                return HttpNotFound();
            }
            return View(aNH);
        }

        // GET: Admin/Anhs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Anhs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Url")] ANH aNH)
        {
            if (ModelState.IsValid)
            {
                db.ANHs.Add(aNH);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(aNH);
        }

        // GET: Admin/Anhs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ANH aNH = db.ANHs.Find(id);
            if (aNH == null)
            {
                return HttpNotFound();
            }
            return View(aNH);
        }

        // POST: Admin/Anhs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Url")] ANH aNH)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aNH).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(aNH);
        }

        // GET: Admin/Anhs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ANH aNH = db.ANHs.Find(id);
            if (aNH == null)
            {
                return HttpNotFound();
            }
            return View(aNH);
        }

        // POST: Admin/Anhs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ANH aNH = db.ANHs.Find(id);
            db.ANHs.Remove(aNH);
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
