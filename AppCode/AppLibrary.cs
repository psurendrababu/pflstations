using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PipelineFeatureList.Models;
using System.Data.SqlClient;
using System.Data;
//PH 2014.05.22 Added to use EF for DynamicSegmentation insert (keep EF in sync).
using System.Data.Entity;
//PH 2014.05.22 end edit

namespace PipelineFeatureList.AppCode
{
    public partial class AppLibrary
    {
        //PH 2014.05.22 need user id here, and session is out of scope.
        //public static void CopyToHistoryGenerateGradesDynamicSegmentation(Int64 valvesectionid, int copytohistory, int generategrades, int dynamicsegmentation)
        //PH 2014.05.22 end edit
        public static void CopyToHistoryGenerateGradesDynamicSegmentation(Int64 userid, Int64 valvesectionid, int copytohistory, int generategrades, int dynamicsegmentation)
        {
            var sqlDB = new System.Data.SqlClient.SqlConnectionStringBuilder(
            System.Configuration.ConfigurationManager.ConnectionStrings["PipelineFeatureListDBContext"].ConnectionString);
            var dbName = sqlDB.InitialCatalog;

            //PH 2014.05.22: Segmentation should always create a new header.  Right now it only does so when changing statuses.  
            if (dynamicsegmentation != 0)
            {
                //Code below ripped from GenerateReportController.DynamicSegmentation() and edited.
                PipelineFeatureListDBContext db = new PipelineFeatureListDBContext();
                DynamicSegmentation ds = new DynamicSegmentation
                {
                    QueuedOn = DateTime.Now,
                    StartedOn = null,
                    CompletedOn = null,
                    ValveSectionID = valvesectionid,
                    Engineer_UserID = userid,
                    IsLatestCopy = true,
                    ProcessingMessage = "Queued for Segmentation"
                };
                db.DynamicSegmentations.Add(ds);
                db.SaveChanges();
                db.Dispose();
            }
            //PH 2014.05.22 end edit

            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["PipelineFeatureListDBContext"].ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("Copy_Grades_Segment", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@ValveSectionID", valvesectionid));
            cmd.Parameters.Add(new SqlParameter("@CopyToHistory", copytohistory));
            cmd.Parameters.Add(new SqlParameter("@GenerateGrades", generategrades));
            cmd.Parameters.Add(new SqlParameter("@DynamicallySegment", dynamicsegmentation));
            //cmd.ExecuteNonQuery();
            //cmd.Dispose();
            //conn.Close();
            //conn.Dispose();

            try
            {
                cmd.BeginExecuteNonQuery(delegate(IAsyncResult ar)
                {
                    int rowCount = cmd.EndExecuteNonQuery(ar);
                    //nothing to do with rowCount at this point.
                }, cmd);
            }
            catch (SqlException s)
            {
                throw s;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
