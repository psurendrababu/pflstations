using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PipelineFeatureList.Models
{
    public class PipeType
    {
        [DisplayName("ID")]
        public int PipeTypeID { get; set; }
        [DisplayName("Pipe Type")]
        public string PipeTypeItem { get; set; } 
        [DisplayName("Feature")]
        public int FeatureID { get; set; }

        [DisplayName("Features")]
        public Feature Feature { get; set; }
    }
}