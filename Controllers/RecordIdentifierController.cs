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
    public class RecordIdentifierController : Controller
    {
        private PipelineFeatureListDBContext db = new PipelineFeatureListDBContext();

        //
        // GET: /RecordIdentifier/

        public ActionResult Index()
        {
            return View(db.RecordIdentifiers.ToList());
        }

        //
        // GET: /RecordIdentifier/Details/5

        public ActionResult Details(int id = 0)
        {
            RecordIdentifier recordidentifier = db.RecordIdentifiers.Find(id);
            if (recordidentifier == null)
            {
                return HttpNotFound();
            }
            return View(recordidentifier);
        }

        //
        // GET: /RecordIdentifier/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /RecordIdentifier/Create

        [HttpPost]
        public ActionResult Create(RecordIdentifier recordidentifier)
        {
            if (ModelState.IsValid)
            {
                db.RecordIdentifiers.Add(recordidentifier);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(recordidentifier);
        }

        //
        // GET: /RecordIdentifier/Edit/5

        public ActionResult Edit(int id = 0)
        {
            RecordIdentifier recordidentifier = db.RecordIdentifiers.Find(id);
            if (recordidentifier == null)
            {
                return HttpNotFound();
            }
            return View(recordidentifier);
        }

        //
        // POST: /RecordIdentifier/Edit/5

        [HttpPost]
        public ActionResult Edit(RecordIdentifier recordidentifier)
        {
            if (ModelState.IsValid)
            {
                db.Entry(recordidentifier).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(recordidentifier);
        }

        //
        // GET: /RecordIdentifier/Delete/5

        public ActionResult Delete(int id = 0)
        {
            RecordIdentifier recordidentifier = db.RecordIdentifiers.Find(id);
            if (recordidentifier == null)
            {
                return HttpNotFound();
            }
            return View(recordidentifier);
        }

        //
        // POST: /RecordIdentifier/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            RecordIdentifier recordidentifier = db.RecordIdentifiers.Find(id);
            db.RecordIdentifiers.Remove(recordidentifier);
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