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
    public class GenerateReport
    {
        public IEnumerable<SelectListItem> PipeSystemData { get; set; }
        public int PipeSystemDataID { get; set; }

        public IEnumerable<SelectListItem> PipelineData { get; set; }
        public int PipelineDataID { get; set; }

        public IEnumerable<SelectListItem> NotificationEmailData { get; set; }
        public int NotificationEmailID { get; set; }

        public IEnumerable<SelectListItem> ReportOptionData { get; set; }
        public int ReportOptionID { get; set; }

        //public MultiSelectList ANRValveSectionsData { get; set; }
        //public MultiSelectList GLGTValveSectionsData { get; set; }
        //public MultiSelectList GTNValveSectionsData { get; set; }
        //public MultiSelectList NBValveSectionsData { get; set; }
    }
    public class ANRValveSections
    {
        public Int64 ValveSectionID { get; set; }
        public string PipeSystem { get; set; }
        public string Pipeline { get; set; }
        public string ValveSectionBegin { get; set; }
        public string OrionStationSeries { get; set; }
        public int? Status { get; set; }
    }
    public class GLGTValveSections
    {
        public Int64 ValveSectionID { get; set; }
        public string PipeSystem { get; set; }
        public string Pipeline { get; set; }
        public string ValveSectionBegin { get; set; }
        public string OrionStationSeries { get; set; }
        public int? Status { get; set; }
    }
    public class GTNValveSections
    {
        public Int64 ValveSectionID { get; set; }
        public string PipeSystem { get; set; }
        public string Pipeline { get; set; }
        public string ValveSectionBegin { get; set; }
        public string OrionStationSeries { get; set; }
        public int? Status { get; set; }
    }
    public class NBValveSections
    {
        public Int64 ValveSectionID { get; set; }
        public string PipeSystem { get; set; }
        public string Pipeline { get; set; }
        public string ValveSectionBegin { get; set; }
        public string OrionStationSeries { get; set; }
        public int? Status { get; set; }
    }
}