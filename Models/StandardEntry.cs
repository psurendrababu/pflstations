using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PipelineFeatureList.Models
{
    public class StandardEntry
    {
        [DisplayName("ID")]
        public int StandardEntryID { get; set; }
        [DisplayName("Standard Entry")]
        public string StandardEntryItem { get; set; }
    }
}