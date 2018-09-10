using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PipelineFeatureList.Models
{
    public class BendRadius
    {
        [DisplayName("ID")]
        public int BendRadiusID { get; set; }
        [DisplayName("Bend Raduis")]
        public string BendRadiusItem { get; set; }
    }
}