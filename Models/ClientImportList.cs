using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using PipelineFeatureList.Models;

namespace PipelineFeatureList.Models
{
    public class ClientImportList
    {
        [DisplayName("Client Import List ID")]
        public Int64 ClientImportListID { get; set; }
        [DisplayName("File Name")]
        public String FileName { get; set; }
    }
}