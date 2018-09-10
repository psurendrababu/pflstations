using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PipelineFeatureList.Models
{
    public class FeatureError
    {
        [DisplayName("ID")]
        public Int64 FeatureErrorID { get; set; }
        [DisplayName("Feature Error")]
        public string FeatureErrorItem { get; set; }
        [DisplayName("Error Text")]
        public string ErrorText { get; set; }
        [DisplayName("Order By")]
        public int OrderBy { get; set; }
    }
}