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
    public class DocumentRecordController : Controller
    {
        private PipelineFeatureListDBContext db = new PipelineFeatureListDBContext();

        //
        // GET: /DocumentRecord/

        public ActionResult Index()
        {
            var documentrecords = db.DocumentRecords.Include(d => d.ValveSection).Include(d => d.DocumentType).Include(d => d.RecordIdentifier);
            return View(documentrecords.ToList());
        }

        //
        // GET: /DocumentRecord/Details/5

        public ActionResult Details(int id = 0)
        {
            DocumentRecord documentrecord = db.DocumentRecords.Find(id);
            if (documentrecord == null)
            {
                return HttpNotFound();
            }
            return View(documentrecord);
        }

        //
        // GET: /DocumentRecord/Create

        public ActionResult Create()
        {
            ViewBag.ValveSectionID = new SelectList(db.ValveSection, "ValveSectionID", "OrionStationSeries");
            ViewBag.DocumentTypeID = new SelectList(db.DocumentTypes, "DocumentTypeID", "DocumentTypeItem");
            ViewBag.RecordIdentifierID = new SelectList(db.RecordIdentifiers, "RecordIdentifierID", "RecordIdentifierItem");
            return View();
        }

        //
        // POST: /DocumentRecord/Create

        [HttpPost]
        public ActionResult Create(DocumentRecord documentrecord)
        {
            if (ModelState.IsValid)
            {
                db.DocumentRecords.Add(documentrecord);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ValveSectionID = new SelectList(db.ValveSection, "ValveSectionID", "OrionStationSeries", documentrecord.ValveSectionID);
            ViewBag.DocumentTypeID = new SelectList(db.DocumentTypes, "DocumentTypeID", "DocumentTypeItem", documentrecord.DocumentTypeID);
            ViewBag.RecordIdentifierID = new SelectList(db.RecordIdentifiers, "RecordIdentifierID", "RecordIdentifierItem", documentrecord.RecordIdentifierID);
            return View(documentrecord);
        }

        //
        // GET: /DocumentRecord/Edit/5

        public ActionResult Edit(int id = 0)
        {
            DocumentRecord documentrecord = db.DocumentRecords.Find(id);
            if (documentrecord == null)
            {
                return HttpNotFound();
            }
            ViewBag.ValveSectionID = new SelectList(db.ValveSection, "ValveSectionID", "OrionStationSeries", documentrecord.ValveSectionID);
            ViewBag.DocumentTypeID = new SelectList(db.DocumentTypes, "DocumentTypeID", "DocumentTypeItem", documentrecord.DocumentTypeID);
            ViewBag.RecordIdentifierID = new SelectList(db.RecordIdentifiers, "RecordIdentifierID", "RecordIdentifierItem", documentrecord.RecordIdentifierID);
            return View(documentrecord);
        }

        //
        // POST: /DocumentRecord/Edit/5

        [HttpPost]
        public ActionResult Edit(DocumentRecord documentrecord)
        {
            if (ModelState.IsValid)
            {
                db.Entry(documentrecord).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ValveSectionID = new SelectList(db.ValveSection, "ValveSectionID", "OrionStationSeries", documentrecord.ValveSectionID);
            ViewBag.DocumentTypeID = new SelectList(db.DocumentTypes, "DocumentTypeID", "DocumentTypeItem", documentrecord.DocumentTypeID);
            ViewBag.RecordIdentifierID = new SelectList(db.RecordIdentifiers, "RecordIdentifierID", "RecordIdentifierItem", documentrecord.RecordIdentifierID);
            return View(documentrecord);
        }

        //
        // GET: /DocumentRecord/Delete/5

        public ActionResult Delete(int id = 0)
        {
            DocumentRecord documentrecord = db.DocumentRecords.Find(id);
            if (documentrecord == null)
            {
                return HttpNotFound();
            }
            return View(documentrecord);
        }

        //
        // POST: /DocumentRecord/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            DocumentRecord documentrecord = db.DocumentRecords.Find(id);
            db.DocumentRecords.Remove(documentrecord);
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