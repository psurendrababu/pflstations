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
    public class ReportEmailController : Controller
    {
        private PipelineFeatureListDBContext db = new PipelineFeatureListDBContext();

        //
        // GET: /ReportEmail/

        public ActionResult Index()
        {
            var reportemails = db.ReportEmails.Include(r => r.User).OrderBy(r => r.User.UserName).Include(r => r.Report).OrderBy(r => r.Report.ReportName);
            return View(reportemails.ToList());
        }

        //
        // GET: /ReportEmail/Details/5

        public ActionResult Details(long id = 0)
        {
            ReportEmail reportemail = db.ReportEmails.Find(id);
            if (reportemail == null)
            {
                return HttpNotFound();
            }
            return View(reportemail);
        }

        //
        // GET: /ReportEmail/Create

        public ActionResult Create()
        {
            List<Report> reportList = db.Reports.OrderBy(r => r.ReportName).ToList();
            SelectList ReportList = new SelectList(reportList, "ReportID", "ReportName");
            ViewBag.ReportID = ReportList;
            var userList = db.Users.OrderBy(u => u.LastName).ThenBy(u => u.FirstName)
                    .Select(u => new { Value = u.UserID, Text = u.LastName + ", " + u.FirstName }).ToList();
            SelectList UserList = new SelectList(userList, "Value", "Text"); 
            ViewBag.UserID = UserList; 
            return View();
        }

        //
        // POST: /ReportEmail/Create

        [HttpPost]
        public ActionResult Create(ReportEmail reportemail)
        {
            if (ModelState.IsValid)
            {
                db.ReportEmails.Add(reportemail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            List<Report> reportList = db.Reports.OrderBy(r => r.ReportName).ToList();
            SelectList ReportList = new SelectList(reportList, "ReportID", "ReportName", reportemail.ReportID);
            ViewBag.ReportID = ReportList;
            var userList = db.Users.OrderBy(u => u.LastName).ThenBy(u => u.FirstName)
                    .Select(u => new { Value = u.UserID, Text = u.LastName + ", " + u.FirstName }).ToList();
            SelectList UserList = new SelectList(userList, "Value", "Text", reportemail.UserID); 
            return View(reportemail);
        }

        //
        // GET: /ReportEmail/Edit/5

        public ActionResult Edit(long id = 0)
        {
            ReportEmail reportemail = db.ReportEmails.Find(id);
            if (reportemail == null)
            {
                return HttpNotFound();
            }
            List<Report> reportList = db.Reports.OrderBy(r => r.ReportName).ToList();
            SelectList ReportList = new SelectList(reportList, "ReportID", "ReportName", reportemail.ReportID);
            ViewBag.ReportID = ReportList;
            var userList = db.Users.OrderBy(u => u.LastName).ThenBy(u => u.FirstName)
                    .Select(u => new { Value = u.UserID, Text = u.LastName + ", " + u.FirstName }).ToList();
            SelectList UserList = new SelectList(userList, "Value", "Text", reportemail.UserID); 
            ViewBag.UserID = UserList; 
            return View(reportemail);
        }

        //
        // POST: /ReportEmail/Edit/5

        [HttpPost]
        public ActionResult Edit(ReportEmail reportemail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reportemail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            List<Report> reportList = db.Reports.OrderBy(r => r.ReportName).ToList();
            SelectList ReportList = new SelectList(reportList, "ReportID", "ReportName", reportemail.ReportID);
            ViewBag.ReportID = ReportList;
            var userList = db.Users.OrderBy(u => u.LastName).ThenBy(u => u.FirstName)
                    .Select(u => new { Value = u.UserID, Text = u.LastName + ", " + u.FirstName }).ToList();
            SelectList UserList = new SelectList(userList, "Value", "Text", reportemail.UserID);
            ViewBag.UserID = UserList; return View(reportemail);
        }

        //
        // GET: /ReportEmail/Delete/5

        public ActionResult Delete(long id = 0)
        {
            ReportEmail reportemail = db.ReportEmails.Find(id);
            if (reportemail == null)
            {
                return HttpNotFound();
            }
            return View(reportemail);
        }

        //
        // POST: /ReportEmail/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            ReportEmail reportemail = db.ReportEmails.Find(id);
            db.ReportEmails.Remove(reportemail);
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