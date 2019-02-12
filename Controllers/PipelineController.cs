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

        //------------------------------------------//
        //---------------Main Section---------------//
        //------------------------------------------//
        //
        // GET: /Pipeline/

        public ActionResult Index()
        {
            var model = new List<Pipeline>();

            model = db.Pipelines.OrderBy(p => p.PipelineItem).ToList();

            foreach (var c in model)
            {
                c.CircuitCount = (from vs in db.ValveSection
                                  join p in db.Pipelines on vs.PipelineID equals p.PipelineID
                                  where p.PipelineID == c.PipelineID
                                  select vs).Count();
                //update status of station based on circiuts status
                // check if all circuits in this station are eng approved    

                c.StationStatus = null;

                var engapproved = (from vs in db.ValveSection
                                   join p in db.Pipelines on vs.PipelineID equals p.PipelineID
                                   where p.PipelineID == c.PipelineID && vs.ValveSectionStatusID >= 8
                                   select new
                                   {
                                       vs.ValveSectionStatusID
                                   }).ToList();
                if (c.CircuitCount == engapproved.Count)
                {
                    if (engapproved.Where(a => a.ValveSectionStatusID == 8).Count() > 0)
                    {
                        c.StationStatus = "Ready for Deliver";
                    }                    
                }
                else
                  c.StationStatus = null;
                // check if all circuits in this station are delivered
                var delivered = (from vs in db.ValveSection
                                   join p in db.Pipelines on vs.PipelineID equals p.PipelineID
                                   where p.PipelineID == c.PipelineID && vs.ValveSectionStatusID >= 9
                                   select new
                                   {
                                       vs.ValveSectionStatusID
                                   }).ToList();
                if (c.CircuitCount == delivered.Count)
                {
                    if (delivered.Where(a => a.ValveSectionStatusID == 9).Count() > 0)
                    {
                        c.StationStatus = "Delivered";
                    }                    
                }
                // check if all circuits in this station are client approved
                var clapprove = (from vs in db.ValveSection
                                 join p in db.Pipelines on vs.PipelineID equals p.PipelineID
                                 where p.PipelineID == c.PipelineID && vs.ValveSectionStatusID >= 10
                                 select new
                                 {
                                     vs.ValveSectionStatusID
                                 }).ToList();

                if (c.CircuitCount == clapprove.Count)
                {
                    if (clapprove.Where(a => a.ValveSectionStatusID == 10).Count() > 0)
                    {
                        c.StationStatus = "Client Approved";
                    }                    
                }

                // check if all circuits in this station are client approved by default
                var clapprovedef = (from vs in db.ValveSection
                                 join p in db.Pipelines on vs.PipelineID equals p.PipelineID
                                 where p.PipelineID == c.PipelineID && vs.ValveSectionStatusID >= 11
                                 select new
                                 {
                                     vs.ValveSectionStatusID
                                 }).ToList();

                if (c.CircuitCount == clapprovedef.Count)
                {
                    if (clapprovedef.Where(a => a.ValveSectionStatusID == 11).Count() > 0)
                    {
                        c.StationStatus = "Client Approved by default";
                    }                    
                }

                c.PipeSystem = (from p in db.Pipelines
                                join ps in db.PipeSystems on p.PipeSystemID equals ps.PipeSystemID
                                where p.PipelineID == c.PipelineID
                                select ps).FirstOrDefault();

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

            var PTRList = (from p in db.Pipelines
                           join ptr in db.PressureTestRecords on p.PipelineID equals ptr.PipelineID
                           where ptr.PipelineID == pipeline.PipelineID
                           select ptr).ToList();
            ViewData.Add("PTRList", PTRList);

            // check if all circuits in this station Engineering review is approved if so then 'Delivered' button gets enabled.
            var engapproved = (from vs in db.ValveSection
                               join p in db.Pipelines on vs.PipelineID equals p.PipelineID
                               where p.PipelineID == pipeline.PipelineID && vs.ValveSectionStatusID >= 8
                               select new
                               {
                                   vs.ValveSectionStatusID
                               }).ToList();
            if (pipeline.CircuitCount == engapproved.Count)
            {
                if (engapproved.Where(a => a.ValveSectionStatusID == 8).Count() > 0)
                {
                    ViewData.Add("Reviewed", "true");
                }
            }


            // check if all circuits in this station are delivered, if so then 'Client Approved' button gets enabled.
            var delivered = (from vs in db.ValveSection
                               join p in db.Pipelines on vs.PipelineID equals p.PipelineID
                               where p.PipelineID == pipeline.PipelineID && vs.ValveSectionStatusID >= 9
                               select new
                               {
                                   vs.ValveSectionStatusID
                               }).ToList();
            if (pipeline.CircuitCount == delivered.Count)
            {
                if (delivered.Where(a => a.ValveSectionStatusID == 9).Count() > 0)
                {
                    ViewData.Add("Delivered", "true");
                }
            }            

            // If all circuits in this station are delivered more than 7 days ago, then enable 'Default Client Acceptance' button gets enabled.
            var accept = (from vs in db.ValveSection
                          join p in db.Pipelines on vs.PipelineID equals p.PipelineID
                          join wh in db.WorkflowHistories on vs.ValveSectionID equals wh.ValveSectionID
                          where p.PipelineID == pipeline.PipelineID && vs.ValveSectionStatusID >= 9 && wh.New_WorkflowStatusID >= 9
                          select new
                          {
                              changedOn = wh.ChangedOn

                          }).ToList();
           
            if (accept.Count != 0 && (DateTime.Now - accept.Min(a => a.changedOn)).TotalDays > 7 && accept.Count == pipeline.CircuitCount )
            {

                ViewData.Add("accept", "true");
                //ViewData.Remove("Delivered");
            }
            
            return View(pipeline);
        }

        // GET: /Pipeline/StatusChange/5

        public ActionResult StatusChange(int id = 0, string description = "",int newStatus= 0, int oldStatus = 0)
        {
            Pipeline pipeline = db.Pipelines.Find(id);
            if (pipeline == null)
            {
                return HttpNotFound();
            }
            ViewBag.Message = description;
            Session["StatusChangeNewStatusID"] = newStatus;
            Session["StatusChangeOldStatusID"] = oldStatus;
            return View(pipeline);
        }

        // POST: /ValveSection/StatusChange/5
        [HttpPost]
        public ActionResult StatusChange(Pipeline pipeline)
        {
            if (ModelState.IsValid)
            {
                pipeline.ModifiedBy_UserID = Convert.ToInt64(Session["UserID"].ToString());
                pipeline.ModifiedOn = DateTime.Now;
                               
                int? newStatus = Convert.ToInt32(Session["StatusChangeNewStatusID"].ToString());
                int? oldStatus = Convert.ToInt32(Session["StatusChangeOldStatusID"].ToString());
                //ValveSection valveSection = new ValveSection();
                //db.Entry(valveSection).State = EntityState.Modified;
                //db.SaveChanges();

                using (PipelineFeatureListDBContext db1 = new PipelineFeatureListDBContext())
                {

                    db1.ValveSection
                          .Where(x => x.PipelineID == pipeline.PipelineID)
                          .ToList()
                          .ForEach(a =>
                          {
                              a.ValveSectionStatusID = newStatus;
                              InsertWorkHistory(a.ValveSectionID, oldStatus.Value, 1, newStatus.Value);
                          }
                          );

                    db1.SaveChanges();
                }
                return RedirectToAction("Index");
            }

            return View(pipeline);
        }

        private void InsertWorkHistory(long ValveSectionID, int origStatusID, int actionID, int newsStatusID)
        {
            PipelineFeatureListDBContext db1 = new PipelineFeatureListDBContext();

            WorkflowHistory wf = new WorkflowHistory();
            wf.ValveSectionID = Convert.ToInt64(ValveSectionID);
            wf.ChangedBy_UserID = Convert.ToInt64(Session["UserID"].ToString());
            wf.ChangedOn = DateTime.Now;
            wf.Old_WorkflowStatusID = origStatusID;
            wf.WorkflowActionID = actionID;
            wf.New_WorkflowStatusID = newsStatusID;

            db1.WorkflowHistories.Add(wf);
            db1.SaveChanges();
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
                ModelState.AddModelError("", "Station assigned to a Valve Section and cannot be deleted.");

                if (pipeline == null)
                {
                    return HttpNotFound();
                }
                return View(pipeline);
            }

            var documents = (from d in db.DocumentRecords
                             where d.PipelineID == id
                             select new { documents = d.DocumentRecordID }).ToList();

            if (documents.Count != 0)
            {
                ModelState.AddModelError("", "Station as documents assigned and cannot be deleted.");
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


        //------------------------------------------------------//
        //---------------Material Records Section---------------//
        //------------------------------------------------------//

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
                return RedirectToAction("Details", "Pipeline", new { id = documentrecord.PipelineID });
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
        // POST: /Pipeline/EditDoc/

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
                return RedirectToAction("Details", "Pipeline", new { id = documentrecord.PipelineID });
            }
            return View(documentrecord);
        }

        //
        // GET: /Pipeline/DeleteDoc

        public ActionResult DeleteDoc(int id = 0)
        {
            DocumentRecord documentrecord = db.DocumentRecords.Find(id);
            if (documentrecord == null)
            {
                return HttpNotFound();
            }

            ViewBag.StationName = (from p in db.Pipelines
                                   join doc in db.DocumentRecords on p.PipelineID equals doc.PipelineID
                                   where doc.DocumentRecordID == documentrecord.DocumentRecordID
                                   select p.PipelineItem).FirstOrDefault();

            return View(documentrecord);
        }

        //
        // POST: /Pipeline/Delete/

        [HttpPost, ActionName("DeleteDoc")]
        public ActionResult DeleteDocConfirmed(int id)
        {
            DocumentRecord documentrecord = db.DocumentRecords.Find(id);

            //Need to add in some validation code to look for feature attributes that have this document assigned to them.
            //Cannot delete the record if there are documents assigned to any features

            db.DocumentRecords.Remove(documentrecord);
            db.SaveChanges();
            return RedirectToAction("Details", "Pipeline", new { id = documentrecord.PipelineID });
        }

        //---------------------------------------------------//
        //---------------Pressure Test Section---------------//
        //---------------------------------------------------//

        //
        // GET: /Pipeline/CreateDoc

        public ActionResult CreatePTR(Pipeline pipeline)
        {
            int entry = 0;
            try { entry = (int)db.PressureTestRecords.Where(p => p.PipelineID == pipeline.PipelineID).OrderByDescending(p => p.PressureTestRecordID).Max(d => d.PressureTestRecordID); }
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


            PressureTestRecord pressuretestrecord = new PressureTestRecord() { PipelineID = pipeline.PipelineID };
            
            ViewBag.PipelineItem = pipeline.PipelineItem;
            //ViewBag.PTMedium = new SelectList(db.PTTestMediums, "PTTestMediumID", "PTMedium");
            ViewBag.PTMedium = new SelectList(db.PTTestMediums, "PTMedium", "PTMedium");
            return View(pressuretestrecord);
        }

        //
        // POST: /Pipeline/CreateDoc

        [HttpPost]
        public ActionResult CreatePTR(PressureTestRecord pressuretestrecord)
        {


            if (ModelState.IsValid)
            {
                //documentrecord.PipelineID = Convert.ToInt64(Session["CurrentPipeline"].ToString());

                if(pressuretestrecord.PTMedium is null)
                {
                    pressuretestrecord.PTMedium = "Unknown";
                }
                pressuretestrecord.PressureTestRecordID = Convert.ToInt32(Session["CurrentRecordIdentifier"].ToString());

                db.PressureTestRecords.Add(pressuretestrecord);
                db.SaveChanges();
                return RedirectToAction("Details", "Pipeline", new { id = pressuretestrecord.PipelineID });
            }

            return View();
        }

        //
        // GET: /Pipeline/EditDoc/

        public ActionResult EditPTR(int id = 0)
        {
            PressureTestRecord pressuretestrecord = db.PressureTestRecords.Find(id);
            //ViewBag.SelectedPTMediumID = new SelectList(db.PTTestMediums, "PTTestMediumID", "PTMedium", pressuretestrecord.PTMedium);
            ViewBag.SelectedPTMediumID = new SelectList(db.PTTestMediums, "PTMedium", "PTMedium", pressuretestrecord.PTMedium);

            if (pressuretestrecord == null)
            {
                return HttpNotFound();
            }

            ViewBag.PipelineItem = db.Pipelines.Where(p => p.PipelineID == pressuretestrecord.PipelineID).Select(p => p.PipelineItem).FirstOrDefault();

            ViewBag.PressureTestRecordID = id;

            return View(pressuretestrecord);
        }

        //
        // POST: /Pipeline/EditDoc/

        [HttpPost]
        public ActionResult EditPTR(PressureTestRecord pressuretestrecord)
        {
            
            
            if (ModelState.IsValid)
            {
                db.Entry(pressuretestrecord).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "Pipeline", new { id = pressuretestrecord.PipelineID });
            }
            return View(pressuretestrecord);
        }

        //
        // GET: /Pipeline/DeleteDoc

        public ActionResult DeletePTR(int id = 0)
        {
            PressureTestRecord pressuretestrecord = db.PressureTestRecords.Find(id);
            if (pressuretestrecord == null)
            {
                return HttpNotFound();
            }

            ViewBag.StationName = (from p in db.Pipelines
                                   join doc in db.PressureTestRecords on p.PipelineID equals doc.PipelineID
                                   where doc.PressureTestRecordID == pressuretestrecord.PressureTestRecordID
                                   select p.PipelineItem).FirstOrDefault();

            return View(pressuretestrecord);
        }

        //
        // POST: /Pipeline/Delete/

        [HttpPost, ActionName("DeletePTR")]
        public ActionResult DeletePTRConfirmed(int id)
        {
            PressureTestRecord pressuretestrecord = db.PressureTestRecords.Find(id);

            //Need to add in some validation code to look for feature attributes that have this document assigned to them.
            //Cannot delete the record if there are documents assigned to any features

            db.PressureTestRecords.Remove(pressuretestrecord);
            db.SaveChanges();
            return RedirectToAction("Details", "Pipeline", new { id = pressuretestrecord.PipelineID });
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        
    }
}