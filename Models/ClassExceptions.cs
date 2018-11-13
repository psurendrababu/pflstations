using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PipelineFeatureList.Models
{
    public class ClassExceptions
    {
        [DisplayName("Class Exception ID")]
        public int ClassExceptionsID { get; set; }
        [DisplayName("Class Exception Name")]
        public string ClassExceptionsName { get; set; }
    }
}