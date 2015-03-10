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
    public class GalleryImagesController : Controller
    {
        private Entities db = new Entities();

        // GET: GalleryImages
        public ActionResult Index()
        {
            return View(db.GalleryImages.ToList());
        }

        // GET: GalleryImages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GalleryImage galleryImage = db.GalleryImages.Find(id);
            if (galleryImage == null)
            {
                return HttpNotFound();
            }
            return View(galleryImage);
        }

        // GET: GalleryImages/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GalleryImages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HttpPostedFileBase image_url, [Bind(Include = "id,title,image_url,statuss")] GalleryImage galleryImage)
        {
            if (ModelState.IsValid)
            {
                if (image_url != null && image_url.ContentLength > 0)
                {
                    var image_url_fileName = Path.GetFileName(image_url.FileName);
                    bool exists = Directory.Exists(Server.MapPath("~/Images/Gallery"));

                    if (!exists)
                        Directory.CreateDirectory(Server.MapPath("~/Images/Gallery"));
                    var image_url_path = Path.Combine(Server.MapPath("~/Images/Gallery"), image_url_fileName);
                    ImageResizer.ResizeImageFile(image_url_path, image_url);
                    //image_url.SaveAs(image_url_path);
                    galleryImage.image_url = "/Images/Gallery/" + image_url_fileName;
                }
                db.GalleryImages.Add(galleryImage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(galleryImage);
        }

        // GET: GalleryImages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GalleryImage galleryImage = db.GalleryImages.Find(id);
            if (galleryImage == null)
            {
                return HttpNotFound();
            }
            return View(galleryImage);
        }

        // POST: GalleryImages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(HttpPostedFileBase image_url,[Bind(Include = "id,title,image_url,statuss")] GalleryImage galleryImage)
        {
            if (ModelState.IsValid)
            {
                GalleryImage tempGalleryImage = db.GalleryImages.Find(galleryImage.id);
                if (tempGalleryImage != null)
                {
                    var fullPath = Server.MapPath("~" + tempGalleryImage.image_url);
                    if (System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);
                    }
                }
                if (image_url != null && image_url.ContentLength > 0)
                {
                    var image_url_fileName = Path.GetFileName(image_url.FileName);
                    bool exists = Directory.Exists(Server.MapPath("~/Images/Gallery"));

                    if (!exists)
                        Directory.CreateDirectory(Server.MapPath("~/Images/Gallery"));
                    var image_url_path = Path.Combine(Server.MapPath("~/Images/Gallery"), image_url_fileName);
                    ImageResizer.ResizeImageFile(image_url_path, image_url);
                    //image_url.SaveAs(image_url_path);
                    galleryImage.image_url = "/Images/Gallery/" + image_url_fileName;
                    tempGalleryImage.image_url = galleryImage.image_url;
                }
                tempGalleryImage.statuss = galleryImage.statuss;
                tempGalleryImage.title = galleryImage.title;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(galleryImage);
        }

        // GET: GalleryImages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GalleryImage galleryImage = db.GalleryImages.Find(id);
            if (galleryImage == null)
            {
                return HttpNotFound();
            }
            return View(galleryImage);
        }

        // POST: GalleryImages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GalleryImage galleryImage = db.GalleryImages.Find(id);
            if (galleryImage != null)
            {
                var fullPath = Server.MapPath("~" + galleryImage.image_url);
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
            }
            db.GalleryImages.Remove(galleryImage);
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
