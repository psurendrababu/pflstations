using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PipelineFeatureList.Models;

namespace PipelineFeatureList.Controllers
{
    public class ClientReportController : Controller
    {
        private PipelineFeatureListDBContext db = new PipelineFeatureListDBContext();

        //
        // GET: /ClientReport/

        public ActionResult Index()
        {
            var model = new ClientReportScreen();

            List<Report> rdList = db.Reports.OrderBy(d => d.Description).ToList();
            ViewBag.MultiSelectReports = new MultiSelectList(rdList, "ReportID", "Description");
            List<PipeSystem> psList = db.PipeSystems.OrderBy(p => p.PipeSystemItem).ToList();
            ViewBag.MultiSelectPipeSystems = new MultiSelectList(psList, "PipeSystemID", "PipeSystemItem");
            var plList = db.Pipelines.Include("PipeSystem")
                .Select(p => new { Value = p.PipelineID, Text = p.PipeSystem.PipeSystemItem + " - " + p.PipelineItem})
                .OrderBy(p => p.Text).ToList();
            ViewBag.MultiSelectPipelines = new MultiSelectList(plList, "Value", "Text");

            List<ClientImportList> ciList = db.ClientImportLists.OrderBy(c => c.FileName).ToList();
            ViewBag.MultiSelectClientImports = new MultiSelectList(ciList, "ClientImportListID", "FileName");

            model.ClientReportData = new ClientReport();
            model.ClientReportData.FromDate = DateTime.Now;
            model.ClientReportData.ToDate = DateTime.Now;

            return View(model);
        }

        //
        // POST: /ValveSectionFeature/Create

        [HttpPost]
        public ActionResult Index(ClientReportScreen crs, int[] Reports, int[] PipeSystems, int[] Pipelines, int[] ClientImports, string FromDate, string ToDate, string importFile)
        {
            if (ModelState.IsValid)
            {
                ClientReport cr = new ClientReport();
                cr.CreatedOn = DateTime.Now;
                cr.CreatedBy_UserID = Convert.ToInt64(Session["UserID"].ToString());
                cr.FromDate = Convert.ToDateTime(FromDate);
                cr.ToDate = Convert.ToDateTime(ToDate);
                cr.ImportFile = importFile;
                cr.PermanentImport = true;
                cr.Processed = false;
                
                db.ClientReports.Add(cr);
                db.SaveChanges();
                Int64 clientreportID = db.ClientReports.Select(c => c.ClientReportID).Max();

                if (Reports != null)
                {
                    ClientReportReportList crrl = new ClientReportReportList();
                    foreach (var ReportID in Reports)
                    {
                        crrl.ClientReportID = clientreportID;
                        crrl.ReportID = ReportID;
                        db.ClientReportReportLists.Add(crrl);
                        db.SaveChanges();
                    }
                }
                if (PipeSystems != null)
                {
                    ClientReportPipeSystem crps = new ClientReportPipeSystem();
                    foreach (var PipeSystemID in PipeSystems)
                    {
                        crps.ClientReportID = clientreportID;
                        crps.PipeSystemID = PipeSystemID;
                        db.ClientReportPipeSystems.Add(crps);
                        db.SaveChanges();
                    }
                }
                if (Pipelines != null)
                {
                    ClientReportPipeline crpl = new ClientReportPipeline();
                    foreach (var PipelineID in Pipelines)
                    {
                        crpl.ClientReportID = clientreportID;
                        crpl.PipelineID = PipelineID;
                        db.ClientReportPipelines.Add(crpl);
                        db.SaveChanges();
                    }
                }
                if (ClientImports != null)
                {
                    ClientReportImportList cril = new ClientReportImportList();
                    foreach (var ClientImportID in ClientImports)
                    {
                        cril.ClientReportID = clientreportID;
                        cril.ClientImportListID = ClientImportID;
                        db.ClientReportImportLists.Add(cril);
                        db.SaveChanges();
                    }
                }
                return RedirectToAction("Index", "Admin", null);
            }

            return View(crs);
        }
    }
}
