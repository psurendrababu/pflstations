using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PipelineFeatureList.Models
{
    public class ValveSectionErrorLevel
    {
        [DisplayName("ID")]
        public int ValveSectionErrorLevelID { get; set; }
        [DisplayName("Valve Section Error Level")]
        public string ValveSectionErrorLevelItem { get; set; }
        [DisplayName("Back Color")]
        public string BackColor { get; set; }
        [DisplayName("Fore Color")]
        public string ForeColor { get; set; }
    }
}
