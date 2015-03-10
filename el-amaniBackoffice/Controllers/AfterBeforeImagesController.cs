using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using el_amaniBackoffice.Models;
using System.IO;

namespace el_amaniBackoffice.Controllers
{
    public class AfterBeforeImagesController : Controller
    {
        private Entities db = new Entities();

        // GET: AfterBeforeImages
        public ActionResult Index()
        {
            return View(db.AfterBeforeImages.ToList());
        }

        // GET: AfterBeforeImages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AfterBeforeImage afterBeforeImage = db.AfterBeforeImages.Find(id);
            if (afterBeforeImage == null)
            {
                return HttpNotFound();
            }
            return View(afterBeforeImage);
        }

        // GET: AfterBeforeImages/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AfterBeforeImages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HttpPostedFileBase url_picture_before, HttpPostedFileBase url_picture_after, [Bind(Include = "id,url_picture_before,url_picture_after,statuss,first_title,secoundry_title")] AfterBeforeImage afterBeforeImage)
        {
            if (ModelState.IsValid)
            {
                if (url_picture_before != null && url_picture_before.ContentLength > 0)
                {
                    var url_picture_before_fileName = Path.GetFileName(url_picture_before.FileName);
                    bool exists = Directory.Exists(Server.MapPath("~/Images/AfterBeforeImages"));

                    if (!exists)
                        Directory.CreateDirectory(Server.MapPath("~/Images/AfterBeforeImages"));
                    var url_picture_before_path = Path.Combine(Server.MapPath("~/Images/AfterBeforeImages"), url_picture_before_fileName);
                    ImageResizer.ResizeImageFile(url_picture_before_path,url_picture_before);
                    afterBeforeImage.url_picture_before = "/Images/AfterBeforeImages/" + url_picture_before_fileName;
                }
                if (url_picture_after != null && url_picture_after.ContentLength > 0)
                {
                    var url_picture_after_fileName = Path.GetFileName(url_picture_after.FileName);
                    var url_picture_after_path = Path.Combine(Server.MapPath("~/Images/AfterBeforeImages"), url_picture_after_fileName);
                    ImageResizer.ResizeImageFile(url_picture_after_path, url_picture_after);
                    afterBeforeImage.url_picture_after = "/Images/AfterBeforeImages/" + url_picture_after_fileName;
                }
                db.AfterBeforeImages.Add(afterBeforeImage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(afterBeforeImage);
        }

        // GET: AfterBeforeImages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AfterBeforeImage afterBeforeImage = db.AfterBeforeImages.Find(id);
            if (afterBeforeImage == null)
            {
                return HttpNotFound();
            }
            return View(afterBeforeImage);
        }

        // POST: AfterBeforeImages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(HttpPostedFileBase url_picture_before, HttpPostedFileBase url_picture_after, [Bind(Include = "id,url_picture_before,url_picture_after,statuss,first_title,secoundry_title")] AfterBeforeImage afterBeforeImage)
        {
                if (ModelState.IsValid)
                {
                    AfterBeforeImage tempAfterBeforeImage = db.AfterBeforeImages.Find(afterBeforeImage.id);
                    if (tempAfterBeforeImage != null)
                    {
                        var fullPath = Server.MapPath("~" + tempAfterBeforeImage.url_picture_after);
                        if (System.IO.File.Exists(fullPath))
                        {
                            System.IO.File.Delete(fullPath);
                        }
                        fullPath = Server.MapPath("~" + tempAfterBeforeImage.url_picture_before);
                        if (System.IO.File.Exists(fullPath))
                        {
                            System.IO.File.Delete(fullPath);
                        }
                    }
                    if (url_picture_before != null && url_picture_before.ContentLength > 0)
                    {
                        var url_picture_before_fileName = Path.GetFileName(url_picture_before.FileName);
                        bool exists = Directory.Exists(Server.MapPath("~/Images/AfterBeforeImages"));

                        if (!exists)
                            Directory.CreateDirectory(Server.MapPath("~/Images/AfterBeforeImages"));
                        var url_picture_before_path = Path.Combine(Server.MapPath("~/Images/AfterBeforeImages"), url_picture_before_fileName);
                        ImageResizer.ResizeImageFile(url_picture_before_path, url_picture_before);
                        //url_picture_before.SaveAs(url_picture_before_path);
                        afterBeforeImage.url_picture_before = "/Images/AfterBeforeImages/" + url_picture_before_fileName;
                        tempAfterBeforeImage.url_picture_before = afterBeforeImage.url_picture_before;
                    }
                    if (url_picture_after != null && url_picture_after.ContentLength > 0)
                    {
                        var url_picture_after_fileName = Path.GetFileName(url_picture_after.FileName);
                        var url_picture_after_path = Path.Combine(Server.MapPath("~/Images/AfterBeforeImages"), url_picture_after_fileName);
                        ImageResizer.ResizeImageFile(url_picture_after_path, url_picture_after);
                        //url_picture_after.SaveAs(url_picture_after_path);
                        afterBeforeImage.url_picture_after = "/Images/AfterBeforeImages/" + url_picture_after_fileName;
                        tempAfterBeforeImage.url_picture_after = afterBeforeImage.url_picture_after;
                    }
                    tempAfterBeforeImage.secoundry_title = afterBeforeImage.secoundry_title;
                    tempAfterBeforeImage.first_title = afterBeforeImage.first_title;
                    tempAfterBeforeImage.statuss = afterBeforeImage.statuss;

                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(afterBeforeImage);
            

        }
        // GET: AfterBeforeImages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AfterBeforeImage afterBeforeImage = db.AfterBeforeImages.Find(id);
            if (afterBeforeImage == null)
            {
                return HttpNotFound();
            }
            return View(afterBeforeImage);
        }

        // POST: AfterBeforeImages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AfterBeforeImage afterBeforeImage = db.AfterBeforeImages.Find(id);
            if (afterBeforeImage != null)
            {
                var fullPath = Server.MapPath("~" + afterBeforeImage.url_picture_after);
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
                fullPath = Server.MapPath("~" + afterBeforeImage.url_picture_before);
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
            }
            db.AfterBeforeImages.Remove(afterBeforeImage);
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
