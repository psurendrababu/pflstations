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
    public class OutsideDiameterController : Controller
    {
        private PipelineFeatureListDBContext db = new PipelineFeatureListDBContext();

        //
        // GET: /OutsideDiameter/

        public ActionResult Index()
        {
            return View(db.OutsideDiameters.ToList());
        }

        //
        // GET: /OutsideDiameter/Details/5

        public ActionResult Details(int id = 0)
        {
            OutsideDiameter outsidediameter = db.OutsideDiameters.Find(id);
            if (outsidediameter == null)
            {
                return HttpNotFound();
            }
            return View(outsidediameter);
        }

        //
        // GET: /OutsideDiameter/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /OutsideDiameter/Create

        [HttpPost]
        public ActionResult Create(OutsideDiameter outsidediameter)
        {
            if (ModelState.IsValid)
            {
                db.OutsideDiameters.Add(outsidediameter);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(outsidediameter);
        }

        //
        // GET: /OutsideDiameter/Edit/5

        public ActionResult Edit(int id = 0)
        {
            OutsideDiameter outsidediameter = db.OutsideDiameters.Find(id);
            if (outsidediameter == null)
            {
                return HttpNotFound();
            }
            return View(outsidediameter);
        }

        //
        // POST: /OutsideDiameter/Edit/5

        [HttpPost]
        public ActionResult Edit(OutsideDiameter outsidediameter)
        {
            if (ModelState.IsValid)
            {
                db.Entry(outsidediameter).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(outsidediameter);
        }

        //
        // GET: /OutsideDiameter/Delete/5

        public ActionResult Delete(int id = 0)
        {
            OutsideDiameter outsidediameter = db.OutsideDiameters.Find(id);
            if (outsidediameter == null)
            {
                return HttpNotFound();
            }
            return View(outsidediameter);
        }

        //
        // POST: /OutsideDiameter/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            OutsideDiameter outsidediameter = db.OutsideDiameters.Find(id);
            db.OutsideDiameters.Remove(outsidediameter);
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