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
    public class Report
    {
        [DisplayName("Report ID")]
        public Int64 ReportID { get; set; }
        [DisplayName("Report Name")]
        public string ReportName { get; set; }
        [DisplayName("Description")]
        public string Description { get; set; }
    }
}