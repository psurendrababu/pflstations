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
    public class PTTestMediumController : Controller
    {
        private PipelineFeatureListDBContext db = new PipelineFeatureListDBContext();

        //
        // GET: /PTTestMedium/

        public ActionResult Index()
        {
            return View(db.PTTestMediums.ToList());
        }

        //
        // GET: /PTTestMedium/Details/5

        public ActionResult Details(int id = 0)
        {
            PTTestMedium pttestmedium = db.PTTestMediums.Find(id);            
            if (pttestmedium == null)
            {
                return HttpNotFound();
            }
            return View(pttestmedium);
        }

        //
        // GET: /PTTestMedium/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /PTTestMedium/Create

        [HttpPost]
        public ActionResult Create(PTTestMedium pttestmedium)
        {
            if (ModelState.IsValid)
            {
                db.PTTestMediums.Add(pttestmedium);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pttestmedium);
        }

        //
        // GET: /PTTestMedium/Edit/5

        public ActionResult Edit(int id = 0)
        {
            PTTestMedium pttestmedium = db.PTTestMediums.Find(id);
            var ptfeatures = (from vf in db.PressureTestRecords
                              where vf.PTMedium == pttestmedium.PTMedium
                              select new
                              {
                                  vf
                              }).ToList();


            if (ptfeatures.Count > 0)
            {
                ModelState.AddModelError("PTMedium", "Warning! This PT Test Medium is assigned to Pressure Test Record(s).");
                ViewBag.HasError = "True";
            }

            if (pttestmedium == null)
            {
                return HttpNotFound();
            }
            return View(pttestmedium);
        }

        //
        // POST: /PTTestMedium/Edit/5

        [HttpPost]
        public ActionResult Edit(PTTestMedium pttestmedium)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pttestmedium).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pttestmedium);
        }

        //
        // GET: /PTTestMedium/Delete/5

        public ActionResult Delete(int id = 0)
        {
            PTTestMedium pttestmedium = db.PTTestMediums.Find(id);
            var ptfeatures = (from vf in db.PressureTestRecords
                              where vf.PTMedium == pttestmedium.PTMedium
                              select new
                              {
                                  vf
                              }).ToList();


            if (ptfeatures.Count > 0)
            {
                ModelState.AddModelError("PTMedium", "This PT Test Medium is assigned to Pressure Test Record(s) and cannot be deleted.");
                ViewBag.HasError = "True";            
            }


            if (pttestmedium == null)
            {
                return HttpNotFound();
            }
            return View(pttestmedium);
        }

        //
        // POST: /PTTestMedium/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            PTTestMedium pttestmedium = db.PTTestMediums.Find(id);
            db.PTTestMediums.Remove(pttestmedium);
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