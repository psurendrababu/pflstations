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
    public class SpecRatingController : Controller
    {
        private PipelineFeatureListDBContext db = new PipelineFeatureListDBContext();

        //
        // GET: /SpecRating/

        public ActionResult Index()
        {
            return View(db.SpecRatings.ToList());
        }

        //
        // GET: /SpecRating/Details/5

        public ActionResult Details(int id = 0)
        {
            SpecRating specrating = db.SpecRatings.Find(id);
            if (specrating == null)
            {
                return HttpNotFound();
            }
            return View(specrating);
        }

        //
        // GET: /SpecRating/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /SpecRating/Create

        [HttpPost]
        public ActionResult Create(SpecRating specrating)
        {
            if (ModelState.IsValid)
            {
                db.SpecRatings.Add(specrating);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(specrating);
        }

        //
        // GET: /SpecRating/Edit/5

        public ActionResult Edit(int id = 0)
        {
            SpecRating specrating = db.SpecRatings.Find(id);
            var specfeatures = (from vf in db.ValveSectionFeatures
                                where vf.SpecRatingID == specrating.SpecRatingID
                                select new
                                {
                                    vf
                                }).ToList();


            if (specfeatures.Count > 0)
            {
                ModelState.AddModelError("SpecRatingItem", "Warning! This Specification is assigned to Circuit feature(s).");
                ViewBag.HasError = "True";
            }
            if (specrating == null)
            {
                return HttpNotFound();
            }
            return View(specrating);
        }

        //
        // POST: /SpecRating/Edit/5

        [HttpPost]
        public ActionResult Edit(SpecRating specrating)
        {
            if (ModelState.IsValid)
            {
                db.Entry(specrating).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(specrating);
        }

        //
        // GET: /SpecRating/Delete/5

        public ActionResult Delete(int id = 0)
        {
            SpecRating specrating = db.SpecRatings.Find(id);
            var specfeatures = (from vf in db.ValveSectionFeatures
                               where vf.SpecRatingID == specrating.SpecRatingID
                                select new
                               {
                                   vf
                               }).ToList();


            if (specfeatures.Count > 0)
            {
                ModelState.AddModelError("SpecRatingItem", "This Specification is assigned to Circuit feature(s) and cannot be deleted.");
                ViewBag.HasError = "True";
            }
            if (specrating == null)
            {
                return HttpNotFound();
            }
            return View(specrating);
        }

        //
        // POST: /SpecRating/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            SpecRating specrating = db.SpecRatings.Find(id);
            db.SpecRatings.Remove(specrating);
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