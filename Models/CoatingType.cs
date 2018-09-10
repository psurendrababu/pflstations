using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PipelineFeatureList.Models
{
    public class CoatingType
    {
        [DisplayName("ID")]
        public int CoatingTypeID { get; set; }
        [DisplayName("Coating Type")]
        public string CoatingTypeItem { get; set; }
    }
}