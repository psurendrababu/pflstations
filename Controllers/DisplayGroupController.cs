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
    public class DisplayGroupController : Controller
    {
        private PipelineFeatureListDBContext db = new PipelineFeatureListDBContext();

        //
        // GET: /DisplayGroup/

        public ActionResult Index()
        {
            return View(db.DisplayGroups.ToList());
        }

        //
        // GET: /DisplayGroup/Details/5

        public ActionResult Details(int id = 0)
        {
            DisplayGroup displaygroup = db.DisplayGroups.Find(id);
            if (displaygroup == null)
            {
                return HttpNotFound();
            }
            return View(displaygroup);
        }

        //
        // GET: /DisplayGroup/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /DisplayGroup/Create

        [HttpPost]
        public ActionResult Create(DisplayGroup displaygroup)
        {
            if (ModelState.IsValid)
            {
                db.DisplayGroups.Add(displaygroup);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(displaygroup);
        }

        //
        // GET: /DisplayGroup/Edit/5

        public ActionResult Edit(int id = 0)
        {
            DisplayGroup displaygroup = db.DisplayGroups.Find(id);
            if (displaygroup == null)
            {
                return HttpNotFound();
            }
            return View(displaygroup);
        }

        //
        // POST: /DisplayGroup/Edit/5

        [HttpPost]
        public ActionResult Edit(DisplayGroup displaygroup)
        {
            if (ModelState.IsValid)
            {
                db.Entry(displaygroup).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(displaygroup);
        }

        //
        // GET: /DisplayGroup/Delete/5

        public ActionResult Delete(int id = 0)
        {
            DisplayGroup displaygroup = db.DisplayGroups.Find(id);
            if (displaygroup == null)
            {
                return HttpNotFound();
            }
            return View(displaygroup);
        }

        //
        // POST: /DisplayGroup/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            DisplayGroup displaygroup = db.DisplayGroups.Find(id);
            db.DisplayGroups.Remove(displaygroup);
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