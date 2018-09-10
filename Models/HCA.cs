using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PipelineFeatureList.Models
{
    public class HCA
    {
        [DisplayName("ID")]
        public int HCAID { get; set; }
        [DisplayName("HCA")]
        public string HCAItem { get; set; }
    }
}