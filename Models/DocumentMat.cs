using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PipelineFeatureList.Models
{
    public class DocumentMat
    {
        [DisplayName("ID")]
        public int DocumentMatID { get; set; }
        [DisplayName("Document Type 1 ID")]
        public int DocumentTypeID1 { get; set; }
        [DisplayName("Document Type 2 ID")]
        public int DocumentTypeID2 { get; set; }
        [DisplayName("Combo Type")]
        public string ComboType { get; set; }
    }
}