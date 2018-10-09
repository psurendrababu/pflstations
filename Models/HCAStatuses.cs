using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PipelineFeatureList.Models
{
    public class HCAStatus
    {
        [DisplayName("HCA Status ID")]
        public int HCAStatusID { get; set; }
        [DisplayName("HCA Status Name")]
        public string HCAStatusName { get; set; }
    }
}