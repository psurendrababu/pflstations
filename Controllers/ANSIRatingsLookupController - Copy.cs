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
    public class ANSIRatingsLookupController : Controller
    {
        private PipelineFeatureListDBContext db = new PipelineFeatureListDBContext();

        //
        // GET: /ANSIRatingsLookup/

        public ActionResult Index()
        {
            return View(db.ANSIRatingsLookups.ToList());
        }

        //
        // GET: /ANSIRatingsLookup/Details/5

        public ActionResult Details(int id = 0)
        {
            ANSIRatingsLookup ansirating = db.ANSIRatingsLookups.Find(id);
            if (ansirating == null)
            {
                return HttpNotFound();
            }
            return View(ansirating);
        }

        //
        // GET: /ANSIRatingsLookup/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /ANSIRatingsLookup/Create

        [HttpPost]
        public ActionResult Create(ANSIRatingsLookup ansirating)
        {
            if (ModelState.IsValid)
            {
                db.ANSIRatingsLookups.Add(ansirating);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ansirating);
        }

        //
        // GET: /ANSIRatingsLookup/Edit/5

        public ActionResult Edit(int id = 0)
        {
            ANSIRatingsLookup ansirating = db.ANSIRatingsLookups.Find(id);
            if (ansirating == null)
            {
                return HttpNotFound();
            }
            return View(ansirating);
        }

        //
        // POST: /ANSIRatingsLookup/Edit/5

        [HttpPost]
        public ActionResult Edit(ANSIRatingsLookup ansirating)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ansirating).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ansirating);
        }

        //
        // GET: /ANSIRatingsLookup/Delete/5

        public ActionResult Delete(int id = 0)
        {
            ANSIRatingsLookup ansirating = db.ANSIRatingsLookups.Find(id);
            if (ansirating == null)
            {
                return HttpNotFound();
            }
            return View(ansirating);
        }

        //
        // POST: /ANSIRatingsLookup/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            ANSIRatingsLookup ansirating = db.ANSIRatingsLookups.Find(id);
            db.ANSIRatingsLookups.Remove(ansirating);
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