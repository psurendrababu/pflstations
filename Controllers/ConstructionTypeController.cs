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
    public class ConstructionTypeController : Controller
    {
        private PipelineFeatureListDBContext db = new PipelineFeatureListDBContext();

        //
        // GET: /ConstructionType/

        public ActionResult Index()
        {
            return View(db.ConstructionTypes.ToList());
        }

        //
        // GET: /ConstructionType/Details/5

        public ActionResult Details(int id = 0)
        {
            ConstructionType constructiontype = db.ConstructionTypes.Find(id);
            if (constructiontype == null)
            {
                return HttpNotFound();
            }
            return View(constructiontype);
        }

        //
        // GET: /ConstructionType/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /ConstructionType/Create

        [HttpPost]
        public ActionResult Create(ConstructionType constructiontype)
        {
            if (ModelState.IsValid)
            {
                db.ConstructionTypes.Add(constructiontype);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(constructiontype);
        }

        //
        // GET: /ConstructionType/Edit/5

        public ActionResult Edit(int id = 0)
        {
            ConstructionType constructiontype = db.ConstructionTypes.Find(id);
            if (constructiontype == null)
            {
                return HttpNotFound();
            }
            return View(constructiontype);
        }

        //
        // POST: /ConstructionType/Edit/5

        [HttpPost]
        public ActionResult Edit(ConstructionType constructiontype)
        {
            if (ModelState.IsValid)
            {
                db.Entry(constructiontype).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(constructiontype);
        }

        //
        // GET: /ConstructionType/Delete/5

        public ActionResult Delete(int id = 0)
        {
            ConstructionType constructiontype = db.ConstructionTypes.Find(id);
            if (constructiontype == null)
            {
                return HttpNotFound();
            }
            return View(constructiontype);
        }

        //
        // POST: /ConstructionType/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            ConstructionType constructiontype = db.ConstructionTypes.Find(id);
            db.ConstructionTypes.Remove(constructiontype);
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