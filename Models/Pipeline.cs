using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PipelineFeatureList.Models
{
    public class Pipeline
    {
        [DisplayName("ID")]
        public int PipelineID { get; set; }
        [DisplayName("Station")]
        public string PipelineItem { get; set; }
        [DisplayName("Station System")]
        public int PipeSystemID { get; set; }
        [DisplayName("Created By")]
        public Int64 CreatedBy_UserID { get; set; }
        [DisplayName("Created Date/Time")]
        public DateTime CreatedOn { get; set; }
        [DisplayName("Modified By")]
        public Int64 ModifiedBy_UserID { get; set; }
        [DisplayName("Modified Date/Time")]
        public DateTime ModifiedOn { get; set; }

        [DisplayName("Station System")]
        public PipeSystem PipeSystem { get; set; }

    }
}