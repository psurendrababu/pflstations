using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PipelineFeatureList.Models
{
    public class UserType
    {
        [DisplayName("ID")]
        public Int64 UserTypeID { get; set; }
        [DisplayName("User Type")]
        public string UserTypeItem { get; set; }
    }
}