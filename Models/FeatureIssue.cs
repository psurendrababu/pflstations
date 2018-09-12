using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PipelineFeatureList.Models
{
    public class FeatureIssue
    {
        [DisplayName("Feature Issue ID")]
        public Int64 FeatureIssueID { get; set; }
        [DisplayName("Circuit ID")]
        public Int64 ValveSectionID { get; set; }
        [Required]
        [DisplayName("Circuit Feature ID")]
        public Int64 ValveSectionFeatureID { get; set; }
        [DisplayName("Feature Number")]
        public string FeatureNumber { get; set; }
        [DisplayName("Builder ID")]
        public Nullable<Int64> BuilderID { get; set; }
        [DisplayName("Created by Builder")]
        public Nullable<DateTime> BuilderCreatedOn { get; set; }
        [DisplayName("Description of Issue: Builder")]
        public String BuilderDescription { get; set; }        
        [DisplayName("Checker ID")]
        public Nullable<Int64> CheckerID { get; set; }
        [DisplayName("Created by Checker")]
        public Nullable<DateTime> CheckerCreatedOn { get; set; }
        [DisplayName("Description of Issue: QC")]
        public String CheckerDescription { get; set; }        
        [DisplayName("Engineer ID")]
        public Nullable<Int64> EngineerID { get; set; }
        [DisplayName("Created by Engineer")]
        public Nullable<DateTime> EngineerCreatedOn { get; set; }
        [DisplayName("Description of Issue: Engineer")]
        public String EngineerDescription { get; set; }        
    }
}