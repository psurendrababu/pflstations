using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PipelineFeatureList.Models
{
    public class GroupClassification
    {
        [DisplayName("ID")]
        public Int64 GroupClassificationID { get; set; }
        [DisplayName("Group Classification")]
        public string GroupClassificationItem { get; set; }
    }
}