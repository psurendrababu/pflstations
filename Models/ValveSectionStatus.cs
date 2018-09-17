using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PipelineFeatureList.Models
{
    public class ValveSectionStatus
    {
        [DisplayName("ID")]
        public int ValveSectionStatusID { get; set; }
        [DisplayName("Circuit Status")]
        public string ValveSectionStatusItem { get; set; }
        [DisplayName("Created By")]
        public Int64 CreatedBy_UserID { get; set; }
        [DisplayName("Created Date/Time")]
        public DateTime CreatedOn { get; set; }
        [DisplayName("Modified By")]
        public Int64 ModifiedBy_UserID { get; set; }
        [DisplayName("Modified Date/Time")]
        public DateTime ModifiedOn { get; set; }
        [DisplayName("Copy Data To History")]
        public bool CopyDataToHistory { get; set; }
        [DisplayName("Generate Grades")]
        public bool GenerateGrades { get; set; }
        [DisplayName("Dynamic Segmentation")]
        public bool DynamicSegmentation { get; set; }
        [DisplayName("Queue Certification")]
        public bool QueueCertification { get; set; }
        [DisplayName("Reporting Group")]
        public int ReportingGroupID { get; set; }
        [DisplayName("Display Group")]
        public int DisplayGroupID { get; set; }
        [DisplayName("Assign Builder")]
        public bool AssignBuilder { get; set; }
        [DisplayName("Assign QC")]
        public bool AssignQC { get; set; }
        [DisplayName("Assign Engineer")]
        public bool AssignEngineer { get; set; }
        [DisplayName("Assign Final Engineer")]
        public bool AssignFinalEngineer { get; set; }
        [DisplayName("Assign Annual Reviewer")]
        public bool AssignAnnualReviewer { get; set; }
        [DisplayName("Redirect Action")]
        public string RedirectAction { get; set; }
        [DisplayName("Disable Edit")]
        public bool DisableEdit { get; set; }
        [DisplayName("Dirty So Disable ")]
        public bool DirtyDisable { get; set; }

        [DisplayName("Display Group")]
        public DisplayGroup DisplayGroup { get; set; }
        
    }
}