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
    public class GroupClassificationController : Controller
    {
        private PipelineFeatureListDBContext db = new PipelineFeatureListDBContext();

        //
        // GET: /GroupClassification/

        public ActionResult Index()
        {
            return View(db.GroupClassifications.ToList());
        }

        //
        // GET: /GroupClassification/Details/5

        public ActionResult Details(int id = 0)
        {
            GroupClassification GroupClassification = db.GroupClassifications.Find(id);
            if (GroupClassification == null)
            {
                return HttpNotFound();
            }
            return View(GroupClassification);
        }

        //
        // GET: /GroupClassification/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /GroupClassification/Create

        [HttpPost]
        public ActionResult Create(GroupClassification GroupClassification)
        {
            if (ModelState.IsValid)
            {
                db.GroupClassifications.Add(GroupClassification);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(GroupClassification);
        }

        //
        // GET: /GroupClassification/Edit/5

        public ActionResult Edit(int id = 0)
        {
            GroupClassification GroupClassification = db.GroupClassifications.Find(id);
            if (GroupClassification == null)
            {
                return HttpNotFound();
            }
            return View(GroupClassification);
        }

        //
        // POST: /GroupClassification/Edit/5

        [HttpPost]
        public ActionResult Edit(GroupClassification GroupClassification)
        {
            if (ModelState.IsValid)
            {
                db.Entry(GroupClassification).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(GroupClassification);
        }

        //
        // GET: /GroupClassification/Delete/5

        public ActionResult Delete(int id = 0)
        {
            GroupClassification GroupClassification = db.GroupClassifications.Find(id);
            if (GroupClassification == null)
            {
                return HttpNotFound();
            }
            return View(GroupClassification);
        }

        //
        // POST: /GroupClassification/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            GroupClassification GroupClassification = db.GroupClassifications.Find(id);
            db.GroupClassifications.Remove(GroupClassification);
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