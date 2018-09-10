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
    public class ManufacturerTypeController : Controller
    {
        private PipelineFeatureListDBContext db = new PipelineFeatureListDBContext();

        //
        // GET: /ManufacturerType/

        public ActionResult Index()
        {
            return View(db.ManufacturerTypes.ToList());
        }

        //
        // GET: /ManufacturerType/Details/5

        public ActionResult Details(long id = 0)
        {
            ManufacturerType manufacturertype = db.ManufacturerTypes.Find(id);
            if (manufacturertype == null)
            {
                return HttpNotFound();
            }
            return View(manufacturertype);
        }

        //
        // GET: /ManufacturerType/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /ManufacturerType/Create

        [HttpPost]
        public ActionResult Create(ManufacturerType manufacturertype)
        {
            if (ModelState.IsValid)
            {
                db.ManufacturerTypes.Add(manufacturertype);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(manufacturertype);
        }

        //
        // GET: /ManufacturerType/Edit/5

        public ActionResult Edit(long id = 0)
        {
            ManufacturerType manufacturertype = db.ManufacturerTypes.Find(id);
            if (manufacturertype == null)
            {
                return HttpNotFound();
            }
            return View(manufacturertype);
        }

        //
        // POST: /ManufacturerType/Edit/5

        [HttpPost]
        public ActionResult Edit(ManufacturerType manufacturertype)
        {
            if (ModelState.IsValid)
            {
                db.Entry(manufacturertype).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(manufacturertype);
        }

        //
        // GET: /ManufacturerType/Delete/5

        public ActionResult Delete(long id = 0)
        {
            ManufacturerType manufacturertype = db.ManufacturerTypes.Find(id);
            if (manufacturertype == null)
            {
                return HttpNotFound();
            }
            return View(manufacturertype);
        }

        //
        // POST: /ManufacturerType/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            ManufacturerType manufacturertype = db.ManufacturerTypes.Find(id);
            db.ManufacturerTypes.Remove(manufacturertype);
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