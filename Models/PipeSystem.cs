using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PipelineFeatureList.Models
{
    public class PipeSystem
    {
        [DisplayName("ID")]
        public int PipeSystemID { get; set; }
        [DisplayName("Station System")]
        public string PipeSystemItem { get; set; }
    }
}