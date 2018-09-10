using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PipelineFeatureList.Models
{
    public class ManufacturerType
    {
        [DisplayName("Manufacturer Type ID")]
        public int ManufacturerTypeID { get; set; }
        [DisplayName("Manufacturer Type")]
        public string ManufacturerTypeItem { get; set; }
    }
}