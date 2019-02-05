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
    public class MaterialTypeController : Controller
    {
        private PipelineFeatureListDBContext db = new PipelineFeatureListDBContext();

        //
        // GET: /MaterialType/

        public ActionResult Index()
        {
            return View(db.MaterialTypes.ToList());
        }

        //
        // GET: /MaterialType/Details/5

        public ActionResult Details(int id = 0)
        {
            MaterialType materialtype = db.MaterialTypes.Find(id);
            if (materialtype == null)
            {
                return HttpNotFound();
            }
            return View(materialtype);
        }

        //
        // GET: /MaterialType/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /MaterialType/Create

        [HttpPost]
        public ActionResult Create(MaterialType materialtype)
        {
            if (ModelState.IsValid)
            {
                db.MaterialTypes.Add(materialtype);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(materialtype);
        }

        //
        // GET: /MaterialType/Edit/5

        public ActionResult Edit(int id = 0)
        {
            MaterialType materialtype = db.MaterialTypes.Find(id);
            var materialfeatures = (from vf in db.ValveSectionFeatures
                                    where vf.MaterialTypeID == materialtype.MaterialTypeID
                                    select new
                                    {
                                        vf
                                    }).ToList();


            if (materialfeatures.Count > 0)
            {
                ModelState.AddModelError("MaterialTypeItem", "Warning! This Material Type is assigned to Circuit feature(s).");
                ViewBag.HasError = "True";
            }
            if (materialtype == null)
            {
                return HttpNotFound();
            }
            return View(materialtype);
        }

        //
        // POST: /MaterialType/Edit/5

        [HttpPost]
        public ActionResult Edit(MaterialType materialtype)
        {
            if (ModelState.IsValid)
            {
                db.Entry(materialtype).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(materialtype);
        }

        //
        // GET: /MaterialType/Delete/5

        public ActionResult Delete(int id = 0)
        {
            MaterialType materialtype = db.MaterialTypes.Find(id);
            var materialfeatures = (from vf in db.ValveSectionFeatures
                               where vf.MaterialTypeID == materialtype.MaterialTypeID
                                    select new
                               {
                                   vf
                               }).ToList();


            if (materialfeatures.Count > 0)
            {
                ModelState.AddModelError("MaterialTypeItem", "This Material Type is assigned to Circuit feature(s) and cannot be deleted.");
                ViewBag.HasError = "True";
            }

            if (materialtype == null)
            {
                return HttpNotFound();
            }
            return View(materialtype);
        }

        //
        // POST: /MaterialType/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            MaterialType materialtype = db.MaterialTypes.Find(id);
            db.MaterialTypes.Remove(materialtype);
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