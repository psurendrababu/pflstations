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
    public class PipelineController : Controller
    {
        private PipelineFeatureListDBContext db = new PipelineFeatureListDBContext();

        //
        // GET: /Pipeline/

        public ActionResult Index()
        {
            var model = db.Pipelines.OrderBy(p => p.PipelineItem).ToList();
            
            foreach (var c in model)
            {
                c.CircuitCount = (from vs in db.ValveSection
                                  join p in db.Pipelines on vs.PipelineID equals p.PipelineID
                                  where p.PipelineID == c.PipelineID
                                  select vs).Count();

                db.Entry(c).Property("CircuitCount").IsModified = true;
                db.SaveChanges();
            }

            return View(model);
        }

        //
        // GET: /Pipeline/Details/5

        public ActionResult Details(int id = 0)
        {
            Pipeline pipeline = db.Pipelines.Include("PipeSystem").Where(p => p.PipelineID == id).First();
            if (pipeline == null)
            {
                return HttpNotFound();
            }

                pipeline.CircuitCount = (from vs in db.ValveSection
                                  join p in db.Pipelines on vs.PipelineID equals p.PipelineID
                                  where p.PipelineID == pipeline.PipelineID
                                  select vs).Count();

                db.Entry(pipeline).Property("CircuitCount").IsModified = true;
                db.SaveChanges();

            return View(pipeline);
        }

        //
        // GET: /Pipeline/Create

        public ActionResult Create()
        {
            ViewBag.PipeSystemID = new SelectList(db.PipeSystems, "PipeSystemID", "PipeSystemItem");
            return View();
        }

        //
        // POST: /Pipeline/Create

        [HttpPost]
        public ActionResult Create(Pipeline pipeline)
        {
            if (ModelState.IsValid)
            {
                pipeline.CreatedBy_UserID = Convert.ToInt64(Session["UserID"].ToString());
                pipeline.ModifiedBy_UserID = Convert.ToInt64(Session["UserID"].ToString());
                pipeline.CreatedOn = DateTime.Now;
                pipeline.ModifiedOn = DateTime.Now;

                db.Pipelines.Add(pipeline);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pipeline);
        }

        //
        // GET: /Pipeline/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Pipeline pipeline = db.Pipelines.Find(id);
            if (pipeline == null)
            {
                return HttpNotFound();
            }

            ViewBag.PipeSystemID = new SelectList(db.PipeSystems, "PipeSystemID", "PipeSystemItem", pipeline.PipeSystemID);

            return View(pipeline);
        }

        //
        // POST: /Pipeline/Edit/5

        [HttpPost]
        public ActionResult Edit(Pipeline pipeline)
        {
            if (ModelState.IsValid)
            {
                pipeline.ModifiedBy_UserID = Convert.ToInt64(Session["UserID"].ToString());
                pipeline.ModifiedOn = DateTime.Now;

                db.Entry(pipeline).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pipeline);
        }

        //
        // GET: /Pipeline/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Pipeline pipeline = db.Pipelines.Include("PipeSystem").Where(p => p.PipelineID == id).First();
            if (pipeline == null)
            {
                return HttpNotFound();
            }
            return View(pipeline);
        }

        //
        // POST: /Pipeline/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Pipeline pipeline = db.Pipelines.Include("PipeSystem").Where(p => p.PipelineID == id).First();

            var assigned = (from v in db.ValveSection
                        where v.PipelineID == id
                        select new { found = v.ValveSectionID }).ToList();

            if (assigned.Count != 0)
            {
                ModelState.AddModelError("", "Pipeline is assigned to a Valve Section and cannot be deleted.");
                
                if (pipeline == null)
                {
                    return HttpNotFound();
                }
                return View(pipeline);
            }

            db.Pipelines.Remove(pipeline);
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