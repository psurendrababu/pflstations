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
    public class NotificationEmail
    {
        [DisplayName("Notification Email ID")]
        public Int64 NotificationEmailID { get; set; }
        [DisplayName("Report")]
        public string Report { get; set; }
        [DisplayName("Emails")]
        public string Emails { get; set; }
        [DisplayName("Flag")]
        public int? Flag { get; set; }
        [DisplayName("Description")]
        public string Description { get; set; }
    }
}