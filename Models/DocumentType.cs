using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PipelineFeatureList.Models
{
    public class DocumentType
    {
        [DisplayName("ID")]
        public int DocumentTypeID { get; set; }
        [DisplayName("Document Type")]
        public string DocumentTypeItem { get; set; }
        [DisplayName("Pre Construction?")]
        public bool PreConstruction { get; set; }
        [DisplayName("Station System")]
        public int PipeSystemID { get; set; }
        [DisplayName("Station")]
        public int PipeLineID { get; set; }
        [DisplayName("Page Required?")]
        public bool PageRequired { get; set; }

        [DisplayName("Station System")]
        public PipeSystem PipeSystem { get; set; }
        [DisplayName("Station")]
        public Pipeline Pipeline { get; set; }
    }
}