using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PipelineFeatureList.Models
{
    public class OutsideDiameter
    {
        [DisplayName("ID")]
        public int OutsideDiameterID { get; set; }
        [DisplayName("Outside Diameter")]
        [DisplayFormat(DataFormatString = "{0:0.000}", ApplyFormatInEditMode = false)]
        public decimal OutsideDiameterItem { get; set; }

        [DisplayName("Nominal Diameter")]
        [DisplayFormat(DataFormatString = "{0:0.000}", ApplyFormatInEditMode = false)]
        public decimal NominalDiameterItem { get; set; }     
     

    }
}