using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PipelineFeatureList.Models;
using System.Text;
using System.Data.SqlClient;
using System.IO;
using System.Threading;

namespace PipelineFeatureList.Controllers
{
    public class ValveSectionController : Controller
    {
        private PipelineFeatureListDBContext db = new PipelineFeatureListDBContext();

        //
        // GET: /ValveSection/
        // for Admins

        public ActionResult Index()
        {
            if (Session["GroupClassificationAdmin"].ToString() != "Administrator")
                return (null);    
            else
            {
                var model = (from v in db.ValveSection
                             join b in db.Users on v.BuilderID equals b.UserID into b1
                             from bd in b1.DefaultIfEmpty()
                             join q in db.Users on v.QCID equals q.UserID into q1
                             from qd in q1.DefaultIfEmpty()
                             join e in db.Users on v.EngineerID equals e.UserID into e1
                             from ed in e1.DefaultIfEmpty()
                             join fe in db.Users on v.FinalEngineerID equals fe.UserID into fe1
                             from fed in fe1.DefaultIfEmpty()
                             join ps in db.PipeSystems on v.PipeSystemID equals ps.PipeSystemID into ps1
                             from psd in ps1.DefaultIfEmpty()
                             join p in db.Pipelines on v.PipelineID equals p.PipelineID into p1
                             from pd in p1.DefaultIfEmpty()
                             join vs in db.ValveSectionStatus on v.ValveSectionStatusID equals vs.ValveSectionStatusID into vs1
                             from vsd in vs1.DefaultIfEmpty()
                             //join dg in db.DisplayGroups on vsd.DisplayGroupID equals dg.DisplayGroupID into dg1
                             //from dgd in dg1.DefaultIfEmpty()
                             orderby v.PipelineID
                             select new ValveSectionAdmin
                             {
                                 ValveSection = v,
                                 PipeSystem = psd,
                                 Pipeline = pd,
                                 Builder = bd,
                                 QC = qd,
                                 Engineer = ed,
                                 FinalEngineer = fed,
                                 ValveStatus = vsd
                                 //, DisplayGroup = dgd
                             }).ToList();
                return View(model);
            }
            
        }

        //
        // GET: /ValveSection/BuildAssigned

        public ActionResult BuildAssigned()
        {
            Int64 currbuilder = -1;
            try { currbuilder = Convert.ToInt64(Session["UserID"].ToString()); } catch { currbuilder = -1; }

            var model = (from v in db.ValveSection
                         join vss in db.ValveSectionStatus on v.ValveSectionStatusID equals vss.ValveSectionStatusID into vss1
                         from vssd in vss1.DefaultIfEmpty()
                         join dg in db.DisplayGroups on vssd.DisplayGroupID equals dg.DisplayGroupID into dg1
                         from dgd in dg1.DefaultIfEmpty()
                         join b in db.Users on v.BuilderID equals b.UserID into b1
                         from bd in b1.DefaultIfEmpty()
                         join q in db.Users on v.QCID equals q.UserID into q1
                         from qd in q1.DefaultIfEmpty()
                         join e in db.Users on v.EngineerID equals e.UserID into e1
                         from ed in e1.DefaultIfEmpty()
                         join fe in db.Users on v.FinalEngineerID equals fe.UserID into fe1
                         from fed in fe1.DefaultIfEmpty()
                         join ps in db.PipeSystems on v.PipeSystemID equals ps.PipeSystemID into ps1
                         from psd in ps1.DefaultIfEmpty()
                         join p in db.Pipelines on v.PipelineID equals p.PipelineID into p1
                         from pd in p1.DefaultIfEmpty()
                         join vs in db.ValveSectionStatus on v.ValveSectionStatusID equals vs.ValveSectionStatusID into vs1
                         from vsd in vs1.DefaultIfEmpty()
                         where v.BuilderID == currbuilder
                         && dgd.DisplayGroupName == "Build"
                         orderby v.PipelineID
                         select new ValveSectionAdmin
                         {
                             ValveSection = v,
                             PipeSystem = psd,
                             Pipeline = pd,
                             ValveStatus = vsd, 
                             Builder = bd,
                             QC = qd,
                             Engineer = ed,
                             FinalEngineer = fed
                         }).ToList();

            return View(model);
        }

        //
        // GET: /ValveSection/QC

        public ActionResult QC()
        {
            List<ValveSectionQC> model = new List<ValveSectionQC>();
            foreach (var item in QCAssigned())
            {
                ValveSectionQC qc = new ValveSectionQC();
                qc.ValveSectionQCAssigned = item;
                model.Add(qc);
            }
            foreach (var item in QCPool())
            {
                ValveSectionQC qc = new ValveSectionQC();
                qc.ValveSectionQCPool = item;
                model.Add(qc);
            }

            return View(model);
        }

        //
        // GET: /ValveSection/QCAssigned

        public List<ValveSectionQCAssigned> QCAssigned()
        {
            Int64 currQC = -1;
            try { currQC = Convert.ToInt64(Session["UserID"].ToString()); }
            catch { currQC = -1; }

            //List<ValveSectionQCAssigned> model =
            var model = (from v in db.ValveSection
                         join vss in db.ValveSectionStatus on v.ValveSectionStatusID equals vss.ValveSectionStatusID into vss1
                         from vssd in vss1.DefaultIfEmpty()
                         join dg in db.DisplayGroups on vssd.DisplayGroupID equals dg.DisplayGroupID into dg1
                         from dgd in dg1.DefaultIfEmpty()
                         join ps in db.PipeSystems on v.PipeSystemID equals ps.PipeSystemID into ps1
                         from psd in ps1.DefaultIfEmpty()
                         join p in db.Pipelines on v.PipelineID equals p.PipelineID into p1
                         from pd in p1.DefaultIfEmpty()
                         join b in db.Users on v.BuilderID equals b.UserID into b1
                         from bd in b1.DefaultIfEmpty()
                         where v.QCID == currQC
                         && dgd.DisplayGroupName == "Quality Control"
                         orderby v.PipelineID
                         select new ValveSectionQCAssigned
                         {
                             ValveSection = v,
                             PipeSystem = psd,
                             Pipeline = pd,
                             ValveStatus = vssd,
                             Builder = bd
                         }).ToList();

            foreach (var item in model)
            {
                item.NumberOfFeatures = (from v in db.ValveSectionFeatures where v.ValveSectionID == item.ValveSection.ValveSectionID select new { }).Count();
                item.Length = Convert.ToDecimal(item.ValveSection.PFLLength);
                var firstready = (from w in db.WorkflowHistories
                                  join v in db.ValveSectionStatus on w.New_WorkflowStatusID equals v.ValveSectionStatusID
                                  where w.ValveSectionID == item.ValveSection.ValveSectionID
                                  && v.ValveSectionStatusItem == "Build Complete / Ready for QC"
                                  orderby w.ChangedOn descending
                                  select new { w.ChangedOn }).FirstOrDefault();
                item.ReadyForQC = (firstready == null ? DateTime.Now : firstready.ChangedOn);
            }
            return model;
        }
        public List<ValveSectionQCPool> QCPool()
        {
            Int64 currQC = -1;
            try { currQC = Convert.ToInt64(Session["UserID"].ToString()); }
            catch { currQC = -1; }

            List<ValveSectionQCPool> model = (from v in db.ValveSection
                         join vss in db.ValveSectionStatus on v.ValveSectionStatusID equals vss.ValveSectionStatusID into vss1
                         from vssd in vss1.DefaultIfEmpty()
                         join dg in db.DisplayGroups on vssd.DisplayGroupID equals dg.DisplayGroupID into dg1
                         from dgd in dg1.DefaultIfEmpty()
                         join ps in db.PipeSystems on v.PipeSystemID equals ps.PipeSystemID into ps1
                         from psd in ps1.DefaultIfEmpty()
                         join p in db.Pipelines on v.PipelineID equals p.PipelineID into p1
                         from pd in p1.DefaultIfEmpty()
                         join b in db.Users on v.BuilderID equals b.UserID into b1
                         from bd in b1.DefaultIfEmpty()
                         where (v.QCID == 0 || v.QCID == null)
                         && (v.BuilderID != currQC || v.BuilderID == null)
                         && (v.EngineerID != currQC || v.EngineerID == null)
                         && (v.FinalEngineerID != currQC || v.FinalEngineerID == null)
                         && dgd.DisplayGroupName == "Quality Control"
                         orderby v.PipelineID
                         select new ValveSectionQCPool
                         {
                             ValveSection = v,
                             PipeSystem = psd,
                             Pipeline = pd,
                             ValveStatus = vssd,
                             Builder = bd
                         }).ToList();

            foreach (var item in model)
            {
                item.NumberOfFeatures = (from v in db.ValveSectionFeatures where v.ValveSectionID == item.ValveSection.ValveSectionID select new { }).Count();
                item.Length = Convert.ToDecimal(item.ValveSection.PFLLength); 
                var firstready = (from w in db.WorkflowHistories
                                        join v in db.ValveSectionStatus on w.New_WorkflowStatusID equals v.ValveSectionStatusID
                                        where w.ValveSectionID == item.ValveSection.ValveSectionID
                                        && v.ValveSectionStatusItem == "Build Complete / Ready for QC"
                                        orderby w.ChangedOn descending
                                        select new { w.ChangedOn }).FirstOrDefault();
                item.ReadyForQC = (firstready == null ? DateTime.Now : firstready.ChangedOn);
            }
            return model;
        }

