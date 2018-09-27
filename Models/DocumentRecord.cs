using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PipelineFeatureList.Models
{
    public class DocumentRecord
    {
        [DisplayName("ID")]
        public Int64 DocumentRecordID { get; set; }
        [DisplayName("Station")]
        public Int64 PipelineID { get; set; }
        [DisplayName("Document Type")]
        public int DocumentTypeID { get; set; }
        [DisplayName("File Name")]
        public string Filename { get; set; }
        [DisplayName("Records ID")]
        public string RecordIDName { get; set; }
        [DisplayName("Drawing Number")]
        public string DrawingNumber { get; set; }
        [DisplayName("Page Number")]
        public string Page { get; set; }
        [DisplayName("Revision Number")]
        public string RevisionNumber { get; set; }
        
        //[DisplayName("Station")]
        //public Pipeline Pipeline { get; set; }
        //[DisplayName("Document Type")]
        //public DocumentType DocumentType { get; set; }
        //[DisplayName("Record Identifier")]
        //public RecordIdentifier RecordIdentifier { get; set; }
    }
}