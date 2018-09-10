using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PipelineFeatureList.Models
{
    public class ReportingGroup
    {
        [DisplayName("ID")]
        public int ReportingGroupID { get; set; }
        [DisplayName("Reporting Group")]
        public string ReportingGroupName { get; set; }
    }
}