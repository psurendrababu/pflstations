using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PipelineFeatureList.Models
{
    public class ValveSectionFeature
    {
        [DisplayName("Circuit Feature ID")]
        public Int64 ValveSectionFeatureID { get; set; }
        [DisplayName("Circuit ID")]
        public Int64 ValveSectionID { get; set; }
        //[Required]
        //[DisplayName("GIS Alignment Sheet Begin Station")]
        //[DisplayFormat(DataFormatString = "{0:0+00}", ApplyFormatInEditMode = false)]
        //public decimal GISAlignStart { get; set; }
        //[Required]
        //[DisplayName("GIS Alignment Sheet End Station")]
        //[DisplayFormat(DataFormatString = "{0:0+00}", ApplyFormatInEditMode = false)]
        //public decimal GISAlignEnd { get; set; }
        //[Required]
        //[DisplayName("GIS Alignment Sheet Length Discrepancy")]
        //[DisplayFormat(DataFormatString = "{0:0.0}", ApplyFormatInEditMode = false)]
        //public decimal LengthDiscrepancy { get; set; }
        [DisplayName("Feature Number")]
        public decimal FeatureNumber { get; set; }
        [DisplayName("Feature")]
        public Nullable<int> FeatureID { get; set; }
        [DisplayName("Type")]
        public Nullable<int> TypeID { get; set; }
        [DisplayName("Length (feet)")]
        //[DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = false)]
        public decimal Length { get; set; }
        [DisplayName("Job Number/Work Order/Purchase Order #")]
        public string JobWOPO { get; set; }
        [DisplayName("Unknown")]
        public bool JobWOPOUnknown { get; set; }
        [DisplayName("Install Date mm/dd/yyyy")]
        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<DateTime> InstallDate { get; set; }
        
        [DisplayName("Unknown")]
        public bool InstallDateUnknown { get; set; }
        [DisplayName("In-Service Date mm/dd/yyyy")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable <DateTime> InServiceDate { get; set; }
        [DisplayName("Unknown")]
        public bool InServiceDateUnknown { get; set; }
        [DisplayName("Construction Type")]
        public Nullable<int> ConstructionTypeID { get; set; }
        [DisplayName("Notes / Comments")]
        //[RegularExpression(@"!""#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\]^_`abcdefghijklmnopqrstuvwxyz{|}~", ErrorMessage = "Please enter valid characters.")]
        public string Notes { get; set; }
        [DisplayName("Drawing Identifier")]
        public Nullable<int> DrawingID { get; set; }
        [DisplayName("O.D. 1 (inch)")]
        public Nullable<int> ODID1 { get; set; }
        [DisplayName("OD Records Identifier 1")]
        public Nullable<int> ODRecordID1 { get; set; }
        [DisplayName("OD Records Identifier 2")]
        public Nullable<int> ODRecordID2 { get; set; }
        [DisplayName("OD Record Matrix Check")]
        public Nullable<int> ODRecordMatrixCheck { get; set; }
        [DisplayName("W.T. 1 (inch)")]
        [Range(0, 1, ErrorMessage = "Value must be between 0\" and 1\"")]
        //[DisplayFormat(DataFormatString = "{0:0.0000}", ApplyFormatInEditMode = true)]
        public Nullable<double> WallThickness1 { get; set; }
        [DisplayName("WT Records Identifier 1")]
        public Nullable<int> WTRecordID1 { get; set; }
        [DisplayName("WT Records Identifier 2")]
        public Nullable<int> WTRecordID2 { get; set; }
        [DisplayName("WT Record Matrix Check")]
        public Nullable<int> WTRecordMatrixCheck { get; set; }
        [DisplayName("O.D. 2 (inch)")]
        public Nullable<int> ODID2 { get; set; }
        [DisplayFormat(DataFormatString = "{0:0.0000}", ApplyFormatInEditMode = true)]
        [DisplayName("W.T. 2 (inch)")]
        [Range(0, 1, ErrorMessage="Value must be between 0\" and 1\"")]
        public Nullable<double> WallThickness2 { get; set; }
        [DisplayName("Seam Weld Type")]
        public Nullable<int> SeamWeldTypeID { get; set; }
        [DisplayName("Seam Records Identifier 1")]
        public Nullable<int> SeamRecordID1 { get; set; }
        [DisplayName("Seam Records Identifier 2")]
        public Nullable<int> SeamRecordID2 { get; set; }
        [DisplayName("Seam Record Matrix Check")]
        public Nullable<int> SeamRecordMatrixCheck { get; set; }
        [DisplayName("Specification")]
        public Nullable<int> SpecRatingID { get; set; }
        [DisplayName("Grade")]
        public Nullable<int> GradeID { get; set; }
        [DisplayName("Spec/Grade Records Identifier 1")]
        public Nullable<int> SpecRatingRecordID1 { get; set; }
        [DisplayName("Spec/Grade Records Identifier 2")]
        public Nullable<int> SpecRatingRecordID2 { get; set; }
        [DisplayName("Spec/Grade Record Matrix Check")]
        public Nullable<int> SpecRatingRecordMatrixCheck { get; set; }
        [DisplayName("ANSI Rating")]
        public Nullable<int> ANSIRatingID { get; set; }
        [DisplayName("Material Type")]
        public Nullable<int> MaterialTypeID { get; set; }
        [DisplayName("Angle")]
        public Nullable<int> AngleID { get; set; }
        [DisplayName("Radius")]
        public Nullable<int> RadiusID { get; set; }
        [DisplayName("Orient")]
        public Nullable<int> OrientID { get; set; }
        [DisplayName("Coating Type")]
        public Nullable<int> CoatingTypeID { get; set; }
        [DisplayName("Coating Description")]
        public string Description { get; set; }
        [DisplayName("Coating Desc. Unknown")]
        public bool DescriptionUnknown { get; set; }
        [DisplayName("Manufacturer")]
        public Nullable<int> ManufacturerID { get; set; }
        [DisplayName("Manufacturer Type")]
        public Nullable<int> ManufacturerTypeID { get; set; }
        [DisplayName("Mill")]
        public string Mill { get; set; }
        [DisplayName("Unknown")]
        public bool MillUnknown { get; set; }
        [DisplayName("MFR Date mm/dd/yyyy")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<DateTime> MFRDate { get; set; }
        [DisplayName("Unknown")]
        public bool MFRDateUnknown { get; set; }
        [DisplayName("Current Class Loc.")]
        public Nullable<int> CurrentClassLoc { get; set; }
        [DisplayName("Class Design Factor")]
        public string ClassDesignFactor { get; set; }
        //[DisplayName("HCA Name")]
        //public string HCAName { get; set; }
        [DisplayName("SMYS [S] (psi)")]
        public Nullable<int> SMYS { get; set; }
        [DisplayName("Outside Diameter [D] (inch)")]
        public string OutsideDiameter { get; set; }
        [DisplayName("Wall Thickness [t] (inch)")]
        public string WallThickness { get; set; }
        [DisplayName("Design Factor [F]")]
        public string DesignFactor { get; set; }
        [DisplayName(" Joint Factor [E]")]
        public string JointFactor { get; set; }
        [DisplayName("Temperature Derating [T]")]
        public string TemperatureDerating { get; set; }
        [DisplayName("Design Pressure [P] (psig)")]
        public string DesignPressure { get; set; }
        [DisplayName("Builder ID")]
        public Nullable<int> BuilderID { get; set; }
        [DisplayName("Checker ID")]
        public Nullable<int> CheckerID { get; set; }
        [DisplayName("Engineer ID")]
        public Nullable<int> EngineerID { get; set; }
        [DisplayName("Created By")]
        public Int64 CreatedBy_UserID { get; set; }
        [DisplayName("Created Date/Time")]
        public DateTime CreatedOn { get; set; }
        [DisplayName("Modified By")]
        public Int64 ModifiedBy_UserID { get; set; }
        [DisplayName("Modified Date/Time")]
        public DateTime ModifiedOn { get; set; }


        [DisplayName("Grade Record ID 1")]
        public Nullable<int> GradeRecordID1 { get; set; }
        [DisplayName("Grade Record ID 2")]
        public Nullable<int> GradeRecordID2 { get; set; }
        [DisplayName("Grade Record Matrix Check")]
        public Nullable<int> GradeRecordMatrixCheck { get; set; }
        [DisplayName("MFR Model Number")]
        public string MFRModelNumber { get; set; }
        [DisplayName("PressureTestRecordID")]
        public Nullable<int> PTRID { get; set; }
        [DisplayName("Feature Mark Number")]
        public string FeatureMarkNumber { get; set; }
        [DisplayName("HCA Status ID")]
        public Nullable<int> HCAStatusID { get; set; }

        [DisplayName("Class Exceptions ID")]
        public Nullable<int> ClassExceptionsID { get; set; }

        [DisplayName("Established Operating Pressure")]
        public Nullable<int> OperatingPressure { get; set; }
        //[DisplayName("From Series")]
        [DisplayName("From Pressure Zone")]
        public Nullable<int> FromSeries { get; set; }
        //[DisplayName("To Series")]
        [DisplayName("To Pressure Zone")]
        public Nullable<int> ToSeries { get; set; }


        [DisplayName("OD1 RecID 1 DocType")]
        public string ODRecordID1DocType { get; set; }
        [DisplayName("OD1 RecID 2  DocType")]
        public string ODRecordID2DocType { get; set; }

        [DisplayName("WT1 RecID 1 DocType")]
        public string WTRecordID1DocType { get; set; }
        [DisplayName("WT1 RecID 2  DocType")]
        public string WTRecordID2DocType { get; set; }

        [DisplayName("Seam Weld RecID 1 DocType")]
        public string SeamRecordID1DocType { get; set; }
        [DisplayName("Seam Weld RecID 2  DocType")]
        public string SeamRecordID2DocType { get; set; }

        [DisplayName("Grade RecID 1 DocType")]
        public string GradeRecordID1DocType { get; set; }
        [DisplayName("Grade RecID 2  DocType")]
        public string GradeRecordID2DocType { get; set; }

        [DisplayName("Rating Class RecID 1 DocType")]
        public string SpecRatingRecordID1DocType { get; set; }
        [DisplayName("Rating Class RecID 2  DocType")]
        public string SpecRatingRecordID2DocType { get; set; }


    }
}