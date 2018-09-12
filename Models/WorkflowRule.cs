using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PipelineFeatureList.Models
{
    public class WorkflowRule
    {
        [DisplayName("ID")]
        public Int64 WorkflowRuleID { get; set; }
        [DisplayName("Created By")]
        public Int64 CreatedBy_UserID { get; set; }
        [DisplayName("Created Date/Time")]
        public DateTime CreatedOn { get; set; }
        [DisplayName("Modified By")]
        public Int64 ModifiedBy_UserID { get; set; }
        [DisplayName("Modified Date/Time")]
        public DateTime ModifiedOn { get; set; }
        [DisplayName("Old Circuit Status ID")]
        public int Old_ValveSectionStatusID { get; set; }
        [DisplayName("Workflow Action ID")]
        public int WorkflowActionID { get; set; }
        [DisplayName("New Circuit Status ID")]
        public int New_ValveSectionStatusID { get; set; }
        [DisplayName("New Circtui Owner User ID")]
        public Int64 New_ValveSectionOwner_UserID { get; set; }
        [DisplayName("New Circuit Owner Workflow Variable ID")]
        public Int64 New_ValveSectionOwner_WorkflowVariableID { get; set; }
        [DisplayName("Description")]
        public string Description { get; set; }
        [DisplayName("Button Text")]
        public string ButtonText { get; set; }

        public WorkflowAction WorkflowAction { get; set; }
        //public bool DisableDirty { get; set; }
    }

    public class WorkflowRuleList
    {
        public WorkflowRule WorkflowRule { get; set; }
        public WorkflowAction WorkflowAction { get; set; }
        public bool DisableDirty { get; set; }
    }
}