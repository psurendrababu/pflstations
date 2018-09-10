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
    public class PipeTypeController : Controller
    {
        private PipelineFeatureListDBContext db = new PipelineFeatureListDBContext();

        //
        // GET: /PipeType/

        public ActionResult Index()
        {
            return View(db.PipeTypes.ToList());
        }

        //
        // GET: /PipeType/Details/5

        public ActionResult Details(int id = 0)
        {
            PipeType pipetype = db.PipeTypes.Find(id);
            if (pipetype == null)
            {
                return HttpNotFound();
            }
            return View(pipetype);
        }

        //
        // GET: /PipeType/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /PipeType/Create

        [HttpPost]
        public ActionResult Create(PipeType pipetype)
        {
            if (ModelState.IsValid)
            {
                db.PipeTypes.Add(pipetype);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pipetype);
        }

        //
        // GET: /PipeType/Edit/5

        public ActionResult Edit(int id = 0)
        {
            PipeType pipetype = db.PipeTypes.Find(id);
            if (pipetype == null)
            {
                return HttpNotFound();
            }
            return View(pipetype);
        }

        //
        // POST: /PipeType/Edit/5

        [HttpPost]
        public ActionResult Edit(PipeType pipetype)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pipetype).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pipetype);
        }

        //
        // GET: /PipeType/Delete/5

        public ActionResult Delete(int id = 0)
        {
            PipeType pipetype = db.PipeTypes.Find(id);
            if (pipetype == null)
            {
                return HttpNotFound();
            }
            return View(pipetype);
        }

        //
        // POST: /PipeType/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            PipeType pipetype = db.PipeTypes.Find(id);
            db.PipeTypes.Remove(pipetype);
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