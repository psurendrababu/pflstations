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
    public class WorkflowActionController : Controller
    {
        private PipelineFeatureListDBContext db = new PipelineFeatureListDBContext();

        //
        // GET: /WorkflowAction/

        public ActionResult Index()
        {
            return View(db.WorkflowActions.ToList());
        }

        //
        // GET: /WorkflowAction/Details/5

        public ActionResult Details(int id = 0)
        {
            WorkflowAction workflowaction = db.WorkflowActions.Find(id);
            if (workflowaction == null)
            {
                return HttpNotFound();
            }
            return View(workflowaction);
        }

        //
        // GET: /WorkflowAction/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /WorkflowAction/Create

        [HttpPost]
        public ActionResult Create(WorkflowAction workflowaction)
        {
            if (ModelState.IsValid)
            {
                db.WorkflowActions.Add(workflowaction);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(workflowaction);
        }

        //
        // GET: /WorkflowAction/Edit/5

        public ActionResult Edit(int id = 0)
        {
            WorkflowAction workflowaction = db.WorkflowActions.Find(id);
            if (workflowaction == null)
            {
                return HttpNotFound();
            }
            return View(workflowaction);
        }

        //
        // POST: /WorkflowAction/Edit/5

        [HttpPost]
        public ActionResult Edit(WorkflowAction workflowaction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(workflowaction).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(workflowaction);
        }

        //
        // GET: /WorkflowAction/Delete/5

        public ActionResult Delete(int id = 0)
        {
            WorkflowAction workflowaction = db.WorkflowActions.Find(id);
            if (workflowaction == null)
            {
                return HttpNotFound();
            }
            return View(workflowaction);
        }

        //
        // POST: /WorkflowAction/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            WorkflowAction workflowaction = db.WorkflowActions.Find(id);
            db.WorkflowActions.Remove(workflowaction);
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