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
    public class ValveSectionErrorLevelController : Controller
    {
        private PipelineFeatureListDBContext db = new PipelineFeatureListDBContext();

        //
        // GET: /ValveSectionErrorLevel/

        public ActionResult Index()
        {
            return View(db.ValveSectionErrorLevels.ToList());
        }

        //
        // GET: /ValveSectionErrorLevel/Details/5

        public ActionResult Details(int id = 0)
        {
            ValveSectionErrorLevel valvesectionerrorlevel = db.ValveSectionErrorLevels.Find(id);
            if (valvesectionerrorlevel == null)
            {
                return HttpNotFound();
            }
            return View(valvesectionerrorlevel);
        }

        //
        // GET: /ValveSectionErrorLevel/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /ValveSectionErrorLevel/Create

        [HttpPost]
        public ActionResult Create(ValveSectionErrorLevel valvesectionerrorlevel)
        {
            if (ModelState.IsValid)
            {
                db.ValveSectionErrorLevels.Add(valvesectionerrorlevel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(valvesectionerrorlevel);
        }

        //
        // GET: /ValveSectionErrorLevel/Edit/5

        public ActionResult Edit(int id = 0)
        {
            ValveSectionErrorLevel valvesectionerrorlevel = db.ValveSectionErrorLevels.Find(id);
            if (valvesectionerrorlevel == null)
            {
                return HttpNotFound();
            }
            return View(valvesectionerrorlevel);
        }

        //
        // POST: /ValveSectionErrorLevel/Edit/5

        [HttpPost]
        public ActionResult Edit(ValveSectionErrorLevel valvesectionerrorlevel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(valvesectionerrorlevel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(valvesectionerrorlevel);
        }

        //
        // GET: /ValveSectionErrorLevel/Delete/5

        public ActionResult Delete(int id = 0)
        {
            ValveSectionErrorLevel valvesectionerrorlevel = db.ValveSectionErrorLevels.Find(id);
            if (valvesectionerrorlevel == null)
            {
                return HttpNotFound();
            }
            return View(valvesectionerrorlevel);
        }

        //
        // POST: /ValveSectionErrorLevel/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            ValveSectionErrorLevel valvesectionerrorlevel = db.ValveSectionErrorLevels.Find(id);
            db.ValveSectionErrorLevels.Remove(valvesectionerrorlevel);
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