using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PipelineFeatureList.Models
{
    public class DisplayGroup
    {
        [DisplayName("ID")]
        public int DisplayGroupID { get; set; }
        [DisplayName("Display Group")]
        public string DisplayGroupName { get; set; }
    }
}