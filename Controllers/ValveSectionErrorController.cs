using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PipelineFeatureList.Models
{
    public class ValveSectionErrorController : Controller
    {
        private PipelineFeatureListDBContext db = new PipelineFeatureListDBContext();

        //
        // GET: /ValveSectionError/

        public ActionResult Index()
        {
            return View(db.ValveSectionErrors.ToList());
        }

        //
        // GET: /ValveSectionError/Details/5

        public ActionResult Details(int id = 0)
        {
            ValveSectionError valvesectionerror = db.ValveSectionErrors.Find(id);
            if (valvesectionerror == null)
            {
                return HttpNotFound();
            }
            return View(valvesectionerror);
        }

        //
        // GET: /ValveSectionError/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /ValveSectionError/Create

        [HttpPost]
        public ActionResult Create(ValveSectionError valvesectionerror)
        {
            if (ModelState.IsValid)
            {
                db.ValveSectionErrors.Add(valvesectionerror);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(valvesectionerror);
        }

        //
        // GET: /ValveSectionError/Edit/5

        public ActionResult Edit(int id = 0)
        {
            ValveSectionError valvesectionerror = db.ValveSectionErrors.Find(id);
            if (valvesectionerror == null)
            {
                return HttpNotFound();
            }
            return View(valvesectionerror);
        }

        //
        // POST: /ValveSectionError/Edit/5

        [HttpPost]
        public ActionResult Edit(ValveSectionError valvesectionerror)
        {
            if (ModelState.IsValid)
            {
                db.Entry(valvesectionerror).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(valvesectionerror);
        }

        //
        // GET: /ValveSectionError/Delete/5

        public ActionResult Delete(int id = 0)
        {
            ValveSectionError valvesectionerror = db.ValveSectionErrors.Find(id);
            if (valvesectionerror == null)
            {
                return HttpNotFound();
            }
            return View(valvesectionerror);
        }

        //
        // POST: /ValveSectionError/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            ValveSectionError valvesectionerror = db.ValveSectionErrors.Find(id);
            db.ValveSectionErrors.Remove(valvesectionerror);
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