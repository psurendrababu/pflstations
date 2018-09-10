using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PipelineFeatureList.Models
{
    public class ValveSectionStatusController : Controller
    {
        private PipelineFeatureListDBContext db = new PipelineFeatureListDBContext();

        //
        // GET: /ValveSectionStatus/

        public ActionResult Index()
        {
            return View(db.ValveSectionStatus.ToList());
        }

        //
        // GET: /ValveSectionStatus/Details/5

        public ActionResult Details(int id = 0)
        {
            ValveSectionStatus valvesectionstatus = db.ValveSectionStatus.Find(id);
            if (valvesectionstatus == null)
            {
                return HttpNotFound();
            }
            return View(valvesectionstatus);
        }

        //
        // GET: /ValveSectionStatus/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /ValveSectionStatus/Create

        [HttpPost]
        public ActionResult Create(ValveSectionStatus valvesectionstatus)
        {
            if (ModelState.IsValid)
            {
                db.ValveSectionStatus.Add(valvesectionstatus);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(valvesectionstatus);
        }

        //
        // GET: /ValveSectionStatus/Edit/5

        public ActionResult Edit(int id = 0)
        {
            ValveSectionStatus valvesectionstatus = db.ValveSectionStatus.Find(id);
            if (valvesectionstatus == null)
            {
                return HttpNotFound();
            }
            return View(valvesectionstatus);
        }

        //
        // POST: /ValveSectionStatus/Edit/5

        [HttpPost]
        public ActionResult Edit(ValveSectionStatus valvesectionstatus)
        {
            if (ModelState.IsValid)
            {
                db.Entry(valvesectionstatus).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(valvesectionstatus);
        }

        //
        // GET: /ValveSectionStatus/Delete/5

        public ActionResult Delete(int id = 0)
        {
            ValveSectionStatus valvesectionstatus = db.ValveSectionStatus.Find(id);
            if (valvesectionstatus == null)
            {
                return HttpNotFound();
            }
            return View(valvesectionstatus);
        }

        //
        // POST: /ValveSectionStatus/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            ValveSectionStatus valvesectionstatus = db.ValveSectionStatus.Find(id);
            db.ValveSectionStatus.Remove(valvesectionstatus);
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