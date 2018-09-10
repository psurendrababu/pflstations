using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PipelineFeatureList.Models
{
    public class DynamicSegmentationRecord
    {
        [DisplayName("Dynamic Segmentation Record ID")]
        public Int64 DynamicSegmentationRecordID { get; set; }
        [DisplayName("Dynamic Segmentation ID")]
        public Int64 DynamicSegmentationID { get; set; }
        [DisplayName("HCA Upload Identifier")]
        public string HCA_UploadIdentifier { get; set; }
        public Nullable<int> UploadHighConsequenceAreaDataID { get; set; }
        public string Class_UploadIdentifier { get; set; }
        public Nullable<int> UploadClassDataID { get; set; }
        public string Crossing_UploadIdentifier { get; set; }
        public Nullable<Int64> UploadCrossingDataID { get; set; }
        [DisplayName("Pipe System")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Pipe System Required")]
        public int? PipeSystemID { get; set; }
        [DisplayName("Pipeline")]
        [Required(ErrorMessage = "Pipe Line Required")]
        public int PipelineID { get; set; }
        [DisplayName("Valve Section ID")]
        [Required]
        public Int64 ValveSectionID { get; set; }
        [DisplayName("Valve Section Feature ID")]
        public Int64 ValveSectionFeatureID { get; set; }
        public string PipeSystem { get; set; }
        public string Pipeline { get; set; }
        [DisplayName("Orion Station Series")]
        [Required]
        public string OrionStationSeries { get; set; }
        [DisplayName("Orion Station Begin")]
        //[InputMask("99999+99.99")]
        [DisplayFormat(DataFormatString = "{0:0+00.00}", ApplyFormatInEditMode = false)]
        public Nullable<decimal> OrionStationBegin { get; set; }
        [DisplayName("Orion Station End")]
        //[InputMask("999-999-9999")]
        [DisplayFormat(DataFormatString = "{0:0+00.00}", ApplyFormatInEditMode = false)]
        public Nullable<decimal> OrionStationEnd { get; set; }
        [DisplayName("Valve Section Begin")]
        [Required]
        public string ValveSection_BeginStation { get; set; }
        [DisplayName("Valve Section End")]
        public string ValveSection_EndStation { get; set; }
        [DisplayName("Mile Point Begin")]
        public Nullable<decimal> ValveSection_MilePointBegin { get; set; }
        [DisplayName("Mile Point End")]
        public Nullable<decimal> ValveSection_MilePointEnd { get; set; }
        [DisplayName("GIS Start")]
        [DisplayFormat(DataFormatString = "{0:0+00.00}", ApplyFormatInEditMode = false)]
        public Nullable<decimal> ValveSection_GISStationBegin { get; set; }
        [DisplayName("GIS End")]
        [DisplayFormat(DataFormatString = "{0:0+00.00}", ApplyFormatInEditMode = false)]
        public Nullable<decimal> ValveSection_GISStationEnd { get; set; }
        [DisplayName("Current MAOP")]
        public Nullable<int> ValveSection_CurrentMAOP { get; set; }
        [DisplayName("PFL Length")]
        [DisplayFormat(DataFormatString = "{0:0.0}", ApplyFormatInEditMode = false)]
        public Nullable<decimal> ValveSection_PFLLength { get; set; }
        [DisplayName("Feature Number")]
        public decimal FeatureNumber { get; set; }
        [DisplayName("Segment Number")]
        public int SegmentNumber { get; set; }
        public decimal Segment_Begin_Ft { get; set; }
        public decimal Segment_End_Ft { get; set; }
        [DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = false)]
        public decimal Segment_Length { get; set; }
        public string Segment_HCA { get; set; }
        public Nullable<int> Segment_Class { get; set; }
        [DisplayName("Crossing Center Ft")]
        [DisplayFormat(DataFormatString = "{0:0+00}", ApplyFormatInEditMode = false)]
        public Nullable<int> Crossing_Center_Ft { get; set; }
        [DisplayName("Crossing Orion Center Ft")]
        [DisplayFormat(DataFormatString = "{0:0+00}", ApplyFormatInEditMode = false)]
        public Nullable<decimal> Crossing_Orion_Center_Ft { get; set; }
        public string Crossing_NamesAndNotes { get; set; }
        public string Crossing_Type { get; set; }
        public string Crossing_IssuesAndErrors { get; set; }
        [DisplayName("Feature")]
        public string Feature { get; set; }
        [DisplayName("Feature Type")]
        public string FeatureType { get; set; }
        [DisplayName("Job Number/Work Order/Purchase Order #")]
        public string Feature_JobWorkOrderPurchaseOrder { get; set; }
        [DisplayName("Unknown")]
        public bool Feature_JobWorkOrderPurchaseOrder_Unknown { get; set; }
        [DisplayName("Install Date mm/dd/yyyy")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<DateTime> Feature_InstallDate { get; set; }
        [DisplayName("Unknown")]
        public bool Feature_InstallDate_Unknown { get; set; }
        [DisplayName("In-Service Date mm/dd/yyyy")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<DateTime> Feature_InServiceDate { get; set; }
        [DisplayName("Unknown")]
        public bool Feature_InServiceDate_Unknown { get; set; }
        [DisplayName("Construction Type")]
        public string Feature_ConstructionType { get; set; }
        [DisplayName("Notes / Comments")]
        public string Feature_Notes { get; set; }
        [DisplayName("Drawing Identifier")]
        public Nullable<int> Feature_Drawing_Identifier { get; set; }
        public string Feature_Drawing_Description { get; set; }
        [DisplayName("O.D. 1 (inch)")]
        public Nullable<decimal> Feature_OuterDiameter1 { get; set; }
        [DisplayName("O.D. 2 (inch)")]
        public Nullable<decimal> Feature_OuterDiameter2 { get; set; }
        [DisplayName("OD Records Identifier 1")]
        public Nullable<int> Feature_OuterDiameterRecord1_Identfier { get; set; }
        public string Feature_OuterDiameterRecord1_Description { get; set; }
        [DisplayName("OD Records Identifier 2")]
        public Nullable<int> Feature_OuterDiameterRecord2_Identfier { get; set; }
        public string Feature_OuterDiameterRecord2_Description { get; set; }
        [DisplayName("OD Record Matrix Check")]
        public Nullable<int> Feature_ODRecordMatrixCheck { get; set; }
        [DisplayName("W.T. 1 (inch)")]
        [Range(0, 1, ErrorMessage = "Value must be between 0\" and 1\"")]
        [DisplayFormat(DataFormatString = "{0:0.0000}", ApplyFormatInEditMode = true)]
        public Nullable<double> Feature_WallThickness1 { get; set; }
        [DisplayFormat(DataFormatString = "{0:0.0000}", ApplyFormatInEditMode = true)]
        [DisplayName("W.T. 2 (inch)")]
        [Range(0, 1, ErrorMessage = "Value must be between 0\" and 1\"")]
        public Nullable<double> Feature_WallThickness2 { get; set; }
        [DisplayName("WT Records Identifier 1")]
        public Nullable<int> Feature_WallThicknessRecord1_Identifier { get; set; }
        public string Feature_WallThicknessRecord1_Description { get; set; }
        [DisplayName("WT Records Identifier 2")]
        public Nullable<int> Feature_WallThicknessRecord2_Identifier { get; set; }
        public string Feature_WallThicknessRecord2_Description { get; set; }
        [DisplayName("WT Record Matrix Check")]
        public Nullable<int> Feature_WTRecordMatrixCheck { get; set; }
        [DisplayName("Seam Weld Type")]
        public string Feature_SeamType { get; set; }
        [DisplayName("Seam Records Identifier 1")]
        public Nullable<int> Feature_SeamRecord1_Identifier { get; set; }
        public string Feature_SeamRecord1_Description { get; set; }
        [DisplayName("Seam Records Identifier 2")]
        public Nullable<int> Feature_SeamRecord2_Identifier { get; set; }
        public string Feature_SeamRecord2_Description { get; set; }
        [DisplayName("Seam Record Matrix Check")]
        public Nullable<int> Feature_SeamRecordMatrixCheck { get; set; }
        [DisplayName("Specification/Rating")]
        public string Feature_SpecRating { get; set; }
        [DisplayName("Grade")]
        public string Feature_Grade { get; set; }
        [DisplayName("Spec/Grade Records Identifier 1")]
        public Nullable<int> SpecRatingRecord1_Identifier { get; set; }
        public string SpecRatingRecord1_Description { get; set; }
        [DisplayName("Spec/Grade Records Identifier 2")]
        public Nullable<int> SpecRatingRecord2_Identifier { get; set; }
        public string SpecRatingRecord2_Description { get; set; }
        [DisplayName("Spec/Grade Record Matrix Check")]
        public Nullable<int> Feature_SpecRatingRecordMatrixCheck { get; set; }
        public Nullable<int> Feature_MatrixPass { get; set; }
        [DisplayName("ANSI Rating")]
        public string Feature_ANSIRating { get; set; }
        [DisplayName("Material Type")]
        public string Feature_MaterialType { get; set; }
        [DisplayName("Angle")]
        public Nullable<int> Angle { get; set; }
        [DisplayName("Radius")]
        public string Feature_BendRadius { get; set; }
        [DisplayName("Orient")]
        public string Feature_Orientation { get; set; }
        [DisplayName("Coating Type")]
        public string Feature_CoatingType { get; set; }
        [DisplayName("Description")]
        public string Feature_CoatingDescription { get; set; }
        [DisplayName("Unknown")]
        public bool Feature_CoatingDescription_Unknown { get; set; }
        [DisplayName("Manufacturer")]
        public string Feature_Manufacturer { get; set; }
        [DisplayName("Manufacturer Type")]
        public string Feature_ManufacturerType { get; set; }
        [DisplayName("Mill")]
        public string Feature_Mill { get; set; }
        [DisplayName("Unknown")]
        public bool Feature_Mill_Unknown { get; set; }
        [DisplayName("MFR Date mm/dd/yyyy")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<DateTime> Feature_ManufacturerDate { get; set; }
        [DisplayName("Unknown")]
        public bool Feature_ManufacturerDate_Unknown { get; set; }
        [DisplayName("Current Class Loc.")]
        public string Current_Class_Location { get; set; }
        [DisplayName("SMYS [S] (psi)")]
        public Nullable<int> Segment_SMYS { get; set; }
        [DisplayName("Class Design Factor")]
        public Nullable<decimal> Segment_DesignFactor { get; set; }
        [DisplayName(" Joint Factor [E]")]
        public Nullable<decimal> Segment_JointFactor { get; set; }
        [DisplayName("Temperature Derating [T]")]
        public Nullable<int> Segment_TemperatureDerating { get; set; }
        [DisplayName("Design Pressure [P] (psig)")]
        public Nullable<decimal> Segment_DesignPressure { get; set; }
    }
}