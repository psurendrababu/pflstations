using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PipelineFeatureList.Models;
using System.Data.SqlClient;
using Microsoft.Reporting.WebForms;
using System.Collections;
using System.Threading;

namespace PipelineFeatureList.Controllers
{
    public class ReportingController : Controller
    {
        //
        // GET: /Reporting/

        public ActionResult Index()
        {
            return View();
        }

        private ReportParameter CreateReportParameter(string paramName, string pramValue)
        {
            ReportParameter aParam = new ReportParameter(paramName, pramValue);
            return aParam;
        }

        private ArrayList ReportDefaultParam()
        {
            ArrayList arrLstDefaultParam = new ArrayList();
            arrLstDefaultParam.Add(CreateReportParameter("ValveSectionID", Session["CurrentValveSection"].ToString()));
            return arrLstDefaultParam;
        }

        /// <summary>
        /// Standard SSRS rendering extensions, in the format of <RenderingExtension, FilenameExtension>
        /// </summary>
        public static readonly System.Collections.Specialized.StringDictionary ReportFormats = new System.Collections.Specialized.StringDictionary
        {
            {"PDF", "pdf"},
            {"HTML4.0", "html"},
            {"MHTML", "mhtml"},
            {"CSV", "csv"},
            {"EXCEL", "xls"},
            {"XML", "xml"}
        };

        public FileResult DownloadReport(string ReportName, string ReportFormat)
        {
            // Verify ReportFormat is passed
            if (!ReportFormats.ContainsKey(ReportFormat))
            {
                ArgumentException ex = new ArgumentException(string.Format(@"Unrecognized report format ""?"".", ReportFormat));
                throw ex;
            }

            // If DSR report, see if needs to be resegmented before reporting
            if (ReportName == "Dynamic Segmentation Records")
            {
                //Resegment();
                //PH 2014.05.22 need user id
                //PipelineFeatureList.AppCode.AppLibrary.CopyToHistoryGenerateGradesDynamicSegmentation(Convert.ToInt64(Session["CurrentValveSection"].ToString()), 0, 0, 1);
                PipelineFeatureList.AppCode.AppLibrary.CopyToHistoryGenerateGradesDynamicSegmentation(
                    Convert.ToInt64(Session["UserID"].ToString()),
                    Convert.ToInt64(Session["CurrentValveSection"].ToString()),
                    0,
                    0,
                    1); 
                //PH 2014.05.22 end edit
                Thread.Sleep(10000);
            }

            // Get connected database
            var sqlDB = new System.Data.SqlClient.SqlConnectionStringBuilder(
                System.Configuration.ConfigurationManager.ConnectionStrings["PipelineFeatureListDBContext"].ConnectionString);
            var dbName = sqlDB.InitialCatalog;
            var dbDataSource = sqlDB.DataSource;


            // Set report path
            ReportViewer rptViewer = new ReportViewer();
            string urlReportServer = "http://" + dbDataSource + "/ReportServer"; // "http://g2dev.g2partnersllc.local/ReportServer";
            rptViewer.ProcessingMode = ProcessingMode.Remote; // ProcessingMode will be Either Remote or Local
            rptViewer.ServerReport.ReportServerUrl = new Uri(urlReportServer); //Set the ReportServer Url
            rptViewer.ServerReport.ReportPath = "/" + dbName + "/" + ReportName; //Passing the Report Path

            //Creating an ArrayList for combine the Parameters which will be passed into SSRS Report
            ArrayList reportParam = new ArrayList();
            reportParam = ReportDefaultParam();

            ReportParameter[] param = new ReportParameter[reportParam.Count];
            for (int k = 0; k < reportParam.Count; k++)
            {
                param[k] = (ReportParameter)reportParam[k];
            }

            // pass crendentials
            IReportServerCredentials irsc = new CustomReportCredentials();
            rptViewer.ServerReport.ReportServerCredentials = irsc;

            //pass parmeters to report
            rptViewer.ServerReport.SetParameters(param); //Set Report Parameters

            rptViewer.ServerReport.Refresh();

            byte[] streamBytes = null;
            string mimeType = "";
            string encoding = "";
            string filenameExtension = "";
            string[] streamids = null;
            Warning[] warnings = null;
            string filename = ReportName + "." + ReportFormats[ReportFormat];

            streamBytes = rptViewer.ServerReport.Render(ReportFormat, null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);
            return File(streamBytes, mimeType, filename);
        }

        public ActionResult ASPXView()
        {
            return View();
        }

        public ActionResult ASPXUserControl()
        {
            return View();
        }

        ///PH 2014.05.22 - cleanup to isolate where to put record creation for DynamicSegmentation.  This function has no references.
        //public void Resegment()
        //{
        //    PipelineFeatureListDBContext db = new PipelineFeatureListDBContext();
        //    int currSection = Convert.ToInt16(Session["CurrentValveSection"].ToString());
            
        //    // Valve Section
        //    var VS = db.ValveSection
        //        .Where(v => v.ValveSectionID == currSection)
        //        .ToList();
        //    if (VS[0].IsSegmentationDirty == true)
        //    {
        //        ValveSection valvesection = db.ValveSection.Where(v => v.ValveSectionID == currSection).First();

        //        DynamicSegmentation ds = new DynamicSegmentation
        //        {
        //            Engineer_UserID = Convert.ToInt64(Session["UserID"].ToString()),
        //            QueuedOn = DateTime.Now,
        //            PipelineSystemID = Convert.ToInt32(valvesection.PipeSystemID),
        //            PipelineID = valvesection.PipelineID,
        //            ValveSectionID = currSection,
        //            IsLatestCopy = true,
        //            ProcessingMessage = "Creating - Added from resegment Now button."
        //        };
        //        db.DynamicSegmentations.Add(ds);
        //        db.SaveChanges();

        //        // Call stored procedure to do the import
        //        SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["PipelineFeatureListDBContext"].ConnectionString);
        //        conn.Open();
        //        SqlCommand cmd = new SqlCommand("DynamicallySegment", conn);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.Add(new SqlParameter("@ValveSectionID", currSection));
        //        cmd.ExecuteNonQuery();
        //        conn.Close();
        //        conn.Dispose();
        //    }
        //}
    }
}
