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
    public class ANSIRatingController : Controller
    {
        private PipelineFeatureListDBContext db = new PipelineFeatureListDBContext();

        //
        // GET: /ANSIRating/

        public ActionResult Index()
        {
            return View(db.ANSIRatings.ToList());
        }

        //
        // GET: /ANSIRating/Details/5

        public ActionResult Details(int id = 0)
        {
            ANSIRating ansirating = db.ANSIRatings.Find(id);
            if (ansirating == null)
            {
                return HttpNotFound();
            }
            return View(ansirating);
        }

        //
        // GET: /ANSIRating/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /ANSIRating/Create

        [HttpPost]
        public ActionResult Create(ANSIRating ansirating)
        {
            if (ModelState.IsValid)
            {
                db.ANSIRatings.Add(ansirating);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ansirating);
        }

        //
        // GET: /ANSIRating/Edit/5

        public ActionResult Edit(int id = 0)
        {
            ANSIRating ansirating = db.ANSIRatings.Find(id);
            if (ansirating == null)
            {
                return HttpNotFound();
            }
            return View(ansirating);
        }

        //
        // POST: /ANSIRating/Edit/5

        [HttpPost]
        public ActionResult Edit(ANSIRating ansirating)
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
        // GET: /ANSIRating/Delete/5

        public ActionResult Delete(int id = 0)
        {
            ANSIRating ansirating = db.ANSIRatings.Find(id);
            if (ansirating == null)
            {
                return HttpNotFound();
            }
            return View(ansirating);
        }

        //
        // POST: /ANSIRating/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            ANSIRating ansirating = db.ANSIRatings.Find(id);
            db.ANSIRatings.Remove(ansirating);
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