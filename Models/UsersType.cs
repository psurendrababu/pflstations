using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PipelineFeatureList.Models
{
    public class UsersType
    {
        [DisplayName("ID")]
        public Int64 UsersTypeID { get; set; }
        [DisplayName("User")]
        public Int64 UserID { get; set; }
        [DisplayName("User Type")]
        public Int64 GroupClassificationID { get; set; }
    }
}