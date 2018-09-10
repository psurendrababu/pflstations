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
    public class CertificationApprovedController : Controller
    {
        private PipelineFeatureListDBContext db = new PipelineFeatureListDBContext();

        //
        // GET: /CertificationApproved/

        public ActionResult Index()
        {
            return View(db.CertificationApproveds.ToList());
        }

        //
        // GET: /CertificationApproved/Details/5

        public ActionResult Details(long id = 0)
        {
            CertificationApproved certificationapproved = db.CertificationApproveds.Find(id);
            if (certificationapproved == null)
            {
                return HttpNotFound();
            }
            return View(certificationapproved);
        }

        //
        // GET: /CertificationApproved/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /CertificationApproved/Create

        [HttpPost]
        public ActionResult Create(CertificationApproved certificationapproved)
        {
            if (ModelState.IsValid)
            {
                db.CertificationApproveds.Add(certificationapproved);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(certificationapproved);
        }

        //
        // GET: /CertificationApproved/Edit/5

        public ActionResult Edit(long id = 0)
        {
            CertificationApproved certificationapproved = db.CertificationApproveds.Find(id);
            if (certificationapproved == null)
            {
                return HttpNotFound();
            }
            return View(certificationapproved);
        }

        //
        // POST: /CertificationApproved/Edit/5

        [HttpPost]
        public ActionResult Edit(CertificationApproved certificationapproved)
        {
            if (ModelState.IsValid)
            {
                db.Entry(certificationapproved).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(certificationapproved);
        }

        //
        // GET: /CertificationApproved/Delete/5

        public ActionResult Delete(long id = 0)
        {
            CertificationApproved certificationapproved = db.CertificationApproveds.Find(id);
            if (certificationapproved == null)
            {
                return HttpNotFound();
            }
            return View(certificationapproved);
        }

        //
        // POST: /CertificationApproved/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            CertificationApproved certificationapproved = db.CertificationApproveds.Find(id);
            db.CertificationApproveds.Remove(certificationapproved);
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