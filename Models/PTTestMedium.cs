using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PipelineFeatureList.Models
{
    public class PTTestMedium
    {
        [DisplayName("PT Test Medium ID")]
        public int PTTestMediumID { get; set; }
        [DisplayName("PT Test Medium Item")]
        public string PTMedium { get; set; }
    }
}



