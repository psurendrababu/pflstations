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
    public class PipeSystemController : Controller
    {
        private PipelineFeatureListDBContext db = new PipelineFeatureListDBContext();

        //
        // GET: /PipeSystem/

        public ActionResult Index()
        {
            return View(db.PipeSystems.ToList());
        }

        //
        // GET: /PipeSystem/Details/5

        public ActionResult Details(int id = 0)
        {
            PipeSystem pipesystem = db.PipeSystems.Find(id);
            if (pipesystem == null)
            {
                return HttpNotFound();
            }
            return View(pipesystem);
        }

        //
        // GET: /PipeSystem/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /PipeSystem/Create

        [HttpPost]
        public ActionResult Create(PipeSystem pipesystem)
        {
            if (ModelState.IsValid)
            {
                db.PipeSystems.Add(pipesystem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pipesystem);
        }

        //
        // GET: /PipeSystem/Edit/5

        public ActionResult Edit(int id = 0)
        {
            PipeSystem pipesystem = db.PipeSystems.Find(id);
            var psfeatures = (from vf in db.ValveSection
                              where vf.PipeSystemID == pipesystem.PipeSystemID
                              select new
                              {
                                  vf
                              }).ToList();


            if (psfeatures.Count > 0)
            {
                ModelState.AddModelError("PipeSystemItem", "Warning! This Station Location is assigned to Circuit(s).");
                ViewBag.HasError = "True";
            }
            if (pipesystem == null)
            {
                return HttpNotFound();
            }
            return View(pipesystem);
        }

        //
        // POST: /PipeSystem/Edit/5

        [HttpPost]
        public ActionResult Edit(PipeSystem pipesystem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pipesystem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pipesystem);
        }

        //
        // GET: /PipeSystem/Delete/5

        public ActionResult Delete(int id = 0)
        {
            PipeSystem pipesystem = db.PipeSystems.Find(id);
            var psfeatures = (from vf in db.ValveSection
                              where vf.PipeSystemID == pipesystem.PipeSystemID
                              select new
                              {
                                  vf
                              }).ToList();


            if (psfeatures.Count > 0)
            {
                ModelState.AddModelError("PipeSystemItem", "This Station Location is assigned to Circuit(s) and cannot be deleted.");
                ViewBag.HasError = "True";
            }

            if (pipesystem == null)
            {
                return HttpNotFound();
            }
            return View(pipesystem);
        }

        //
        // POST: /PipeSystem/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            PipeSystem pipesystem = db.PipeSystems.Find(id);
            db.PipeSystems.Remove(pipesystem);
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