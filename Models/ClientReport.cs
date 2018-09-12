using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using PipelineFeatureList.Models;
using System.Web.Mvc;

namespace PipelineFeatureList.Models
{
    public class ClientReportScreen
    {
        [DisplayName("Client Report")]
        public ClientReport ClientReportData { get; set; }
        [DisplayName("Station Systems")]
        public List<PipeSystem> PipeSystemData { get; set; }
        [DisplayName("Stations")]
        public List<Pipeline> PipelineData { get; set; }
        [DisplayName("Reports")]
        public List<Report> ReportData { get; set; }
        [DisplayName("Import Files")]
        public List<ClientImportList> ClientImportListData { get; set; }
    }

    public class ClientReport
    {
        [DisplayName("Client Report ID")]
        [Required]
        public Int64 ClientReportID { get; set; }
        [DisplayName("Created On")]
        public DateTime CreatedOn { get; set; }
        [DisplayName("Created By")]
        public Int64 CreatedBy_UserID { get; set; }
        [DataType(DataType.Date)]
        [DisplayName("From")]
        public DateTime FromDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayName("To")]
        public DateTime ToDate { get; set; }
        [DisplayName("Import File")]
        public String ImportFile { get; set; }
        [DisplayName("Permanent?")]
        public bool PermanentImport { get; set; }
        [DisplayName("Processed")]
        public bool Processed { get; set; }
    }
    public class ClientReportReportList
    {
        [DisplayName("Client Report Report List ID")]
        [Required]
        public Int64 ClientReportReportListID { get; set; }
        [DisplayName("Client Report ID")]
        public Int64 ClientReportID { get; set; }
        [DisplayName("Report ID")]
        public Int64 ReportID { get; set; }
    }
    public class ClientReportPipeSystem
    {
        [DisplayName("Client Report Pipe System ID")]
        [Required]
        public Int64 ClientReportPipeSystemID { get; set; }
        [DisplayName("Client Report ID")]
        public Int64 ClientReportID { get; set; }
        [DisplayName("Pipe System ID")]
        public Int64 PipeSystemID { get; set; }
    }
    public class ClientReportPipeline
    {
        [DisplayName("Client Report Pipeline ID")]
        [Required]
        public Int64 ClientReportPipelineID { get; set; }
        [DisplayName("Client Report ID")]
        public Int64 ClientReportID { get; set; }
        [DisplayName("Pipeline ID")]
        public Int64 PipelineID { get; set; }
    }
    public class ClientReportImportList
    {
        [DisplayName("Client Report Import List ID")]
        [Required]
        public Int64 ClientReportImportListID { get; set; }
        [DisplayName("Client Report ID")]
        public Int64 ClientReportID { get; set; }
        [DisplayName("Client Import List ID")]
        public Int64 ClientImportListID { get; set; }
    }
}