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
    public class DynamicSegmentationRecordController : Controller
    {
        private PipelineFeatureListDBContext db = new PipelineFeatureListDBContext();

        //
        // GET: /DynamicSegmentationRecord/

        public ActionResult Index()
        {
            return View(db.DynamicSegmentationRecords.ToList());
        }

        //
        // GET: /DynamicSegmentationRecord/Details/5

        public ActionResult Details(long id = 0)
        {
            DynamicSegmentationRecord dynamicsegmentationrecord = db.DynamicSegmentationRecords.Find(id);
            if (dynamicsegmentationrecord == null)
            {
                return HttpNotFound();
            }
            return View(dynamicsegmentationrecord);
        }

        //
        // GET: /DynamicSegmentationRecord/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /DynamicSegmentationRecord/Create

        [HttpPost]
        public ActionResult Create(DynamicSegmentationRecord dynamicsegmentationrecord)
        {
            if (ModelState.IsValid)
            {
                db.DynamicSegmentationRecords.Add(dynamicsegmentationrecord);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dynamicsegmentationrecord);
        }

        //
        // GET: /DynamicSegmentationRecord/Edit/5

        public ActionResult Edit(long id = 0)
        {
            DynamicSegmentationRecord dynamicsegmentationrecord = db.DynamicSegmentationRecords.Find(id);
            if (dynamicsegmentationrecord == null)
            {
                return HttpNotFound();
            }
            return View(dynamicsegmentationrecord);
        }

        //
        // POST: /DynamicSegmentationRecord/Edit/5

        [HttpPost]
        public ActionResult Edit(DynamicSegmentationRecord dynamicsegmentationrecord)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dynamicsegmentationrecord).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dynamicsegmentationrecord);
        }

        //
        // GET: /DynamicSegmentationRecord/Delete/5

        public ActionResult Delete(long id = 0)
        {
            DynamicSegmentationRecord dynamicsegmentationrecord = db.DynamicSegmentationRecords.Find(id);
            if (dynamicsegmentationrecord == null)
            {
                return HttpNotFound();
            }
            return View(dynamicsegmentationrecord);
        }

        //
        // POST: /DynamicSegmentationRecord/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            DynamicSegmentationRecord dynamicsegmentationrecord = db.DynamicSegmentationRecords.Find(id);
            db.DynamicSegmentationRecords.Remove(dynamicsegmentationrecord);
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