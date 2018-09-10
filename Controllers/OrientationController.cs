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
    public class OrientationController : Controller
    {
        private PipelineFeatureListDBContext db = new PipelineFeatureListDBContext();

        //
        // GET: /Orientation/

        public ActionResult Index()
        {
            return View(db.Orientations.ToList());
        }

        //
        // GET: /Orientation/Details/5

        public ActionResult Details(int id = 0)
        {
            Orientation orientation = db.Orientations.Find(id);
            if (orientation == null)
            {
                return HttpNotFound();
            }
            return View(orientation);
        }

        //
        // GET: /Orientation/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Orientation/Create

        [HttpPost]
        public ActionResult Create(Orientation orientation)
        {
            if (ModelState.IsValid)
            {
                db.Orientations.Add(orientation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(orientation);
        }

        //
        // GET: /Orientation/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Orientation orientation = db.Orientations.Find(id);
            if (orientation == null)
            {
                return HttpNotFound();
            }
            return View(orientation);
        }

        //
        // POST: /Orientation/Edit/5

        [HttpPost]
        public ActionResult Edit(Orientation orientation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orientation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(orientation);
        }

        //
        // GET: /Orientation/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Orientation orientation = db.Orientations.Find(id);
            if (orientation == null)
            {
                return HttpNotFound();
            }
            return View(orientation);
        }

        //
        // POST: /Orientation/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Orientation orientation = db.Orientations.Find(id);
            db.Orientations.Remove(orientation);
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