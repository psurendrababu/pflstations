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
    public class ManufacturerController : Controller
    {
        private PipelineFeatureListDBContext db = new PipelineFeatureListDBContext();

        //
        // GET: /Manufacturer/

        public ActionResult Index()
        {
            return View(db.Manufacturers.OrderBy(m => m.ManufacturerItem).ToList());
        }

        //
        // GET: /Manufacturer/Details/5

        public ActionResult Details(int id = 0)
        {
            Manufacturer manufacturer = db.Manufacturers.Find(id);
            if (manufacturer == null)
            {
                return HttpNotFound();
            }
            return View(manufacturer);
        }

        //
        // GET: /Manufacturer/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Manufacturer/Create

        [HttpPost]
        public ActionResult Create(Manufacturer manufacturer)
        {
            if (ModelState.IsValid)
            {
                manufacturer.CreatedBy_UserID = Convert.ToInt64(Session["UserID"].ToString());
                manufacturer.ModifiedBy_UserID = Convert.ToInt64(Session["UserID"].ToString());
                manufacturer.CreatedOn = DateTime.Now;
                manufacturer.ModifiedOn = DateTime.Now;

                db.Manufacturers.Add(manufacturer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(manufacturer);
        }

        //
        // GET: /Manufacturer/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Manufacturer manufacturer = db.Manufacturers.Find(id);
            if (manufacturer == null)
            {
                return HttpNotFound();
            }
            return View(manufacturer);
        }

        //
        // POST: /Manufacturer/Edit/5

        [HttpPost]
        public ActionResult Edit(Manufacturer manufacturer)
        {
            if (ModelState.IsValid)
            {
                manufacturer.ModifiedBy_UserID = Convert.ToInt64(Session["UserID"].ToString());
                manufacturer.ModifiedOn = DateTime.Now;
                
                db.Entry(manufacturer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(manufacturer);
        }

        //
        // GET: /Manufacturer/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Manufacturer manufacturer = db.Manufacturers.Find(id);
            if (manufacturer == null)
            {
                return HttpNotFound();
            }
            return View(manufacturer);
        }

        //
        // POST: /Manufacturer/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Manufacturer manufacturer = db.Manufacturers.Find(id);

            var assigned = (from v in db.ValveSectionFeatures
                            where v.ManufacturerID == id
                            select new { found = v.ValveSectionID }).ToList();

            if (assigned.Count != 0)
            {
                ModelState.AddModelError("", "Manufacturer is assigned to a Valve Section Feature and cannot be deleted.");
             
                if (manufacturer == null)
                {
                    return HttpNotFound();
                }
                return View(manufacturer);
            }

            db.Manufacturers.Remove(manufacturer);
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