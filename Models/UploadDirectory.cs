using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace PipelineFeatureList.Models
{
    public class UploadDirectory
    {
        [DisplayName("ID")]
        public int UploadDirectoryID { get; set; }
        [DisplayName("Upload Directory")]
        public string Upload_Directory { get; set; }
        [DisplayName("Local Directory")]
        public string Local_Directory { get; set; }
        [DisplayName("Database Name")]
        public string DatabaseName { get; set; }
    }
}