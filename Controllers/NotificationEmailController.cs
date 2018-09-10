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
    public class NotificationEmailController : Controller
    {
        private PipelineFeatureListDBContext db = new PipelineFeatureListDBContext();

        //
        // GET: /NotificationEmail/

        public ActionResult Index()
        {
            return View(db.NotificationEmails.ToList());
        }

        //
        // GET: /NotificationEmail/Details/5

        public ActionResult Details(long id = 0)
        {
            NotificationEmail notificationemail = db.NotificationEmails.Find(id);
            if (notificationemail == null)
            {
                return HttpNotFound();
            }
            return View(notificationemail);
        }

        //
        // GET: /NotificationEmail/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /NotificationEmail/Create

        [HttpPost]
        public ActionResult Create(NotificationEmail notificationemail)
        {
            if (ModelState.IsValid)
            {
                db.NotificationEmails.Add(notificationemail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(notificationemail);
        }

        //
        // GET: /NotificationEmail/Edit/5

        public ActionResult Edit(long id = 0)
        {
            NotificationEmail notificationemail = db.NotificationEmails.Find(id);
            if (notificationemail == null)
            {
                return HttpNotFound();
            }
            return View(notificationemail);
        }

        //
        // POST: /NotificationEmail/Edit/5

        [HttpPost]
        public ActionResult Edit(NotificationEmail notificationemail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(notificationemail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(notificationemail);
        }

        //
        // GET: /NotificationEmail/Delete/5

        public ActionResult Delete(long id = 0)
        {
            NotificationEmail notificationemail = db.NotificationEmails.Find(id);
            if (notificationemail == null)
            {
                return HttpNotFound();
            }
            return View(notificationemail);
        }

        //
        // POST: /NotificationEmail/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            NotificationEmail notificationemail = db.NotificationEmails.Find(id);
            db.NotificationEmails.Remove(notificationemail);
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