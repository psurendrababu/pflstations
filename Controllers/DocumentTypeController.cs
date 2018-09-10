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
    public class DocumentTypeController : Controller
    {
        private PipelineFeatureListDBContext db = new PipelineFeatureListDBContext();

        //
        // GET: /DocumentType/

        public ActionResult Index()
        {
            var documenttypes = db.DocumentTypes.Include(d => d.PipeSystem).Include(d => d.Pipeline);
            return View(documenttypes.ToList());
        }

        //
        // GET: /DocumentType/Details/5

        public ActionResult Details(int id = 0)
        {
            DocumentType documenttype = db.DocumentTypes.Find(id);
            if (documenttype == null)
            {
                return HttpNotFound();
            }
            return View(documenttype);
        }

        //
        // GET: /DocumentType/Create

        public ActionResult Create()
        {
            ViewBag.PipeSystemID = new SelectList(db.PipeSystems, "PipeSystemID", "PipeSystemItem");
            ViewBag.PipeLineID = new SelectList(db.Pipelines, "PipelineID", "PipelineItem");
            return View();
        }

        //
        // POST: /DocumentType/Create

        [HttpPost]
        public ActionResult Create(DocumentType documenttype)
        {
            if (ModelState.IsValid)
            {
                db.DocumentTypes.Add(documenttype);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PipeSystemID = new SelectList(db.PipeSystems, "PipeSystemID", "PipeSystemItem", documenttype.PipeSystemID);
            ViewBag.PipeLineID = new SelectList(db.Pipelines, "PipelineID", "PipelineItem", documenttype.PipeLineID);
            return View(documenttype);
        }

        //
        // GET: /DocumentType/Edit/5

        public ActionResult Edit(int id = 0)
        {
            DocumentType documenttype = db.DocumentTypes.Find(id);
            if (documenttype == null)
            {
                return HttpNotFound();
            }
            ViewBag.PipeSystemID = new SelectList(db.PipeSystems, "PipeSystemID", "PipeSystemItem", documenttype.PipeSystemID);
            ViewBag.PipeLineID = new SelectList(db.Pipelines, "PipelineID", "PipelineItem", documenttype.PipeLineID);
            return View(documenttype);
        }

        //
        // POST: /DocumentType/Edit/5

        [HttpPost]
        public ActionResult Edit(DocumentType documenttype)
        {
            if (ModelState.IsValid)
            {
                db.Entry(documenttype).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PipeSystemID = new SelectList(db.PipeSystems, "PipeSystemID", "PipeSystemItem", documenttype.PipeSystemID);
            ViewBag.PipeLineID = new SelectList(db.Pipelines, "PipelineID", "PipelineItem", documenttype.PipeLineID);
            return View(documenttype);
        }

        //
        // GET: /DocumentType/Delete/5

        public ActionResult Delete(int id = 0)
        {
            DocumentType documenttype = db.DocumentTypes.Find(id);
            if (documenttype == null)
            {
                return HttpNotFound();
            }
            return View(documenttype);
        }

        //
        // POST: /DocumentType/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            DocumentType documenttype = db.DocumentTypes.Find(id);
            db.DocumentTypes.Remove(documenttype);
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