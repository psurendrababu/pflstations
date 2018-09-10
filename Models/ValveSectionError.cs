using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PipelineFeatureList.Models
{
    public class ValveSectionError
    {
        [DisplayName("ID")]
        public Int64 ValveSectionErrorID { get; set; }
        [DisplayName("Valve Section")]
        public Int64 ValveSectionID { get; set; }
        [DisplayName("Valve Section Feature")]
        public Int64 ValveSectionFeatureID { get; set; }
        [DisplayName("Error Level")]
        public int ValveSectionErrorLevelID { get; set; }
        [DisplayName("Error")]
        public string ErrorDescription { get; set; }
        [DisplayName("Feature Error")]
        public Int64 FeatureErrorID { get; set; }

        [DisplayName("Valve Section Feature")]
        public ValveSectionFeature ValveSectionFeature { get; set; }
        [DisplayName("Valve Section Error Level")]
        public ValveSectionErrorLevel ValveSectionErrorLevel { get; set; }
        [DisplayName("Feature Error")]
        public FeatureError FeatureError { get; set; }
    }
}
