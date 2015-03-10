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
    public class CursoleArticlesController : Controller
    {
        private Entities db = new Entities();

        // GET: CursoleArticles
        public ActionResult Index()
        {
            return View(db.CursoleArticles.ToList());
        }

        // GET: CursoleArticles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CursoleArticle cursoleArticle = db.CursoleArticles.Find(id);
            if (cursoleArticle == null)
            {
                return HttpNotFound();
            }
            return View(cursoleArticle);
        }

        // GET: CursoleArticles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CursoleArticles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,title,subtitle,statuss")] CursoleArticle cursoleArticle)
        {
            if (ModelState.IsValid)
            {
                db.CursoleArticles.Add(cursoleArticle);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cursoleArticle);
        }

        // GET: CursoleArticles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CursoleArticle cursoleArticle = db.CursoleArticles.Find(id);
            if (cursoleArticle == null)
            {
                return HttpNotFound();
            }
            return View(cursoleArticle);
        }

        // POST: CursoleArticles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,title,subtitle,statuss")] CursoleArticle cursoleArticle)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cursoleArticle).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cursoleArticle);
        }

        // GET: CursoleArticles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CursoleArticle cursoleArticle = db.CursoleArticles.Find(id);
            if (cursoleArticle == null)
            {
                return HttpNotFound();
            }
            return View(cursoleArticle);
        }

        // POST: CursoleArticles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CursoleArticle cursoleArticle = db.CursoleArticles.Find(id);
            db.CursoleArticles.Remove(cursoleArticle);
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
