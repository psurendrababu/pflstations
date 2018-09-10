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
    public class FeatureErrorController : Controller
    {
        private PipelineFeatureListDBContext db = new PipelineFeatureListDBContext();

        //
        // GET: /FeatureError/

        public ActionResult Index()
        {
            return View(db.FeatureErrors.ToList());
        }

        //
        // GET: /FeatureError/Details/5

        public ActionResult Details(long id = 0)
        {
            FeatureError featureerror = db.FeatureErrors.Find(id);
            if (featureerror == null)
            {
                return HttpNotFound();
            }
            return View(featureerror);
        }

        //
        // GET: /FeatureError/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /FeatureError/Create

        [HttpPost]
        public ActionResult Create(FeatureError featureerror)
        {
            if (ModelState.IsValid)
            {
                db.FeatureErrors.Add(featureerror);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(featureerror);
        }

        //
        // GET: /FeatureError/Edit/5

        public ActionResult Edit(long id = 0)
        {
            FeatureError featureerror = db.FeatureErrors.Find(id);
            if (featureerror == null)
            {
                return HttpNotFound();
            }
            return View(featureerror);
        }

        //
        // POST: /FeatureError/Edit/5

        [HttpPost]
        public ActionResult Edit(FeatureError featureerror)
        {
            if (ModelState.IsValid)
            {
                db.Entry(featureerror).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(featureerror);
        }

        //
        // GET: /FeatureError/Delete/5

        public ActionResult Delete(long id = 0)
        {
            FeatureError featureerror = db.FeatureErrors.Find(id);
            if (featureerror == null)
            {
                return HttpNotFound();
            }
            return View(featureerror);
        }

        //
        // POST: /FeatureError/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            FeatureError featureerror = db.FeatureErrors.Find(id);
            db.FeatureErrors.Remove(featureerror);
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