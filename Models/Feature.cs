using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PipelineFeatureList.Models
{
    public class Feature
    {
        [DisplayName("ID")]
        public int FeatureID { get; set; }
        [DisplayName("Feature")]
        public string FeatureItem { get; set; }
        [DisplayName("Length Lookup")]
        public bool LengthLookup { get; set; }
    }
}