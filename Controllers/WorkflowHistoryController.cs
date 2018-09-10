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
    public class WorkflowHistoryController : Controller
    {
        private PipelineFeatureListDBContext db = new PipelineFeatureListDBContext();

        //
        // GET: /WorkflowHistory/

        public ActionResult Index()
        {
            return View(db.WorkflowHistories.ToList());
        }

        //
        // GET: /WorkflowHistory/Details/5

        public ActionResult Details(int id = 0)
        {
            WorkflowHistory workflowhistory = db.WorkflowHistories.Find(id);
            if (workflowhistory == null)
            {
                return HttpNotFound();
            }
            return View(workflowhistory);
        }

        //
        // GET: /WorkflowHistory/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /WorkflowHistory/Create

        [HttpPost]
        public ActionResult Create(WorkflowHistory workflowhistory)
        {
            if (ModelState.IsValid)
            {
                db.WorkflowHistories.Add(workflowhistory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(workflowhistory);
        }

        //
        // GET: /WorkflowHistory/Edit/5

        public ActionResult Edit(int id = 0)
        {
            WorkflowHistory workflowhistory = db.WorkflowHistories.Find(id);
            if (workflowhistory == null)
            {
                return HttpNotFound();
            }
            return View(workflowhistory);
        }

        //
        // POST: /WorkflowHistory/Edit/5

        [HttpPost]
        public ActionResult Edit(WorkflowHistory workflowhistory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(workflowhistory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(workflowhistory);
        }

        //
        // GET: /WorkflowHistory/Delete/5

        public ActionResult Delete(int id = 0)
        {
            WorkflowHistory workflowhistory = db.WorkflowHistories.Find(id);
            if (workflowhistory == null)
            {
                return HttpNotFound();
            }
            return View(workflowhistory);
        }

        //
        // POST: /WorkflowHistory/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            WorkflowHistory workflowhistory = db.WorkflowHistories.Find(id);
            db.WorkflowHistories.Remove(workflowhistory);
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