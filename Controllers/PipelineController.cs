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
        // GET: /Pipeline/Details/

        public ActionResult Details(int id = 0)
        {
            Pipeline pipeline = db.Pipelines.Include("PipeSystem").Where(p => p.PipelineID == id).First();

            Session["CurrentPipeline"] = pipeline.PipelineID;

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

            var DocumentList = (from p in db.Pipelines
                                join doc in db.DocumentRecords on p.PipelineID equals doc.PipelineID
                                where doc.PipelineID == pipeline.PipelineID
                                select doc).ToList();
            ViewData.Add("DocumentList", DocumentList);

            var DocumentTypeList = (from dt in db.DocumentTypes select dt).ToList();
            ViewData.Add("DocumentTypeList", DocumentTypeList);

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

        //
        // GET: /Pipeline/CreateDoc

        public ActionResult CreateDoc(Pipeline pipeline)
        {
            //int currentPipeline = Convert.ToInt32(Session["CurrentPipeline"].ToString());

            int entry = 0;
            try { entry = (int)db.DocumentRecords.Where(p => p.PipelineID == pipeline.PipelineID).OrderByDescending(p => p.DocumentRecordID).Max(d => d.DocumentRecordID); }
            catch { }

            if (entry == 0)
            {
                Session["CurrentRecordIdentifier"] = "1";
            }
            else
            {
                entry++;
                try
                {
                    Session["CurrentRecordIdentifier"] = entry;
                }
                catch
                {
                    Session["CurrentRecordIdentifier"] = "1";
                }
            }

            ViewBag.DocumentTypeID = new SelectList(db.DocumentTypes, "DocumentTypeID", "DocumentTypeItem");
            DocumentRecord documentrecord = new DocumentRecord() { PipelineID = pipeline.PipelineID };
            ViewBag.PipelineItem = pipeline.PipelineItem;
            return View(documentrecord);
        }

        //
        // POST: /Pipeline/CreateDoc

        [HttpPost]
        public ActionResult CreateDoc(DocumentRecord documentrecord)
        {
            if (ModelState.IsValid)
            {
                //documentrecord.PipelineID = Convert.ToInt64(Session["CurrentPipeline"].ToString());
                documentrecord.DocumentRecordID = Convert.ToInt32(Session["CurrentRecordIdentifier"].ToString());

                documentrecord.DocumentTypeItem = db.DocumentTypes.Where(doctype => doctype.DocumentTypeID == documentrecord.DocumentTypeID).Select(doctype => doctype.DocumentTypeItem).FirstOrDefault();

                db.DocumentRecords.Add(documentrecord);
                db.SaveChanges();
                return RedirectToAction("Pipeline", "Details", new { documentrecord.PipelineID });
            }

            return View();
        }

        //
        // GET: /Pipeline/EditDoc/

        public ActionResult EditDoc(int id = 0)
        {
            DocumentRecord documentrecord = db.DocumentRecords.Find(id);
            if (documentrecord == null)
            {
                return HttpNotFound();
            }

            ViewBag.PipelineItem = db.Pipelines.Where(p => p.PipelineID == documentrecord.PipelineID).Select(p => p.PipelineItem).FirstOrDefault();

            ViewBag.DocumentTypeID = new SelectList(db.DocumentTypes, "DocumentTypeID", "DocumentTypeItem", documentrecord.DocumentTypeID);

            ViewBag.DocumentRecordID = id;

            return View(documentrecord);
        }

        //
        // POST: /Pipeline/EditDoc/5

        [HttpPost]
        public ActionResult EditDoc(DocumentRecord documentrecord)
        {
            if (ModelState.IsValid)
            {
                //documentrecord.ModifiedBy_UserID = Convert.ToInt64(Session["UserID"].ToString());
                //documentrecord.ModifiedOn = DateTime.Now;

                documentrecord.DocumentTypeItem = db.DocumentTypes.Where(doctype => doctype.DocumentTypeID == documentrecord.DocumentTypeID).Select(doctype => doctype.DocumentTypeItem).FirstOrDefault();

                db.Entry(documentrecord).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details","Pipeline",new { id = documentrecord.PipelineID });
            }
            return View(documentrecord);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}