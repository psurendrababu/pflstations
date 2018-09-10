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
    public class DocumentMatController : Controller
    {
        private PipelineFeatureListDBContext db = new PipelineFeatureListDBContext();

        //
        // GET: /DocumentMat/

        public ActionResult Index()
        {
            return View(db.DocumentMats.ToList());
        }

        //
        // GET: /DocumentMat/Details/5

        public ActionResult Details(int id = 0)
        {
            DocumentMat documentmat = db.DocumentMats.Find(id);
            if (documentmat == null)
            {
                return HttpNotFound();
            }
            return View(documentmat);
        }

        //
        // GET: /DocumentMat/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /DocumentMat/Create

        [HttpPost]
        public ActionResult Create(DocumentMat documentmat)
        {
            if (ModelState.IsValid)
            {
                db.DocumentMats.Add(documentmat);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(documentmat);
        }

        //
        // GET: /DocumentMat/Edit/5

        public ActionResult Edit(int id = 0)
        {
            DocumentMat documentmat = db.DocumentMats.Find(id);
            if (documentmat == null)
            {
                return HttpNotFound();
            }
            return View(documentmat);
        }

        //
        // POST: /DocumentMat/Edit/5

        [HttpPost]
        public ActionResult Edit(DocumentMat documentmat)
        {
            if (ModelState.IsValid)
            {
                db.Entry(documentmat).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(documentmat);
        }

        //
        // GET: /DocumentMat/Delete/5

        public ActionResult Delete(int id = 0)
        {
            DocumentMat documentmat = db.DocumentMats.Find(id);
            if (documentmat == null)
            {
                return HttpNotFound();
            }
            return View(documentmat);
        }

        //
        // POST: /DocumentMat/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            DocumentMat documentmat = db.DocumentMats.Find(id);
            db.DocumentMats.Remove(documentmat);
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