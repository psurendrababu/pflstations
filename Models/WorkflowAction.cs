using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PipelineFeatureList.Models
{
    public class WorkflowAction
    {
        [DisplayName("ID")]
        public int WorkflowActionID { get; set; }
        [DisplayName("Created By")]
        public Int64 CreatedBy_UserID { get; set; }
        [DisplayName("Created Date/Time")]
        public DateTime CreatedOn { get; set; }
        [DisplayName("Modified By")]
        public Int64 ModifiedBy_UserID { get; set; }
        [DisplayName("Modified Date/Time")]
        public DateTime ModifiedOn { get; set; }
        [DisplayName("Workflow Action Item")]
        public string WorkflowActionItem { get; set; }
    }
}