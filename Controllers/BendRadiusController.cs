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
    public class BendRadiusController : Controller
    {
        private PipelineFeatureListDBContext db = new PipelineFeatureListDBContext();

        //
        // GET: /BendRadius/

        public ActionResult Index()
        {
            return View(db.BendRadiuses.ToList());
        }

        //
        // GET: /BendRadius/Details/5

        public ActionResult Details(int id = 0)
        {
            BendRadius bendradius = db.BendRadiuses.Find(id);
            if (bendradius == null)
            {
                return HttpNotFound();
            }
            return View(bendradius);
        }

        //
        // GET: /BendRadius/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /BendRadius/Create

        [HttpPost]
        public ActionResult Create(BendRadius bendradius)
        {
            if (ModelState.IsValid)
            {
                db.BendRadiuses.Add(bendradius);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bendradius);
        }

        //
        // GET: /BendRadius/Edit/5

        public ActionResult Edit(int id = 0)
        {
            BendRadius bendradius = db.BendRadiuses.Find(id);
            if (bendradius == null)
            {
                return HttpNotFound();
            }
            return View(bendradius);
        }

        //
        // POST: /BendRadius/Edit/5

        [HttpPost]
        public ActionResult Edit(BendRadius bendradius)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bendradius).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bendradius);
        }

        //
        // GET: /BendRadius/Delete/5

        public ActionResult Delete(int id = 0)
        {
            BendRadius bendradius = db.BendRadiuses.Find(id);
            if (bendradius == null)
            {
                return HttpNotFound();
            }
            return View(bendradius);
        }

        //
        // POST: /BendRadius/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            BendRadius bendradius = db.BendRadiuses.Find(id);
            db.BendRadiuses.Remove(bendradius);
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