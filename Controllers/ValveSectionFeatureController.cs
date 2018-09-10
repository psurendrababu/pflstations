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
    public class ValveSectionFeatureController : Controller
    {
        private PipelineFeatureListDBContext db = new PipelineFeatureListDBContext();

        //
        // GET: /ValveSectionFeature/
        //private PipelineFeatureListDBContext features = new PipelineFeatureListDBContext();
        
        public ActionResult Index(int ValveSectionID)
        {
            return View();
        }

        //
        // GET: /ValveSectionFeature/Details/5

        public ActionResult Details(int id = 0)
        {
            ValveSectionFeature valvesectionfeature = db.ValveSectionFeatures.Find(id);
            if (valvesectionfeature == null)
            {
                return HttpNotFound();
            }
            return View(valvesectionfeature);
        }

        //
        // GET: /ValveSectionFeature/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /ValveSectionFeature/Create

        [HttpPost]
        public ActionResult Create(ValveSectionFeature valvesectionfeature)
        {
            if (ModelState.IsValid)
            {
                db.ValveSectionFeatures.Add(valvesectionfeature);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(valvesectionfeature);
        }

        //
        // GET: /ValveSectionFeature/Edit/5

        public ActionResult Edit(int id = 0)
        {
            ValveSectionFeature valvesectionfeature = db.ValveSectionFeatures.Find(id);
            if (valvesectionfeature == null)
            {
                return HttpNotFound();
            }
            return View(valvesectionfeature);
        }

        //
        // POST: /ValveSectionFeature/Edit/5

        [HttpPost]
        public ActionResult Edit(ValveSectionFeature valvesectionfeature)
        {
            if (ModelState.IsValid)
            {
                db.Entry(valvesectionfeature).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(valvesectionfeature);
        }

        //
        // GET: /ValveSectionFeature/Delete/5

        public ActionResult Delete(int id = 0)
        {
            ValveSectionFeature valvesectionfeature = db.ValveSectionFeatures.Find(id);
            if (valvesectionfeature == null)
            {
                return HttpNotFound();
            }
            return View(valvesectionfeature);
        }

        //
        // POST: /ValveSectionFeature/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            ValveSectionFeature valvesectionfeature = db.ValveSectionFeatures.Find(id);
            db.ValveSectionFeatures.Remove(valvesectionfeature);
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