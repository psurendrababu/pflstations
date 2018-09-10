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
    public class StandardEntryController : Controller
    {
        private PipelineFeatureListDBContext db = new PipelineFeatureListDBContext();

        //
        // GET: /StandardEntry/

        public ActionResult Index()
        {
            return View(db.StandardEntries.ToList());
        }

        //
        // GET: /StandardEntry/Details/5

        public ActionResult Details(int id = 0)
        {
            StandardEntry standardentry = db.StandardEntries.Find(id);
            if (standardentry == null)
            {
                return HttpNotFound();
            }
            return View(standardentry);
        }

        //
        // GET: /StandardEntry/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /StandardEntry/Create

        [HttpPost]
        public ActionResult Create(StandardEntry standardentry)
        {
            if (ModelState.IsValid)
            {
                db.StandardEntries.Add(standardentry);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(standardentry);
        }

        //
        // GET: /StandardEntry/Edit/5

        public ActionResult Edit(int id = 0)
        {
            StandardEntry standardentry = db.StandardEntries.Find(id);
            if (standardentry == null)
            {
                return HttpNotFound();
            }
            return View(standardentry);
        }

        //
        // POST: /StandardEntry/Edit/5

        [HttpPost]
        public ActionResult Edit(StandardEntry standardentry)
        {
            if (ModelState.IsValid)
            {
                db.Entry(standardentry).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(standardentry);
        }

        //
        // GET: /StandardEntry/Delete/5

        public ActionResult Delete(int id = 0)
        {
            StandardEntry standardentry = db.StandardEntries.Find(id);
            if (standardentry == null)
            {
                return HttpNotFound();
            }
            return View(standardentry);
        }

        //
        // POST: /StandardEntry/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            StandardEntry standardentry = db.StandardEntries.Find(id);
            db.StandardEntries.Remove(standardentry);
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