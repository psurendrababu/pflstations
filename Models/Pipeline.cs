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
        [DisplayName("Station ID")]
        public string PipelineItem { get; set; }
        [DisplayName("Station Location")]
        public int PipeSystemID { get; set; }
        [DisplayName("Created By")]
        public Int64 CreatedBy_UserID { get; set; }
        [DisplayName("Created Date/Time")]
        public DateTime CreatedOn { get; set; }
        [DisplayName("Modified By")]
        public Int64 ModifiedBy_UserID { get; set; }
        [DisplayName("Modified Date/Time")]
        public DateTime ModifiedOn { get; set; }
        [DisplayName("Street")]
        public string Street { get; set; }
        [DisplayName("Cross Street")]
        public string CrossStreet { get; set; }
        [DisplayName("Notes")]
        public string Notes { get; set; }
        [DisplayName("Town")]
        public string Town { get; set; }
        [DisplayName("Circuit Count")]
        public int? CircuitCount { get; set; }
        [DisplayName("Station Name")]
        public string StationName { get; set; }
        
        [DisplayName("Station Location")]
        public PipeSystem PipeSystem { get; set; }

    }
}