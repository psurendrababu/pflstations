using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PipelineFeatureList.Models
{
    public class Grade
    {
        [DisplayName("ID")]
        public int GradeID { get; set; }
        [DisplayName("Grade")]
        public string GradeItem { get; set; }
    }
}