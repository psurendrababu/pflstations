using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PipelineFeatureList.Models
{
    public class WorkflowHistory
    {
        [DisplayName("ID")]
        public Int64 WorkflowHistoryID { get; set; }
        [DisplayName("Circuit ID")]
        public Int64 ValveSectionID { get; set; }
        [DisplayName("Changed By")]
        public Int64 ChangedBy_UserID { get; set; }
        [DisplayName("Changed Date/Time")]
        public DateTime ChangedOn { get; set; }
        [DisplayName("Old Workflow Status ID")]
        public int Old_WorkflowStatusID { get; set; }
        [DisplayName("Workflow Action ID")]
        public int WorkflowActionID { get; set; }
        [DisplayName("New Workflow Status ID")]
        public int New_WorkflowStatusID { get; set; }
    }
}