        //
        // GET: /ValveSection/Engineering

        public ActionResult Engineering()
        {
            List<ValveSectionEngineering> model = new List<ValveSectionEngineering>();
            foreach (var item in EngineeringAssigned())
            {
                ValveSectionEngineering eng = new ValveSectionEngineering();
                eng.ValveSectionEngineeringAssigned = item;
                model.Add(eng);
            }
            foreach (var item in EngineeringPool())
            {
                ValveSectionEngineering eng = new ValveSectionEngineering();
                eng.ValveSectionEngineeringPool = item;
                model.Add(eng);
            }

            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult EngineeringDropDowns(int id1, int id2)
        {
            Session["VSEP_PipeSystemID"] = id1;
            Session["VSEP_PipelineID"] = id2;

            return Json("", JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /ValveSection/EngineeringAssigned

        public List<ValveSectionEngineeringAssigned> EngineeringAssigned()
        {
            Int64 currEngineer = -1;
            try { currEngineer = Convert.ToInt64(Session["UserID"].ToString()); }
            catch { currEngineer = -1; }

            List<ValveSectionEngineeringAssigned> model = (from v in db.ValveSection
                                                join vss in db.ValveSectionStatus on v.ValveSectionStatusID equals vss.ValveSectionStatusID into vss1
                                                from vssd in vss1.DefaultIfEmpty()
                                                join dg in db.DisplayGroups on vssd.DisplayGroupID equals dg.DisplayGroupID into dg1
                                                from dgd in dg1.DefaultIfEmpty()
                                                join ps in db.PipeSystems on v.PipeSystemID equals ps.PipeSystemID into ps1
                                                from psd in ps1.DefaultIfEmpty()
                                                join p in db.Pipelines on v.PipelineID equals p.PipelineID into p1
                                                from pd in p1.DefaultIfEmpty()
                                                where v.EngineerID == currEngineer
                                                && dgd.DisplayGroupName == "Engineering"
                                                orderby psd.PipeSystemItem, pd.PipelineItem
                                                select new ValveSectionEngineeringAssigned
                                                {
                                                    ValveSection = v,
                                                    PipeSystem = psd,
                                                    Pipeline = pd,
                                                    ValveStatus = vssd
                                                }).ToList();

            return model;
        }
        public List<ValveSectionEngineeringPool> EngineeringPool()
        {
            Int64 currEngineer = -1;
            try { currEngineer = Convert.ToInt64(Session["UserID"].ToString()); }
            catch { currEngineer = -1; }

            Int64 filterPipeSystem = 0;
            try { filterPipeSystem = Convert.ToInt64(Session["VSEP_PipeSystemID"].ToString()); }
            catch { filterPipeSystem = 0; }
            Int64 filterPipeline = 0;
            try { filterPipeline = Convert.ToInt64(Session["VSEP_PipelineID"].ToString()); }
            catch { filterPipeline = 0; }

            List<ValveSectionEngineeringPool> model = (from v in db.ValveSection
                                              join vss in db.ValveSectionStatus on v.ValveSectionStatusID equals vss.ValveSectionStatusID into vss1
                                              from vssd in vss1.DefaultIfEmpty()
                                              join dg in db.DisplayGroups on vssd.DisplayGroupID equals dg.DisplayGroupID into dg1
                                              from dgd in dg1.DefaultIfEmpty()
                                              join ps in db.PipeSystems on v.PipeSystemID equals ps.PipeSystemID into ps1
                                              from psd in ps1.DefaultIfEmpty()
                                              join p in db.Pipelines on v.PipelineID equals p.PipelineID into p1
                                              from pd in p1.DefaultIfEmpty()
                                              where (v.EngineerID == 0 || v.EngineerID == null)
                                              && v.BuilderID != currEngineer
                                              && v.QCID != currEngineer
                                              && dgd.DisplayGroupName == "Engineering"
                                              && (filterPipeSystem == 0 ?
                                                        true :
                                                        v.PipeSystemID == filterPipeSystem)
                                                       && (filterPipeline == 0 ?
                                                        true :
                                                        v.PipelineID == filterPipeline)
                                              orderby psd.PipeSystemItem, pd.PipelineItem
                                              select new ValveSectionEngineeringPool
                                              {
                                                  ValveSection = v,
                                                  PipeSystem = psd,
                                                  Pipeline = pd,
                                                  ValveStatus = vssd
                                              }).ToList();

            ViewBag.PipeSystemID = new SelectList(db.PipeSystems, "PipeSystemID", "PipeSystemItem", filterPipeSystem);
            ViewBag.PipelineID = new SelectList(db.Pipelines, "PipelineID", "PipelineItem", filterPipeline);

            return model;
        }

        
        //
        // GET: /ValveSection/FinalEngineering

        public ActionResult FinalEngineering()
        {
            List<ValveSectionFinalEngineering> model = new List<ValveSectionFinalEngineering>();
            foreach (var item in FinalEngineeringAssigned())
            {
                ValveSectionFinalEngineering eng = new ValveSectionFinalEngineering();
                eng.ValveSectionFinalEngineeringAssigned = item;
                model.Add(eng);
            }
            foreach (var item in FinalEngineeringPool())
            {
                ValveSectionFinalEngineering eng = new ValveSectionFinalEngineering();
                eng.ValveSectionFinalEngineeringPool = item;
                model.Add(eng);
            }

            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult FinalEngineeringDropDowns(int id1, int id2)
        {
            Session["VSFEP_PipeSystemID"] = id1;
            Session["VSFEP_PipelineID"] = id2;
            
            return Json("", JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /ValveSection/FinalEngineeringAssigned

        public List<ValveSectionFinalEngineeringAssigned> FinalEngineeringAssigned()
        {
            Int64 currEngineer = -1;
            try { currEngineer = Convert.ToInt64(Session["UserID"].ToString()); }
            catch { currEngineer = -1; }

            List<ValveSectionFinalEngineeringAssigned> model = (from v in db.ValveSection
                                                    join vss in db.ValveSectionStatus on v.ValveSectionStatusID equals vss.ValveSectionStatusID into vss1
                                                    from vssd in vss1.DefaultIfEmpty()
                                                    join dg in db.DisplayGroups on vssd.DisplayGroupID equals dg.DisplayGroupID into dg1
                                                    from dgd in dg1.DefaultIfEmpty()
                                                    join ps in db.PipeSystems on v.PipeSystemID equals ps.PipeSystemID into ps1
                                                    from psd in ps1.DefaultIfEmpty()
                                                    join p in db.Pipelines on v.PipelineID equals p.PipelineID into p1
                                                    from pd in p1.DefaultIfEmpty()
                                                    where v.FinalEngineerID == currEngineer
                                                    && dgd.DisplayGroupName == "Final Engineering"
                                                    orderby psd.PipeSystemItem, pd.PipelineItem
                                                    select new ValveSectionFinalEngineeringAssigned
                                                    {
                                                        ValveSection = v,
                                                        PipeSystem = psd,
                                                        Pipeline = pd,
                                                        ValveStatus = vssd
                                                    }).ToList();

            return model;
        }
        public List<ValveSectionFinalEngineeringPool> FinalEngineeringPool()
        {
            Int64 currEngineer = -1;
            try { currEngineer = Convert.ToInt64(Session["UserID"].ToString()); }
            catch { currEngineer = -1; }

            Int64 filterPipeSystem = 0;
            try { filterPipeSystem = Convert.ToInt64(Session["VSFEP_PipeSystemID"].ToString()); }
            catch { filterPipeSystem = 0; }
            Int64 filterPipeline = 0;
            try { filterPipeline = Convert.ToInt64(Session["VSFEP_PipelineID"].ToString()); }
            catch { filterPipeline = 0; }

            List<ValveSectionFinalEngineeringPool> model = (from v in db.ValveSection
                                                       join vss in db.ValveSectionStatus on v.ValveSectionStatusID equals vss.ValveSectionStatusID into vss1
                                                       from vssd in vss1.DefaultIfEmpty()
                                                       join dg in db.DisplayGroups on vssd.DisplayGroupID equals dg.DisplayGroupID into dg1
                                                       from dgd in dg1.DefaultIfEmpty()
                                                       join ps in db.PipeSystems on v.PipeSystemID equals ps.PipeSystemID into ps1
                                                       from psd in ps1.DefaultIfEmpty()
                                                       join p in db.Pipelines on v.PipelineID equals p.PipelineID into p1
                                                       from pd in p1.DefaultIfEmpty()
                                                       where (v.FinalEngineerID == 0 || v.FinalEngineerID == null)
                                                       && v.BuilderID != currEngineer
                                                       && v.QCID != currEngineer
                                                       && dgd.DisplayGroupName == "Final Engineering"
                                                       && (filterPipeSystem == 0 ? 
                                                        true : 
                                                        v.PipeSystemID == filterPipeSystem)
                                                       && (filterPipeline == 0 ? 
                                                        true : 
                                                        v.PipelineID == filterPipeline)
                                                       orderby psd.PipeSystemItem, pd.PipelineItem
                                                       select new ValveSectionFinalEngineeringPool
                                                       {
                                                           ValveSection = v,
                                                           PipeSystem = psd,
                                                           Pipeline = pd,
                                                           ValveStatus = vssd
                                                       }).ToList();
            
            ViewBag.PipeSystemID = new SelectList(db.PipeSystems, "PipeSystemID", "PipeSystemItem", filterPipeSystem);
            ViewBag.PipelineID = new SelectList(db.Pipelines, "PipelineID", "PipelineItem", filterPipeline);

            return model;
        }

        //
        // GET: /ValveSection/AnnualReview

        public ActionResult AnnualReview()
        {
            List<ValveSectionAnnualReview> model = new List<ValveSectionAnnualReview>();
            foreach (var item in AnnualReviewAssigned())
            {
                ValveSectionAnnualReview eng = new ValveSectionAnnualReview();
                eng.ValveSectionAnnualReviewAssigned = item;
                model.Add(eng);
            }
            foreach (var item in AnnualReviewPool())
            {
                ValveSectionAnnualReview eng = new ValveSectionAnnualReview();
                eng.ValveSectionAnnualReviewPool = item;
                model.Add(eng);
            }

            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult AnnualReviewDropDowns(int id1, int id2)
        {
            Session["VSARP_PipeSystemID"] = id1;
            Session["VSARP_PipelineID"] = id2;

            return Json("", JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /ValveSection/AnnualReviewAssigned

        public List<ValveSectionAnnualReviewAssigned> AnnualReviewAssigned()
        {
            Int64 currEngineer = -1;
            try { currEngineer = Convert.ToInt64(Session["UserID"].ToString()); }
            catch { currEngineer = -1; }

            List<ValveSectionAnnualReviewAssigned> model = (from v in db.ValveSection
                                                                join vss in db.ValveSectionStatus on v.ValveSectionStatusID equals vss.ValveSectionStatusID into vss1
                                                                from vssd in vss1.DefaultIfEmpty()
                                                                join dg in db.DisplayGroups on vssd.DisplayGroupID equals dg.DisplayGroupID into dg1
                                                                from dgd in dg1.DefaultIfEmpty()
                                                                join ps in db.PipeSystems on v.PipeSystemID equals ps.PipeSystemID into ps1
                                                                from psd in ps1.DefaultIfEmpty()
                                                                join p in db.Pipelines on v.PipelineID equals p.PipelineID into p1
                                                                from pd in p1.DefaultIfEmpty()
                                                                where v.AnnualReviewerID == currEngineer
                                                                && dgd.DisplayGroupName == "Annual Review"
                                                                orderby psd.PipeSystemItem, pd.PipelineItem
                                                                select new ValveSectionAnnualReviewAssigned
                                                                {
                                                                    ValveSection = v,
                                                                    PipeSystem = psd,
                                                                    Pipeline = pd,
                                                                    ValveStatus = vssd
                                                                }).ToList();

            return model;
        }
        public List<ValveSectionAnnualReviewPool> AnnualReviewPool()
        {
            Int64 currEngineer = -1;
            try { currEngineer = Convert.ToInt64(Session["UserID"].ToString()); }
            catch { currEngineer = -1; }

            Int64 filterPipeSystem = 0;
            try { filterPipeSystem = Convert.ToInt64(Session["VSARP_PipeSystemID"].ToString()); }
            catch { filterPipeSystem = 0; }
            Int64 filterPipeline = 0;
            try { filterPipeline = Convert.ToInt64(Session["VSARP_PipelineID"].ToString()); }
            catch { filterPipeline = 0; }

            List<ValveSectionAnnualReviewPool> model = (from v in db.ValveSection
                                                            join vss in db.ValveSectionStatus on v.ValveSectionStatusID equals vss.ValveSectionStatusID into vss1
                                                            from vssd in vss1.DefaultIfEmpty()
                                                            join dg in db.DisplayGroups on vssd.DisplayGroupID equals dg.DisplayGroupID into dg1
                                                            from dgd in dg1.DefaultIfEmpty()
                                                            join ps in db.PipeSystems on v.PipeSystemID equals ps.PipeSystemID into ps1
                                                            from psd in ps1.DefaultIfEmpty()
                                                            join p in db.Pipelines on v.PipelineID equals p.PipelineID into p1
                                                            from pd in p1.DefaultIfEmpty()
                                                            where (v.AnnualReviewerID == 0 || v.AnnualReviewerID == null)
                                                            && dgd.DisplayGroupName == "Annual Review"
                                                            && (filterPipeSystem == 0 ?
                                                             true :
                                                             v.PipeSystemID == filterPipeSystem)
                                                            && (filterPipeline == 0 ?
                                                             true :
                                                             v.PipelineID == filterPipeline)
                                                            orderby psd.PipeSystemItem, pd.PipelineItem
                                                            select new ValveSectionAnnualReviewPool
                                                            {
                                                                ValveSection = v,
                                                                PipeSystem = psd,
                                                                Pipeline = pd,
                                                                ValveStatus = vssd
                                                            }).ToList();

            ViewBag.PipeSystemID = new SelectList(db.PipeSystems, "PipeSystemID", "PipeSystemItem", filterPipeSystem);
            ViewBag.PipelineID = new SelectList(db.Pipelines, "PipelineID", "PipelineItem", filterPipeline);

            return model;
        }

        //
        // GET: /ValveSection/CertificationApproved

        public ActionResult CertificationApproved()
        {
            List<ValveSectionCertificationApproved> model = new List<ValveSectionCertificationApproved>();
            //foreach (var item in CertificationApprovedAssigned())
            //{
            //    ValveSectionCertificationApproved eng = new ValveSectionCertificationApproved();
            //    eng.ValveSectionCertificationApprovedAssigned = item;
            //    model.Add(eng);
            //}
            foreach (var item in CertificationApprovedPool())
            {
                ValveSectionCertificationApproved eng = new ValveSectionCertificationApproved();
                eng.ValveSectionCertificationApprovedPool = item;
                model.Add(eng);
            }

            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult CertificationApprovedDropDowns(int id1, int id2)
        {
            Session["VSCAP_PipeSystemID"] = id1;
            Session["VSCAP_PipelineID"] = id2;

            return Json("", JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /ValveSection/CertificationApprovedAssigned

        public List<ValveSectionCertificationApprovedAssigned> CertificationApprovedAssigned()
        {
            //Int64 currEngineer = -1;
            //try { currEngineer = Convert.ToInt64(Session["UserID"].ToString()); }
            //catch { currEngineer = -1; }

            //List<ValveSectionCertificationApprovedAssigned> model = (from v in db.ValveSection
            //                                                    join vss in db.ValveSectionStatus on v.ValveSectionStatusID equals vss.ValveSectionStatusID into vss1
            //                                                    from vssd in vss1.DefaultIfEmpty()
            //                                                    join dg in db.DisplayGroups on vssd.DisplayGroupID equals dg.DisplayGroupID into dg1
            //                                                    from dgd in dg1.DefaultIfEmpty()
            //                                                    join ps in db.PipeSystems on v.PipeSystemID equals ps.PipeSystemID into ps1
            //                                                    from psd in ps1.DefaultIfEmpty()
            //                                                    join p in db.Pipelines on v.PipelineID equals p.PipelineID into p1
            //                                                    from pd in p1.DefaultIfEmpty()
            //                                                    where v.FinalEngineerID == currEngineer
            //                                                    && dgd.DisplayGroupName == "Certification Approved"
            //                                                    orderby psd.PipeSystemItem, pd.PipelineItem, v.ValveSectionBegin
            //                                                         select new ValveSectionCertificationApprovedAssigned
            //                                                    {
            //                                                        ValveSection = v,
            //                                                        PipeSystem = psd,
            //                                                        Pipeline = pd,
            //                                                        ValveStatus = vssd
            //                                                    }).ToList();

            return null; // model;
        }
        public List<ValveSectionCertificationApprovedPool> CertificationApprovedPool()
        {
            //Int64 currEngineer = -1;
            //try { currEngineer = Convert.ToInt64(Session["UserID"].ToString()); }
            //catch { currEngineer = -1; }

            Int64 filterPipeSystem = 0;
            try { filterPipeSystem = Convert.ToInt64(Session["VSCAP_PipeSystemID"].ToString()); }
            catch { filterPipeSystem = 0; }
            Int64 filterPipeline = 0;
            try { filterPipeline = Convert.ToInt64(Session["VSCAP_PipelineID"].ToString()); }
            catch { filterPipeline = 0; }

            List<ValveSectionCertificationApprovedPool> model = (from v in db.ValveSection
                                                            join vss in db.ValveSectionStatus on v.ValveSectionStatusID equals vss.ValveSectionStatusID into vss1
                                                            from vssd in vss1.DefaultIfEmpty()
                                                            join dg in db.DisplayGroups on vssd.DisplayGroupID equals dg.DisplayGroupID into dg1
                                                            from dgd in dg1.DefaultIfEmpty()
                                                            join ps in db.PipeSystems on v.PipeSystemID equals ps.PipeSystemID into ps1
                                                            from psd in ps1.DefaultIfEmpty()
                                                            join p in db.Pipelines on v.PipelineID equals p.PipelineID into p1
                                                            from pd in p1.DefaultIfEmpty()
                                                            where dgd.DisplayGroupName == "Certification Approved"
                                                            && (filterPipeSystem == 0 ?
                                                             true :
                                                             v.PipeSystemID == filterPipeSystem)
                                                            && (filterPipeline == 0 ?
                                                             true :
                                                             v.PipelineID == filterPipeline)
                                                            orderby psd.PipeSystemItem, pd.PipelineItem
                                                            select new ValveSectionCertificationApprovedPool
                                                            {
                                                                ValveSection = v,
                                                                PipeSystem = psd,
                                                                Pipeline = pd,
                                                                ValveStatus = vssd
                                                            }).ToList();

            ViewBag.PipeSystemID = new SelectList(db.PipeSystems, "PipeSystemID", "PipeSystemItem", filterPipeSystem);
            ViewBag.PipelineID = new SelectList(db.Pipelines, "PipelineID", "PipelineItem", filterPipeline);

            return model;
        }

        //
        // GET: /ValveSection/Details/5

        public ActionResult Details(int id = 0)
        {
            ValveSection ValveSection = db.ValveSection.Find(id);
            if (ValveSection == null)
            {
                return HttpNotFound();
            }
            return View(ValveSection);
        }

        //
        // GET: /ValveSection/Create

        public ActionResult Create()
        {
            //ViewBag.PipeSystemID = new SelectList(db.PipeSystems, "PipeSystemID", "PipeSystemItem");
            ViewBag.PipelineID = new SelectList(db.Pipelines, "PipelineID", "PipelineItem");
            //ViewBag.ValveSectionStatusID = new SelectList(db.ValveSectionStatus, "ValveSectionStatusID", "ValveSectionStatusItem");
            //var builder = new SelectList((from u in db.Users.OrderBy(u => u.FirstName).ToList()
            //                              select new
            //                              {
            //                                  value = u.UserID,
            //                                  text = u.FirstName + " " + u.LastName
            //                              }),
            //                "value",
            //                "text",
            //                null); 
            //ViewBag.BuilderID = builder;
            
            var builder = (from u in db.Users.OrderBy(u => u.FirstName)
                           join ut in db.UsersTypes on u.UserID equals ut.UserID
                           join gc in db.GroupClassifications on ut.GroupClassificationID equals gc.GroupClassificationID
                           where gc.GroupClassificationID == 2
                           select new
                           {
                               value = u.UserID,
                               text = u.FirstName + " " + u.LastName
                           });


            ViewBag.BuilderID = new SelectList(builder, "value", "text");

            
            var qcer = (from u in db.Users.OrderBy(u => u.FirstName)
                           join ut in db.UsersTypes on u.UserID equals ut.UserID
                           join gc in db.GroupClassifications on ut.GroupClassificationID equals gc.GroupClassificationID
                           where gc.GroupClassificationID == 3
                           select new
                           {
                               value = u.UserID,
                               text = u.FirstName + " " + u.LastName
                           });


            ViewBag.QCID = new SelectList(qcer, "value", "text");

            var eng = (from u in db.Users.OrderBy(u => u.FirstName)
                        join ut in db.UsersTypes on u.UserID equals ut.UserID
                        join gc in db.GroupClassifications on ut.GroupClassificationID equals gc.GroupClassificationID
                        where gc.GroupClassificationID == 4
                        select new
                        {
                            value = u.UserID,
                            text = u.FirstName + " " + u.LastName
                        });


            ViewBag.EngineerID = new SelectList(eng, "value", "text");

            var caid = (from u in db.Users.OrderBy(u => u.FirstName)
                       join ut in db.UsersTypes on u.UserID equals ut.UserID
                       join gc in db.GroupClassifications on ut.GroupClassificationID equals gc.GroupClassificationID
                       where gc.GroupClassificationID == 6
                       select new
                       {
                           value = u.UserID,
                           text = u.FirstName + " " + u.LastName
                       });


            ViewBag.CAID = new SelectList(caid, "value", "text");

            return View();
        }

        //
        // POST: /ValveSection/Create

        [HttpPost]
        public ActionResult Create(ValveSection ValveSection)
        {
            //bool hasError = false;
            //if (ValveSection.ValveSectionID == 0)
            //{
            //    ModelState.AddModelError("ValveSectionID", "Please enter valid value to CircuitID");
            //    hasError = true;
            //}
            //if (ValveSection.PipelineID == 0)
            //{
            //    ModelState.AddModelError("PipelineID", "Please select value to Station");
            //    hasError = true;
            //}
            //if (ValveSection.QCID == null)
            //{
            //    ModelState.AddModelError("QCID", "Please select value to Qcer");
            //    hasError = true;
            //}
            //if (ValveSection.EngineerID == null)
            //{
            //    ModelState.AddModelError("EngineerID", "Please select value to Engineer");
            //    hasError = true;
            //}
            //if (ValveSection.CAID == null)
            //{
            //    ModelState.AddModelError("CAID", "Please select value to Certification Approver");
            //    hasError = true;
            //}            

            //if(hasError)
            //    return View(ValveSection);
                       

            //var errors = ModelState
            //.Where(x => x.Value.Errors.Count > 0)
            //.Select(x => new { x.Key, x.Value.Errors })
            //.ToArray();

            //if (!ModelState.IsValid)
            //    return View(ValveSection);

            ValveSection.CreatedBy_UserID = Convert.ToInt64(Session["UserID"].ToString());
            ValveSection.ModifiedBy_UserID = Convert.ToInt64(Session["UserID"].ToString());
            ValveSection.CreatedOn = DateTime.Now;
            ValveSection.ModifiedOn = DateTime.Now;
            ValveSection.PipeSystemID = (from p in db.Pipelines where p.PipelineID == ValveSection.PipelineID select p.PipeSystemID).FirstOrDefault();

            db.ValveSection.Add(ValveSection);
            db.SaveChanges();

            // Workflow Actions IDs
            var approve = (from w in db.WorkflowActions
                            where w.WorkflowActionItem == "Approve"
                            select new { w.WorkflowActionID }).FirstOrDefault();
            //var unassigned = (from v in db.ValveSectionStatus where v.ValveSectionStatusItem == "Unassigned / New"
            //                  select new { v.ValveSectionStatusID }).FirstOrDefault();
            var unassigned = (from v in db.ValveSectionStatus
                                where v.ValveSectionStatusItem == "New Circuit"
                                select new { v.ValveSectionStatusID }).FirstOrDefault();

            InsertWorkHistory(ValveSection, 0, approve.WorkflowActionID, unassigned.ValveSectionStatusID);
            ValveSection.ValveSectionStatusID = unassigned.ValveSectionStatusID;
            if (ValveSection.BuilderID != null)
            {
                var readyforbuild = (from v in db.ValveSectionStatus
                                        where v.ValveSectionStatusItem == "Ready for Build"
                                        select new { v.ValveSectionStatusID }).FirstOrDefault();
                InsertWorkHistory(ValveSection, unassigned.ValveSectionStatusID, approve.WorkflowActionID, readyforbuild.ValveSectionStatusID);
                ValveSection.ValveSectionStatusID = readyforbuild.ValveSectionStatusID;

                db.Entry(ValveSection).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
                    
            }
            else
            {
                var readyforbuild = (from v in db.ValveSectionStatus
                                            //where v.ValveSectionStatusItem == "Ready for Build"
                                        where v.ValveSectionStatusItem == "New Circuit"
                                        select new { v.ValveSectionStatusID }).FirstOrDefault();
                InsertWorkHistory(ValveSection, unassigned.ValveSectionStatusID, approve.WorkflowActionID, readyforbuild.ValveSectionStatusID);
                ValveSection.ValveSectionStatusID = readyforbuild.ValveSectionStatusID;

                db.Entry(ValveSection).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
                //return View(ValveSection);
            }
            
            return View(ValveSection);
        }

        //
        // GET: /ValveSection/Edit/5

        public ActionResult Edit(int id = 0)
        {
            ValveSection ValveSection = db.ValveSection.Find(id);
            if (ValveSection == null)
            {
                return HttpNotFound();
            }
            ViewBag.PipeSystemID = new SelectList(db.PipeSystems, "PipeSystemID", "PipeSystemItem", ValveSection.PipeSystemID);
            ViewBag.PipelineID = new SelectList(db.Pipelines, "PipelineID", "PipelineItem", ValveSection.PipelineID);
            //ViewBag.ValveSectionStatusID = new SelectList(db.ValveSectionStatus, "ValveSectionStatusID", "ValveSectionStatusItem", ValveSection.ValveSectionStatusID);
            var builder = new SelectList((from u in db.Users.OrderBy(u => u.FirstName).ToList()
                                          select new
                                          {
                                              value = u.UserID,
                                              text = u.FirstName + " " + u.LastName
                                          }),
                            "value",
                            "text", ValveSection.BuilderID);
            ViewBag.BuilderID = builder;
            var qc = new SelectList((from u in db.Users.OrderBy(u => u.FirstName).ToList()
                                          select new
                                          {
                                              value = u.UserID,
                                              text = u.FirstName + " " + u.LastName
                                          }),
                            "value",
                            "text", ValveSection.QCID);
            ViewBag.QCID = qc;
            var engineer = new SelectList((from u in db.Users.OrderBy(u => u.FirstName).ToList()
                                          select new
                                          {
                                              value = u.UserID,
                                              text = u.FirstName + " " + u.LastName
                                          }),
                            "value",
                            "text", ValveSection.EngineerID);
            ViewBag.EngineerID = engineer;

            var certifier = new SelectList((from u in db.Users.OrderBy(u => u.FirstName).ToList()
                                           select new
                                           {
                                               value = u.UserID,
                                               text = u.FirstName + " " + u.LastName
                                           }),
                "value",
                "text", ValveSection.CAID);
            ViewBag.CAID = certifier;

            //var finalengineer = new SelectList((from u in db.Users.OrderBy(u => u.FirstName).ToList()
            //                               select new
            //                               {
            //                                   value = u.UserID,
            //                                   text = u.FirstName + " " + u.LastName
            //                               }),
            //                "value",
            //                "text", ValveSection.FinalEngineerID);
            //ViewBag.FinalEngineerID = finalengineer;
            return View(ValveSection);
        }

        //
        // POST: /ValveSection/Edit/5

        [HttpPost]
        public ActionResult Edit(ValveSection ValveSection)
        {
            if (ModelState.IsValid)
            {
                ValveSection.ModifiedBy_UserID = Convert.ToInt64(Session["UserID"].ToString());
                ValveSection.ModifiedOn = DateTime.Now;
                //ValveSection.IsSegmentationDirty = true;
                //ValveSection.LengthDiscrepancyPlus = (ValveSection.OrionStationEnd - ValveSection.OrionStationBegin) - ValveSection.PFLLength;
                db.Entry(ValveSection).State = EntityState.Modified;
                db.SaveChanges();

                // Workflow Actions IDs
                var approve = (from w in db.WorkflowActions
                               where w.WorkflowActionItem == "Approve"
                               select new { w.WorkflowActionID }).FirstOrDefault();
                var unassigned = (from v in db.ValveSectionStatus
                                  where v.ValveSectionStatusItem == "New Circuit"
                                  select new { v.ValveSectionStatusID }).FirstOrDefault();

                if (ValveSection.BuilderID != null && ValveSection.ValveSectionStatusID == unassigned.ValveSectionStatusID)
                {
                    var readyforbuild = (from v in db.ValveSectionStatus
                                         where v.ValveSectionStatusItem == "Ready for Build"
                                         select new { v.ValveSectionStatusID }).FirstOrDefault();
                    InsertWorkHistory(ValveSection, unassigned.ValveSectionStatusID, approve.WorkflowActionID, readyforbuild.ValveSectionStatusID);
                    ValveSection.ValveSectionStatusID = readyforbuild.ValveSectionStatusID;
                    //ValveSection.IsSegmentationDirty = true;

                    db.Entry(ValveSection).State = EntityState.Modified;
                    db.SaveChanges();
                }


                return RedirectToAction("Index");
            }
            ViewBag.PipeSystemID = new SelectList(db.PipeSystems, "PipeSystemID", "PipeSystemItem", ValveSection.PipeSystemID);
            ViewBag.PipelineID = new SelectList(db.Pipelines, "PipelineID", "PipelineItem", ValveSection.PipelineID);
            //ViewBag.ValveSectionStatusID = new SelectList(db.ValveSectionStatus, "ValveSectionStatusID", "ValveSectionStatusItem", ValveSection.ValveSectionStatusID);
            var builder = new SelectList((from u in db.Users.OrderBy(u => u.FirstName).ToList()
                                          select new
                                          {
                                              value = u.UserID,
                                              text = u.FirstName + " " + u.LastName
                                          }),
                            "value",
                            "text", ValveSection.BuilderID);
            ViewBag.BuilderID = builder;
            var qc = new SelectList((from u in db.Users.OrderBy(u => u.FirstName).ToList()
                                     select new
                                     {
                                         value = u.UserID,
                                         text = u.FirstName + " " + u.LastName
                                     }),
                            "value",
                            "text", ValveSection.QCID);
            ViewBag.QCID = qc;
            var engineer = new SelectList((from u in db.Users.OrderBy(u => u.FirstName).ToList()
                                           select new
                                           {
                                               value = u.UserID,
                                               text = u.FirstName + " " + u.LastName
                                           }),
                            "value",
                            "text", ValveSection.EngineerID);
            ViewBag.EngineerID = engineer;
            var finalengineer = new SelectList((from u in db.Users.OrderBy(u => u.FirstName).ToList()
                                           select new
                                           {
                                               value = u.UserID,
                                               text = u.FirstName + " " + u.LastName
                                           }),
                            "value",
                            "text", ValveSection.FinalEngineerID);
            ViewBag.FinalEngineerID = finalengineer;
            return View(ValveSection);
        }

        //
        // GET: /ValveSection/AssignedEdit/5

        public ActionResult AssignedEdit(int id = 0)
        {
            ValveSection ValveSection = db.ValveSection.Find(id);
            if (ValveSection == null)
            {
                return HttpNotFound();
            }

            ViewBag.PipeSystemItem = (from p in db.PipeSystems where p.PipeSystemID == ValveSection.PipeSystemID select new { p.PipeSystemItem }).FirstOrDefault().PipeSystemItem;
            ViewBag.PipelineItem = (from p in db.Pipelines where p.PipelineID == ValveSection.PipelineID select new { p.PipelineItem }).FirstOrDefault().PipelineItem;
            //ViewBag.ValveSectionStatusItem = (from v in db.ValveSectionStatus where v.ValveSectionStatusID == ValveSection.ValveSectionStatusID select new { v.ValveSectionStatusItem }).FirstOrDefault().ValveSectionStatusItem;
            return View(ValveSection);
        }

        //
        // POST: /ValveSection/AssignedEdit/5

        [HttpPost]
        public ActionResult AssignedEdit(ValveSection ValveSection)
        {
            if (ModelState.IsValid)
            {
                ValveSection.ModifiedBy_UserID = Convert.ToInt64(Session["UserID"].ToString());
                ValveSection.ModifiedOn = DateTime.Now;

                db.Entry(ValveSection).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Overview", new { ValveSectionID = ValveSection.ValveSectionID });
            }
            return View(ValveSection);
        }

        //
        // GET: /ValveSection/Delete/5

        public ActionResult Delete(int id = 0)
        {
            ValveSection ValveSection = db.ValveSection.Find(id);
            if (ValveSection == null)
            {
                return HttpNotFound();
            }
            var pipesystem = from p in db.PipeSystems where p.PipeSystemID == ValveSection.PipeSystemID select new { p.PipeSystemItem }.PipeSystemItem;
            var pipeline = from p in db.Pipelines where p.PipelineID == ValveSection.PipelineID select new { p.PipelineItem }.PipelineItem;
            ViewBag.PipeSystemItem = pipesystem.First();
            ViewBag.PipelineItem = pipeline.First();
            return View(ValveSection);
        }

        //
        // POST: /ValveSection/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            ValveSection ValveSection = db.ValveSection.Find(id);
            db.ValveSection.Remove(ValveSection);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //
        // GET: /ValveSection/Crossings/5

        //public ActionResult Crossings(int id = 0)
        //{
        //    ValveSection ValveSection = db.ValveSection.Find(id);
        //    if (ValveSection == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    Session["CurrentValveSection"] = ValveSection.ValveSectionID;
        //    Session["CurrentOrionStationSeries"] = ValveSection.OrionStationSeries;

        //    string crossingstatus;
        //    if (ValveSection.CrossingsStatus == null)
        //        crossingstatus = "M";
        //    else
        //        crossingstatus = ValveSection.CrossingsStatus;
        //    SelectList sl = new SelectList(new[]{
        //      new SelectListItem{ Text="Crossings Imported", Value="Y"},
        //      new SelectListItem{ Text="Crossings Not Applicable", Value="N"},
        //      new SelectListItem{ Text="Crossings Not Available", Value="M"}
        //    }, "Value", "Text", crossingstatus);
        //    ViewBag.CrossingStatus = sl;
            
        //    return View();
        //}

        //
        // POST: /ValveSection/CrossingsImport
        //[HttpPost]
        //public ActionResult Crossings(HttpPostedFileBase file, ValveSectionCrossings ValveSectionCrossing)
        //{
        //    bool uploaded = false;

        //    ValveSection valvesection = db.ValveSection.Find(Convert.ToInt64(Session["CurrentValveSection"].ToString()));
            
        //    if (file != null)
        //    {
        //        if (file.ContentLength > 0)
        //        {
        //            Int64 currValveSectionID = Convert.ToInt64(Session["CurrentValveSection"].ToString());

        //            var fileName = Path.GetFileName(file.FileName);

        //            // Get Orion Station Series for the Valve Section
        //            var OrionStation = (from v in db.ValveSection
        //                                where v.ValveSectionID == currValveSectionID
        //                                select new { v.OrionStationSeries }).FirstOrDefault().OrionStationSeries;

        //            // Determine what database we are on
        //            var sqlDB = new System.Data.SqlClient.SqlConnectionStringBuilder(
        //            System.Configuration.ConfigurationManager.ConnectionStrings["PipelineFeatureListDBContext"].ConnectionString);
        //            var dbName = sqlDB.InitialCatalog;

        //            // Get the upload directory
        //            var UploadDirectories = (from u in db.UploadDirectories
        //                                   where u.DatabaseName == dbName
        //                                select new { u.Upload_Directory, u.Local_Directory }).FirstOrDefault();

        //            // Rename file to standard
        //            string datetime = DateTime.Now.ToString("MMddyyyy_HHmmss");
        //            var path = Path.GetFullPath(UploadDirectories.Upload_Directory + dbName + @"\CVT" + OrionStation + "_" + datetime + ".xlsx");
        //            file.SaveAs(path);

        //            // Call stored procedure to do the import
        //            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["PipelineFeatureListDBContext"].ConnectionString);
        //            conn.Open();
        //            SqlCommand cmd = new SqlCommand("spImport_CrossingData", conn);
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.Add(new SqlParameter("@UserID", Session["UserID"].ToString()));
        //            cmd.Parameters.Add(new SqlParameter("@ValveSectionID", currValveSectionID));
        //            cmd.Parameters.Add(new SqlParameter("@UploadIdentifier", UploadDirectories.Local_Directory + dbName + @"\CVT" + OrionStation + "_" + datetime + ".xlsx"));
        //            cmd.Parameters.Add(new SqlParameter("@ReturnMessage", ""));
        //            cmd.ExecuteNonQuery();
        //            conn.Close();
        //            conn.Dispose();
                    
        //            uploaded = true;
        //        }
        //    }

        //    if (uploaded)
        //    {
        //        valvesection.CrossingsStatus = "Y";
        //        valvesection.IsSegmentationDirty = true;
        //    }
        //    else if (valvesection.CrossingsStatus != "Y")
        //        valvesection.CrossingsStatus = ValveSectionCrossing.CrossingStatus;
        //    else
        //        valvesection.CrossingsStatus = "M";
        //    db.Entry(valvesection).State = EntityState.Modified;
        //    db.SaveChanges();

        //    return RedirectToAction("Index", "Overview", new { ValveSectionID = Session["CurrentValveSection"].ToString(), OrionStationSeries = Session["CurrentOrionStationSeries"].ToString() });
        //}

        //
        // GET: /ValveSection/StatusChange/5

        public ActionResult StatusChange(int id = 0, int oldStatus = 0, int newStatus = 0, string description = "")
        {
            ValveSection ValveSection = db.ValveSection.Find(id);
            if (ValveSection == null)
            {
                return HttpNotFound();
            }
            
            ViewBag.Message = description;
            Session["StatusChangeNewStatusID"] = newStatus;
            ViewBag.ValveSectionStatusItem = (from v in db.ValveSectionStatus where v.ValveSectionStatusID == ValveSection.ValveSectionStatusID select new { v.ValveSectionStatusItem }).FirstOrDefault().ValveSectionStatusItem;
            return View(ValveSection);
        }

        //
        // POST: /ValveSection/StatusChange/5

        [HttpPost]
        public ActionResult StatusChange(ValveSection ValveSection)
        {
            if (ModelState.IsValid)
            {
                ValveSection.ModifiedBy_UserID = Convert.ToInt64(Session["UserID"].ToString());
                ValveSection.ModifiedOn = DateTime.Now;

                // Original/New Statuses
                int? origStatus = ValveSection.ValveSectionStatusID;
                int? newStatus = Convert.ToInt32(Session["StatusChangeNewStatusID"].ToString());

                // Original Display Groups to know where to redirect
                //var origDisplay = (from d in db.DisplayGroups 
                //                              join v in db.ValveSectionStatus on d.DisplayGroupID equals v.DisplayGroupID
                //                              where v.ValveSectionStatusID == ValveSection.ValveSectionStatusID
                //                              select new { d.DisplayGroupName }).FirstOrDefault();

                // Approve or Reject?
                var approveORreject = (from w in db.WorkflowRules
                                       join a in db.WorkflowActions on w.WorkflowActionID equals a.WorkflowActionID
                                       where w.Old_ValveSectionStatusID == origStatus
                                       && w.New_ValveSectionStatusID == newStatus
                                       select new { w.WorkflowActionID, w.WorkflowAction }).FirstOrDefault();

                // New Status Actions
                var newStatusActions = (from v in db.ValveSectionStatus
                                        where v.ValveSectionStatusID == newStatus
                                        select new { v.AssignBuilder, v.AssignQC, v.AssignEngineer, v.AssignFinalEngineer, v.AssignAnnualReviewer,
                                            v.CopyDataToHistory, v.GenerateGrades, v.QueueCertification, v.DynamicSegmentation, v.RedirectAction }).FirstOrDefault();

                // Assign Users as necessary
               //commented by Siva
                //if (newStatusActions.AssignBuilder && approveORreject.WorkflowAction.WorkflowActionItem == "Approve")
                //    ValveSection.BuilderID = Convert.ToInt64(Session["UserID"].ToString());
                //if (newStatusActions.AssignQC && approveORreject.WorkflowAction.WorkflowActionItem == "Approve")
                //    ValveSection.QCID = Convert.ToInt64(Session["UserID"].ToString());
                //if (newStatusActions.AssignEngineer && approveORreject.WorkflowAction.WorkflowActionItem == "Approve")
                //    ValveSection.EngineerID = Convert.ToInt64(Session["UserID"].ToString());
                //if (newStatusActions.AssignFinalEngineer && approveORreject.WorkflowAction.WorkflowActionItem == "Approve")
                //    ValveSection.FinalEngineerID = Convert.ToInt64(Session["UserID"].ToString());
                //if (newStatusActions.AssignAnnualReviewer && approveORreject.WorkflowAction.WorkflowActionItem == "Approve")
                //    ValveSection.AnnualReviewerID = Convert.ToInt64(Session["UserID"].ToString());

                ValveSection.ValveSectionStatusID = newStatus;
                db.Entry(ValveSection).State = EntityState.Modified;
                db.SaveChanges();

                InsertWorkHistory(ValveSection, origStatus.Value, approveORreject.WorkflowActionID, newStatus.Value);

                //int copytohistory = 0, generategrades = 0, dynamicsegmentation = 0;
                //if (newStatusActions.CopyDataToHistory && approveORreject.WorkflowAction.WorkflowActionItem == "Approve")
                //    //CopyToHistory(ValveSection);
                //    copytohistory = 1;
                //if (newStatusActions.GenerateGrades)
                //    //GenerateGrades(ValveSection);
                //    generategrades = 1;

                //PH 2014.05.22 need user id
                //PipelineFeatureList.AppCode.AppLibrary.CopyToHistoryGenerateGradesDynamicSegmentation(ValveSection.ValveSectionID, copytohistory, generategrades, dynamicsegmentation);
                //PipelineFeatureList.AppCode.AppLibrary.CopyToHistoryGenerateGradesDynamicSegmentation(
                //    Convert.ToInt64(Session["UserID"].ToString()),
                //    ValveSection.ValveSectionID,
                //    copytohistory,
                //    generategrades,
                //    dynamicsegmentation);
                //PH 2014.05.22 end edit

                if (newStatusActions.QueueCertification)
                    QueueCertification(ValveSection);

                if(origStatus == 3 && newStatus == 4)
                {
                    return RedirectToAction("BuildAssigned", "ValveSection");
                }
                else if (origStatus == 5 && (newStatus == 2 || newStatus == 6))
                {
                    return RedirectToAction("QC", "ValveSection");
                }                            
                else if (origStatus == 7 && (newStatus == 2 || newStatus == 4 || newStatus == 8))
                {
                    return RedirectToAction("Engineering", "ValveSection"); 
                }
                else if(newStatus == 2)
                {
                    return RedirectToAction("BuildAssigned", "ValveSection");
                }
                // Return to valve sections list if out of user's control
                else if (newStatusActions.RedirectAction == null)
                    return RedirectToAction("Index", "Overview", new { ValveSectionID = Session["CurrentValveSection"].ToString() });

                return RedirectToAction("Index");               

                // Return to Overview screen if still in user's control

                //return RedirectToAction("Index", "Overview", new { ValveSectionID = Session["CurrentValveSection"].ToString(), OrionStationSeries = Session["CurrentOrionStationSeries"].ToString() });
                //return RedirectToAction("Index", "Overview", new { ValveSectionID = Session["CurrentValveSection"].ToString() });
                

            }
            ViewBag.Message = "An error occurred while processing your request.";
            return View(ValveSection);
        }

        private void InsertWorkHistory(ValveSection valvesection, int origStatusID, int actionID, int newsStatusID)
        {
            PipelineFeatureListDBContext db1 = new PipelineFeatureListDBContext(); 
            
            WorkflowHistory wf = new WorkflowHistory();
            wf.ValveSectionID = Convert.ToInt64(valvesection.ValveSectionID);
            wf.ChangedBy_UserID = Convert.ToInt64(Session["UserID"].ToString());
            wf.ChangedOn = DateTime.Now;
            wf.Old_WorkflowStatusID = origStatusID;
            wf.WorkflowActionID = actionID;
            wf.New_WorkflowStatusID = newsStatusID; 

            db1.WorkflowHistories.Add(wf);
            db1.SaveChanges();
        }
        private void CopyToHistory(ValveSection valvesection)
        {
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["PipelineFeatureListDBContext"].ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("CopyToHistory", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@ValveSectionID", valvesection.ValveSectionID));
            cmd.ExecuteNonQuery();
            conn.Close();
            conn.Dispose();
        }

        //PH 2014.05.23 methods no longer in use.
        //[AsyncTimeout(120000)]
        //private void GenerateGrades(ValveSection valvesection)
        //{
        //    SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["PipelineFeatureListDBContext"].ConnectionString);
        //    conn.Open();
        //    SqlCommand cmd = new SqlCommand("GenerateGradesAll", conn);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.Add(new SqlParameter("@ValveSectionID", valvesection.ValveSectionID));
        //    cmd.BeginExecuteNonQuery(delegate (IAsyncResult ar)
        //    {
        //        cmd.EndExecuteNonQuery(ar);
        //        cmd.Dispose();
        //        conn.Close();
        //        conn.Dispose();
        //    }, null);
        //}
        //private void DynamicSegmentation(ValveSection valvesection)
        //{
        //    ValveSection ValveSection = db.ValveSection.Find(valvesection.ValveSectionID);
        //    ValveSection.OrionStationSeries = valvesection.OrionStationSeries.Replace('\t',' ').Trim();

        //    DynamicSegmentation ds = new DynamicSegmentation
        //    {
        //        QueuedOn = DateTime.Now,
        //        ValveSectionID = valvesection.ValveSectionID,
        //        Engineer_UserID = Convert.ToInt64(Session["UserID"].ToString()),
        //        ProcessingMessage = "Queued for Segmentation"
        //    };
        //    db.DynamicSegmentations.Add(ds);
        //    db.SaveChanges();

        //    int vsStatus = (from v in db.ValveSectionStatus
        //                    where v.ValveSectionStatusItem == "Dynamic Segmentation Queued"
        //                    select v.ValveSectionStatusID).First();

        //    valvesection.ValveSectionStatusID = vsStatus;
        //    db.Entry(valvesection).State = EntityState.Modified;
        //    db.SaveChanges();
        //}
        private void QueueCertification(ValveSection valvesection)
        {
            PipelineFeatureListDBContext db1 = new PipelineFeatureListDBContext();

            CertificationApproved ca = new CertificationApproved();
            ca.ValveSectionID = Convert.ToInt64(valvesection.ValveSectionID);
            ca.CertificationDate = DateTime.Now;
            ca.Status = 1;

            db1.CertificationApproveds.Add(ca);
            db1.SaveChanges();
        }
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}