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
    public class ReportEmail
    {
        [Key]
        [DisplayName("Report Email ID")]
        public Int64 ReportEmailsID { get; set; }
        [DisplayName("Report ID")]
        public Int64 ReportID { get; set; }
        [DisplayName("User ID")]
        public Int64 UserID { get; set; }

        [DisplayName("Report")]
        public Report Report { get; set; }
        [DisplayName("User")]
        public User User { get; set; }
    }
}