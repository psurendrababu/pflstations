using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PipelineFeatureList.Models
{
    public class Orientation
    {
        [DisplayName("ID")]
        public int OrientationID { get; set; }
        [DisplayName("Orientation")]
        public string OrientationItem { get; set; }
    }
}