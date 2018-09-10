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
    public class ReportingGroupController : Controller
    {
        private PipelineFeatureListDBContext db = new PipelineFeatureListDBContext();

        //
        // GET: /ReportingGroup/

        public ActionResult Index()
        {
            return View(db.ReportingGroups.ToList());
        }

        //
        // GET: /ReportingGroup/Details/5

        public ActionResult Details(int id = 0)
        {
            ReportingGroup reportinggroup = db.ReportingGroups.Find(id);
            if (reportinggroup == null)
            {
                return HttpNotFound();
            }
            return View(reportinggroup);
        }

        //
        // GET: /ReportingGroup/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /ReportingGroup/Create

        [HttpPost]
        public ActionResult Create(ReportingGroup reportinggroup)
        {
            if (ModelState.IsValid)
            {
                db.ReportingGroups.Add(reportinggroup);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(reportinggroup);
        }

        //
        // GET: /ReportingGroup/Edit/5

        public ActionResult Edit(int id = 0)
        {
            ReportingGroup reportinggroup = db.ReportingGroups.Find(id);
            if (reportinggroup == null)
            {
                return HttpNotFound();
            }
            return View(reportinggroup);
        }

        //
        // POST: /ReportingGroup/Edit/5

        [HttpPost]
        public ActionResult Edit(ReportingGroup reportinggroup)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reportinggroup).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(reportinggroup);
        }

        //
        // GET: /ReportingGroup/Delete/5

        public ActionResult Delete(int id = 0)
        {
            ReportingGroup reportinggroup = db.ReportingGroups.Find(id);
            if (reportinggroup == null)
            {
                return HttpNotFound();
            }
            return View(reportinggroup);
        }

        //
        // POST: /ReportingGroup/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            ReportingGroup reportinggroup = db.ReportingGroups.Find(id);
            db.ReportingGroups.Remove(reportinggroup);
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