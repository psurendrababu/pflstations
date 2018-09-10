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
        [DisplayName("Valve Section")]
        public Int64 ValveSectionID { get; set; }
        [DisplayName("Records Identifier")]
        public int RecordIdentifierID { get; set; }
        [DisplayName("Document Type")]
        public int DocumentTypeID { get; set; }
        [DisplayName("File Name")]
        public string Filename { get; set; }
        [DisplayName("Record ID Name")]
        public string RecordIDName { get; set; }
        [DisplayName("Electronic Unique ID")]
        public string ElectronicUniqueID { get; set; }
        [DisplayName("Page Number")]
        public string Page { get; set; }
        
        [DisplayName("Valve Section")]
        public ValveSection ValveSection { get; set; }
        [DisplayName("Document Type")]
        public DocumentType DocumentType { get; set; }
        [DisplayName("Record Identifier")]
        public RecordIdentifier RecordIdentifier { get; set; }
    }
}