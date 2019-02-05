using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PipelineFeatureList.Models
{
    public class ANSIRating
    {
        [DisplayName("ID")]
        public int ANSIRatingID { get; set; }
        [DisplayName("ANSI Rating")]
        public string ANSIRatingItem { get; set; }
        [DisplayName("Pressure Rating")]
        public string PressureRating { get; set; }

    }
}