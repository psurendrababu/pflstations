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
    public class FeatureController : Controller
    {
        private PipelineFeatureListDBContext db = new PipelineFeatureListDBContext();

        //
        // GET: /Feature/

        public ActionResult Index()
        {
            return View(db.Features.ToList());
        }

        //
        // GET: /Feature/Details/5

        public ActionResult Details(int id = 0)
        {
            Feature feature = db.Features.Find(id);
            if (feature == null)
            {
                return HttpNotFound();
            }
            return View(feature);
        }

        //
        // GET: /Feature/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Feature/Create

        [HttpPost]
        public ActionResult Create(Feature feature)
        {
            if (ModelState.IsValid)
            {
                db.Features.Add(feature);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(feature);
        }

        //
        // GET: /Feature/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Feature feature = db.Features.Find(id);
            var features = (from vf in db.ValveSectionFeatures
                            where vf.FeatureID == feature.FeatureID
                            select new
                            {
                                vf
                            }).ToList();


            if (features.Count > 0)
            {
                ModelState.AddModelError("FeatureItem", "Warning! This Feature is assigned to Circuit feature(s).");
                ViewBag.HasError = "True";
            }
            if (feature == null)
            {
                return HttpNotFound();
            }
            return View(feature);
        }

        //
        // POST: /Feature/Edit/5

        [HttpPost]
        public ActionResult Edit(Feature feature)
        {
            if (ModelState.IsValid)
            {
                db.Entry(feature).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(feature);
        }

        //
        // GET: /Feature/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Feature feature = db.Features.Find(id);
            var features = (from vf in db.ValveSectionFeatures
                               where vf.FeatureID == feature.FeatureID
                        select new
                               {
                                   vf
                               }).ToList();


            if (features.Count > 0)
            {
                ModelState.AddModelError("FeatureItem", "This Feature is assigned to Circuit feature(s) and cannot be deleted.");
                ViewBag.HasError = "True";
            }
            if (feature == null)
            {
                return HttpNotFound();
            }
            return View(feature);
        }

        //
        // POST: /Feature/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Feature feature = db.Features.Find(id);
            db.Features.Remove(feature);
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