using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PipelineFeatureList.Models
{
    public class PressureTestRecord
    {
        [DisplayName("ID")]
        public Int64 PressureTestRecordID { get; set; }
        [DisplayName("Station ID")]
        public Int64 PipelineID { get; set; }
        //[DisplayName("Feature ID")]
        //public Int64 FeatureID { get; set; }
        [DisplayName("File Name")]
        public string Filename { get; set; }

        [DisplayName("PT Min Test Pressure")]
        public int PTMinTestPressure { get; set; }
        [DisplayName("PT Medium")]
        public string PTMedium { get; set; }   
        //public int PTTestMediumID { get; set; }
        //public  Nullable<int> PTTestMediumID  { get; set; }

        [DisplayName("PT Date (mm/dd/yyyy)")]
        public DateTime PTDate { get; set; }
        [DisplayName("PT Duration (Hrs)")]
        public int PTDuration { get; set; }
        [DisplayName("Inst. Dead Wt. Elevation")]
        public int InstDeadWtEval { get; set; }
        [DisplayName("Min Elevation")]
        public int MinElevation { get; set; }
        [DisplayName("Max Elevation")]
        public int MaxElevation { get; set; }
        [DisplayName("PT Hydrotest Name & Location")]
        public string PTNameLocation { get; set; }
        [DisplayName("Pressure Recording Charts")]
        public bool PTRecordingCharts { get; set; }
        [DisplayName("Test Inst. Calibration")]
        public bool PTInstCalibrated { get; set; }
        [DisplayName("Operator Name")]
        public string PTOperatorName { get; set; }
        [DisplayName("PT Person Responsible")]
        public string PTPersonResponsible { get; set; }
        [DisplayName("PT Test Company")]
        public string PTTestCompany { get; set; }
        [DisplayName("Record Identifier")]
        public string PTRIDName { get; set; }





    }
}