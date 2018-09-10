using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PipelineFeatureList.Models
{
    public class ConstructionType
    {
        [DisplayName("ID")]
        public int ConstructionTypeID { get; set; }
        [DisplayName("Construction Type")]
        public string ConstructionTypeItem { get; set; }
    }
}