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
    public class DynamicSegmentationController : Controller
    {
        private PipelineFeatureListDBContext db = new PipelineFeatureListDBContext();

        //
        // GET: /DynamicSegmentation/

        public ActionResult Index()
        {
            return View(db.DynamicSegmentations.ToList());
        }

        //
        // GET: /DynamicSegmentation/Details/5

        public ActionResult Details(long id = 0)
        {
            DynamicSegmentation dynamicsegmentation = db.DynamicSegmentations.Find(id);
            if (dynamicsegmentation == null)
            {
                return HttpNotFound();
            }
            return View(dynamicsegmentation);
        }

        //
        // GET: /DynamicSegmentation/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /DynamicSegmentation/Create

        [HttpPost]
        public ActionResult Create(DynamicSegmentation dynamicsegmentation)
        {
            if (ModelState.IsValid)
            {
                db.DynamicSegmentations.Add(dynamicsegmentation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dynamicsegmentation);
        }

        //
        // GET: /DynamicSegmentation/Edit/5

        public ActionResult Edit(long id = 0)
        {
            DynamicSegmentation dynamicsegmentation = db.DynamicSegmentations.Find(id);
            if (dynamicsegmentation == null)
            {
                return HttpNotFound();
            }
            return View(dynamicsegmentation);
        }

        //
        // POST: /DynamicSegmentation/Edit/5

        [HttpPost]
        public ActionResult Edit(DynamicSegmentation dynamicsegmentation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dynamicsegmentation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dynamicsegmentation);
        }

        //
        // GET: /DynamicSegmentation/Delete/5

        public ActionResult Delete(long id = 0)
        {
            DynamicSegmentation dynamicsegmentation = db.DynamicSegmentations.Find(id);
            if (dynamicsegmentation == null)
            {
                return HttpNotFound();
            }
            return View(dynamicsegmentation);
        }

        //
        // POST: /DynamicSegmentation/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            DynamicSegmentation dynamicsegmentation = db.DynamicSegmentations.Find(id);
            db.DynamicSegmentations.Remove(dynamicsegmentation);
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