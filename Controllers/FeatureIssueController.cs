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
    public class FeatureIssueController : Controller
    {
        private PipelineFeatureListDBContext db = new PipelineFeatureListDBContext();

        //
        // GET: /FeatureIssue/

        public ActionResult Index()
        {
            return View(db.FeatureIssues.ToList());
        }

        //
        // GET: /FeatureIssue/Details/5

        public ActionResult Details(long id = 0)
        {
            FeatureIssue featureissue = db.FeatureIssues.Find(id);
            if (featureissue == null)
            {
                return HttpNotFound();
            }
            return View(featureissue);
        }

        //
        // GET: /FeatureIssue/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /FeatureIssue/Create

        [HttpPost]
        public ActionResult Create(FeatureIssue featureissue)
        {
            if (ModelState.IsValid)
            {
                db.FeatureIssues.Add(featureissue);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(featureissue);
        }

        //
        // GET: /FeatureIssue/Edit/5

        public ActionResult Edit(long id = 0)
        {
            FeatureIssue featureissue = db.FeatureIssues.Find(id);
            if (featureissue == null)
            {
                return HttpNotFound();
            }
            return View(featureissue);
        }

        //
        // POST: /FeatureIssue/Edit/5

        [HttpPost]
        public ActionResult Edit(FeatureIssue featureissue)
        {
            if (ModelState.IsValid)
            {
                db.Entry(featureissue).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(featureissue);
        }

        //
        // GET: /FeatureIssue/Delete/5

        public ActionResult Delete(long id = 0)
        {
            FeatureIssue featureissue = db.FeatureIssues.Find(id);
            if (featureissue == null)
            {
                return HttpNotFound();
            }
            return View(featureissue);
        }

        //
        // POST: /FeatureIssue/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            FeatureIssue featureissue = db.FeatureIssues.Find(id);
            db.FeatureIssues.Remove(featureissue);
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