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
    public class WorkflowRuleController : Controller
    {
        private PipelineFeatureListDBContext db = new PipelineFeatureListDBContext();

        //
        // GET: /WorkflowRule/

        public ActionResult Index()
        {
            return View(db.WorkflowRules.ToList());
        }

        //
        // GET: /WorkflowRule/Details/5

        public ActionResult Details(int id = 0)
        {
            WorkflowRule workflowrule = db.WorkflowRules.Find(id);
            if (workflowrule == null)
            {
                return HttpNotFound();
            }
            return View(workflowrule);
        }

        //
        // GET: /WorkflowRule/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /WorkflowRule/Create

        [HttpPost]
        public ActionResult Create(WorkflowRule workflowrule)
        {
            if (ModelState.IsValid)
            {
                db.WorkflowRules.Add(workflowrule);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(workflowrule);
        }

        //
        // GET: /WorkflowRule/Edit/5

        public ActionResult Edit(int id = 0)
        {
            WorkflowRule workflowrule = db.WorkflowRules.Find(id);
            if (workflowrule == null)
            {
                return HttpNotFound();
            }
            return View(workflowrule);
        }

        //
        // POST: /WorkflowRule/Edit/5

        [HttpPost]
        public ActionResult Edit(WorkflowRule workflowrule)
        {
            if (ModelState.IsValid)
            {
                db.Entry(workflowrule).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(workflowrule);
        }

        //
        // GET: /WorkflowRule/Delete/5

        public ActionResult Delete(int id = 0)
        {
            WorkflowRule workflowrule = db.WorkflowRules.Find(id);
            if (workflowrule == null)
            {
                return HttpNotFound();
            }
            return View(workflowrule);
        }

        //
        // POST: /WorkflowRule/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            WorkflowRule workflowrule = db.WorkflowRules.Find(id);
            db.WorkflowRules.Remove(workflowrule);
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