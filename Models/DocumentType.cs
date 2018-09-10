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
        [DisplayName("Pipe System")]
        public int PipeSystemID { get; set; }
        [DisplayName("Pipeline")]
        public int PipeLineID { get; set; }
        [DisplayName("Page Required?")]
        public bool PageRequired { get; set; }

        [DisplayName("Pipe System")]
        public PipeSystem PipeSystem { get; set; }
        [DisplayName("Pipeline")]
        public Pipeline Pipeline { get; set; }
    }
}