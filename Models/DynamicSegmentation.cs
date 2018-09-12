using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PipelineFeatureList.Models
{
    public class DynamicSegmentation
    {
        [DisplayName("ID")]
        public Int64 DynamicSegmentationID { get; set; }
        [DisplayName("Engineer User ID")]
        public Int64 Engineer_UserID { get; set; }
        [DisplayName("Queued On")]
        public DateTime QueuedOn { get; set; }
        [DisplayName("Started On")]
        public Nullable<DateTime> StartedOn { get; set; }
        [DisplayName("Completed On")]
        public Nullable<DateTime> CompletedOn { get; set; }
        [DisplayName("Station System ID")]
        public int PipelineSystemID { get; set; }
        [DisplayName("Station ID")]
        public int PipelineID { get; set; }
        [DisplayName("Circuit ID")]
        public Int64 ValveSectionID { get; set; }
        [DisplayName("Is Latest Copy")]
        public bool IsLatestCopy { get; set; }
        [DisplayName("Processing Message")]
        public string ProcessingMessage { get; set; }
    }
}