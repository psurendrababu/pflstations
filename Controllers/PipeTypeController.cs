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
    public class PipeTypeController : Controller
    {
        private PipelineFeatureListDBContext db = new PipelineFeatureListDBContext();

        //
        // GET: /PipeType/

        public ActionResult Index()
        {    
            var featuretypes = (from ft in db.PipeTypes
                                 join f in db.Features on ft.FeatureID equals f.FeatureID
                                 select new
                                 {
                                     PipeType = ft,
                                     Feature = f
                                 }).ToList();

            List<PipeType> lpt = new List<PipeType>();


            foreach (var ft in featuretypes) {
                lpt.Add(ft.PipeType);
            }

            return View(lpt);

            //return View(db.PipeTypes.ToList());
        }

        //
        // GET: /PipeType/Details/5

        public ActionResult Details(int id = 0)
        {
            var featuretypes = (from ft in db.PipeTypes
                                join f in db.Features on ft.FeatureID equals f.FeatureID
                                where ft.PipeTypeID == id
                                select new
                                {
                                    PipeType = ft,
                                    Feature = f
                                }).FirstOrDefault();

            //PipeType pipetype = db.PipeTypes.Find(id);

            if (featuretypes == null)
            {
                return HttpNotFound();
            }
            ViewBag.SelectedFeatureID = new SelectList(db.Features, "FeatureID", "FeatureItem", featuretypes.Feature.FeatureID);
            PipeType pipetype = featuretypes.PipeType;
            return View(pipetype);
        }

        //
        // GET: /PipeType/Create

        public ActionResult Create()
        {
            ViewBag.FeatureID = new SelectList(db.Features, "FeatureID", "FeatureItem");
            return View();
        }

        //
        // POST: /PipeType/Create

        [HttpPost]
        public ActionResult Create(PipeType pipetype)
        {
            if (pipetype.FeatureID == 0)
            {                
                ViewBag.FeatureID = new SelectList(db.Features, "FeatureID", "FeatureItem");
                return View(pipetype);
                
            }
            if (pipetype.PipeTypeItem == null)
            {
                ModelState.AddModelError("PipeTypeItem", "Feature Type is required.");
                ViewBag.FeatureID = new SelectList(db.Features, "FeatureID", "FeatureItem");
                return View(pipetype);                
            }

            if (ModelState.IsValid)
            {
                db.PipeTypes.Add(pipetype);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
           

            return View(pipetype);
        }

        //
        // GET: /PipeType/Edit/5

        public ActionResult Edit(int id = 0)
        {
            //ViewBag.FeatureID = new SelectList(db.Features, "FeatureID", "FeatureItem");

            var featuretypes = (from ft in db.PipeTypes
                                join f in db.Features on ft.FeatureID equals f.FeatureID
                                where ft.PipeTypeID == id
                                select new
                                {
                                    PipeType = ft,
                                    Feature = f
                                }).FirstOrDefault();

            ViewBag.SelectedFeatureID = new SelectList(db.Features, "FeatureID", "FeatureItem", featuretypes.Feature.FeatureID);

            PipeType pipetype = db.PipeTypes.Find(id);
            var pipefeatures = (from vf in db.ValveSectionFeatures
                                where vf.TypeID == pipetype.PipeTypeID
                                select new
                                {
                                    vf
                                }).ToList();


            if (pipefeatures.Count > 0)
            {
                ModelState.AddModelError("PipeTypeItem", "Warning! This Feature Type is assigned to Circuit feature(s).");
                ViewBag.HasError = "True";
            }
            if (pipetype == null)
            {
                return HttpNotFound();
            }
            return View(pipetype);
        }

        //
        // POST: /PipeType/Edit/5

        [HttpPost]
        public ActionResult Edit(PipeType pipetype)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pipetype).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pipetype);
        }

        //
        // GET: /PipeType/Delete/5

        public ActionResult Delete(int id = 0)
        {
            var featuretypes = (from ft in db.PipeTypes
                                join f in db.Features on ft.FeatureID equals f.FeatureID
                                where ft.PipeTypeID == id
                                select new
                                {
                                    PipeType = ft,
                                    Feature = f
                                }).FirstOrDefault();

            ViewBag.SelectedFeatureID = new SelectList(db.Features, "FeatureID", "FeatureItem", featuretypes.Feature.FeatureID);

            PipeType pipetype = db.PipeTypes.Find(id);

            var pipefeatures = (from vf in db.ValveSectionFeatures
                               where vf.TypeID == pipetype.PipeTypeID
                                select new
                               {
                                   vf
                               }).ToList();


            if (pipefeatures.Count > 0)
            {
                ModelState.AddModelError("PipeTypeItem", "This Feature Type is assigned to Circuit feature(s) and cannot be deleted.");
                ViewBag.HasError = "True";
            }

            if (pipetype == null)
            {
                return HttpNotFound();
            }
            return View(pipetype);
        }

        //
        // POST: /PipeType/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            PipeType pipetype = db.PipeTypes.Find(id);
            db.PipeTypes.Remove(pipetype);
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