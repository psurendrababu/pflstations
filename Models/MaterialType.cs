using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PipelineFeatureList.Models
{
    public class MaterialType
    {
        [DisplayName("ID")]
        public int MaterialTypeID { get; set; }
        [DisplayName("Material Type")]
        public string MaterialTypeItem { get; set; }
    }
}