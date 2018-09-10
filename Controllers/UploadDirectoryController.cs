using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PipelineFeatureList.Models;

namespace PipelineFeatureList.Controllers
{
    public class UploadDirectoryController : Controller
    {
        private PipelineFeatureListDBContext db = new PipelineFeatureListDBContext();

        //
        // GET: /UploadDirectory/

        public ActionResult Index()
        {
            return View(db.UploadDirectories.ToList());
        }

        //
        // GET: /UploadDirectory/Details/5

        public ActionResult Details(int id = 0)
        {
            UploadDirectory uploaddirectory = db.UploadDirectories.Find(id);
            if (uploaddirectory == null)
            {
                return HttpNotFound();
            }
            return View(uploaddirectory);
        }

        //
        // GET: /UploadDirectory/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /UploadDirectory/Create

        [HttpPost]
        public ActionResult Create(UploadDirectory uploaddirectory)
        {
            if (ModelState.IsValid)
            {
                db.UploadDirectories.Add(uploaddirectory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(uploaddirectory);
        }

        //
        // GET: /UploadDirectory/Edit/5

        public ActionResult Edit(int id = 0)
        {
            UploadDirectory uploaddirectory = db.UploadDirectories.Find(id);
            if (uploaddirectory == null)
            {
                return HttpNotFound();
            }
            return View(uploaddirectory);
        }

        //
        // POST: /UploadDirectory/Edit/5

        [HttpPost]
        public ActionResult Edit(UploadDirectory uploaddirectory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(uploaddirectory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(uploaddirectory);
        }

        //
        // GET: /UploadDirectory/Delete/5

        public ActionResult Delete(int id = 0)
        {
            UploadDirectory uploaddirectory = db.UploadDirectories.Find(id);
            if (uploaddirectory == null)
            {
                return HttpNotFound();
            }
            return View(uploaddirectory);
        }

        //
        // POST: /UploadDirectory/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            UploadDirectory uploaddirectory = db.UploadDirectories.Find(id);
            db.UploadDirectories.Remove(uploaddirectory);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}