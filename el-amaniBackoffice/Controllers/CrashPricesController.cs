using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using el_amaniBackoffice.Models;

namespace el_amaniBackoffice.Controllers
{
    public class CrashPricesController : Controller
    {
        private Entities db = new Entities();

        // GET: CrashPrices
        public ActionResult Index()
        {
            return View(db.CrashPrices.OrderBy(s => s.statuss).ThenByDescending(s => s.created_date).ToList());
        }

        // GET: CrashPrices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CrashPrice crashPrice = db.CrashPrices.Find(id);
            if (crashPrice == null)
            {
                return HttpNotFound();
            }
            return View(crashPrice);
        }

        // GET: CrashPrices/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CrashPrices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,email,phone,messag,url_image_1,url_image_2,url_image_3,statuss,name,created_date")] CrashPrice crashPrice)
        {
            if (ModelState.IsValid)
            {
                db.CrashPrices.Add(crashPrice);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(crashPrice);
        }

        // GET: CrashPrices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CrashPrice crashPrice = db.CrashPrices.Find(id);
            if (crashPrice == null)
            {
                return HttpNotFound();
            }
            return View(crashPrice);
        }

        // POST: CrashPrices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,email,phone,messag,url_image_1,url_image_2,url_image_3,statuss,name,created_date")] CrashPrice crashPrice)
        {
            if (ModelState.IsValid)
            {
                db.Entry(crashPrice).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(crashPrice);
        }

        // GET: CrashPrices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CrashPrice crashPrice = db.CrashPrices.Find(id);
            if (crashPrice == null)
            {
                return HttpNotFound();
            }
            return View(crashPrice);
        }

        // POST: CrashPrices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CrashPrice crashPrice = db.CrashPrices.Find(id);
            db.CrashPrices.Remove(crashPrice);
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
