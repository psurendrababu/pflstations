using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using PipelineFeatureList.Models;
using System.Web.Mvc;

namespace PipelineFeatureList.Models
{
    public class CertificationApproved
    {
        [DisplayName("Certification Approved ID")]
        [Required]
        public Int64 CertificationApprovedID { get; set; }
        [DisplayName("Circuit ID")]
        [Required]
        public Int64 ValveSectionID { get; set; }
        [DisplayName("Certification Date/Time")]
        public DateTime CertificationDate { get; set; }
        [DisplayName("Status")]
        public int Status { get; set; }
    }
}