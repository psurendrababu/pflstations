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
    public class HCAController : Controller
    {
        private PipelineFeatureListDBContext db = new PipelineFeatureListDBContext();

        //
        // GET: /HCA/

        public ActionResult Index()
        {
            return View(db.HCAs.ToList());
        }

        //
        // GET: /HCA/Details/5

        public ActionResult Details(int id = 0)
        {
            HCA hca = db.HCAs.Find(id);
            if (hca == null)
            {
                return HttpNotFound();
            }
            return View(hca);
        }

        //
        // GET: /HCA/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /HCA/Create

        [HttpPost]
        public ActionResult Create(HCA hca)
        {
            if (ModelState.IsValid)
            {
                db.HCAs.Add(hca);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hca);
        }

        //
        // GET: /HCA/Edit/5

        public ActionResult Edit(int id = 0)
        {
            HCA hca = db.HCAs.Find(id);
            if (hca == null)
            {
                return HttpNotFound();
            }
            return View(hca);
        }

        //
        // POST: /HCA/Edit/5

        [HttpPost]
        public ActionResult Edit(HCA hca)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hca).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hca);
        }

        //
        // GET: /HCA/Delete/5

        public ActionResult Delete(int id = 0)
        {
            HCA hca = db.HCAs.Find(id);
            if (hca == null)
            {
                return HttpNotFound();
            }
            return View(hca);
        }

        //
        // POST: /HCA/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            HCA hca = db.HCAs.Find(id);
            db.HCAs.Remove(hca);
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