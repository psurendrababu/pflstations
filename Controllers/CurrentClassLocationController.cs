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
    public class CurrentClassLocationController : Controller
    {
        private PipelineFeatureListDBContext db = new PipelineFeatureListDBContext();

        //
        // GET: /CurrentClassLocation/

        public ActionResult Index()
        {
            return View(db.CurrentClassLocations.ToList());
        }

        //
        // GET: /CurrentClassLocation/Details/5

        public ActionResult Details(int id = 0)
        {
            CurrentClassLocation currentclasslocation = db.CurrentClassLocations.Find(id);
            if (currentclasslocation == null)
            {
                return HttpNotFound();
            }
            return View(currentclasslocation);
        }

        //
        // GET: /CurrentClassLocation/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /CurrentClassLocation/Create

        [HttpPost]
        public ActionResult Create(CurrentClassLocation currentclasslocation)
        {
            if (ModelState.IsValid)
            {
                db.CurrentClassLocations.Add(currentclasslocation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(currentclasslocation);
        }

        //
        // GET: /CurrentClassLocation/Edit/5

        public ActionResult Edit(int id = 0)
        {
            CurrentClassLocation currentclasslocation = db.CurrentClassLocations.Find(id);
            if (currentclasslocation == null)
            {
                return HttpNotFound();
            }
            return View(currentclasslocation);
        }

        //
        // POST: /CurrentClassLocation/Edit/5

        [HttpPost]
        public ActionResult Edit(CurrentClassLocation currentclasslocation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(currentclasslocation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(currentclasslocation);
        }

        //
        // GET: /CurrentClassLocation/Delete/5

        public ActionResult Delete(int id = 0)
        {
            CurrentClassLocation currentclasslocation = db.CurrentClassLocations.Find(id);
            if (currentclasslocation == null)
            {
                return HttpNotFound();
            }
            return View(currentclasslocation);
        }

        //
        // POST: /CurrentClassLocation/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            CurrentClassLocation currentclasslocation = db.CurrentClassLocations.Find(id);
            db.CurrentClassLocations.Remove(currentclasslocation);
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