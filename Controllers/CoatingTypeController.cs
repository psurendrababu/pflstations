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
    public class CoatingTypeController : Controller
    {
        private PipelineFeatureListDBContext db = new PipelineFeatureListDBContext();

        //
        // GET: /CoatingType/

        public ActionResult Index()
        {
            return View(db.CoatingTypes.ToList());
        }

        //
        // GET: /CoatingType/Details/5

        public ActionResult Details(int id = 0)
        {
            CoatingType coatingtype = db.CoatingTypes.Find(id);
            if (coatingtype == null)
            {
                return HttpNotFound();
            }
            return View(coatingtype);
        }

        //
        // GET: /CoatingType/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /CoatingType/Create

        [HttpPost]
        public ActionResult Create(CoatingType coatingtype)
        {
            if (ModelState.IsValid)
            {
                db.CoatingTypes.Add(coatingtype);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(coatingtype);
        }

        //
        // GET: /CoatingType/Edit/5

        public ActionResult Edit(int id = 0)
        {
            CoatingType coatingtype = db.CoatingTypes.Find(id);
            if (coatingtype == null)
            {
                return HttpNotFound();
            }
            return View(coatingtype);
        }

        //
        // POST: /CoatingType/Edit/5

        [HttpPost]
        public ActionResult Edit(CoatingType coatingtype)
        {
            if (ModelState.IsValid)
            {
                db.Entry(coatingtype).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(coatingtype);
        }

        //
        // GET: /CoatingType/Delete/5

        public ActionResult Delete(int id = 0)
        {
            CoatingType coatingtype = db.CoatingTypes.Find(id);
            if (coatingtype == null)
            {
                return HttpNotFound();
            }
            return View(coatingtype);
        }

        //
        // POST: /CoatingType/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            CoatingType coatingtype = db.CoatingTypes.Find(id);
            db.CoatingTypes.Remove(coatingtype);
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