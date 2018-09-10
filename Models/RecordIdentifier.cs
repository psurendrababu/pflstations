using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PipelineFeatureList.Models
{
    public class RecordIdentifier
    {
        [DisplayName("ID")]
        public int RecordIdentifierID { get; set; }
        [DisplayName("Record Identifier")]
        public string RecordIdentifierItem { get; set; }
    }
}