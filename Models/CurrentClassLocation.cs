using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PipelineFeatureList.Models
{
    public class CurrentClassLocation
    {
        [DisplayName("ID")]
        public int CurrentClassLocationID { get; set; }
        [DisplayName("Current Class Location")]
        public string CurrentClassLocationItem { get; set; }
    }
}