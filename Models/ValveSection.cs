using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using PipelineFeatureList.Models;
using System.Web.Mvc;

namespace PipelineFeatureList.Models
{
    public class ValveSection
    {
        [DisplayName("Circuit ID")]
        [Required]
        public Int64 ValveSectionID { get; set; }
        [DisplayName("Station System")]
        [Required(AllowEmptyStrings = false, ErrorMessage="Station System Required")]
        public int? PipeSystemID { get; set; }
        [DisplayName("Station")]
        [Required(ErrorMessage = "Station Required")]
        public int PipelineID { get; set; }
        [DisplayName("Orion Station Series")]
        [Required]
        [RegularExpression(@"^([0-9]\d*)$", ErrorMessage = "Please enter a valid Orion Station Series.")]
        public string OrionStationSeries { get; set; }
        [DisplayName("Circuit Begin")]
        [Required]
        public string ValveSectionBegin { get; set; }
        [DisplayName("Circuit End")]
        public string ValveSectionEnd { get; set; }
        [DisplayName("Mile Point Begin")]
        public Nullable<decimal> MilePointBegin { get; set; }
        [DisplayName("Mile Point End")]
        public Nullable<decimal> MilePointEnd { get; set; }
        [DisplayName("Orion Station Begin")]
        //[InputMask("99999+99.99")]
        [DisplayFormat(DataFormatString = "{0:0+00}", ApplyFormatInEditMode = false)]
        public Nullable<decimal> OrionStationBegin { get; set; }
        [DisplayName("Orion Station End")]
        //[InputMask("999-999-9999")]
        [DisplayFormat(DataFormatString = "{0:0+00}", ApplyFormatInEditMode = false)]
        public Nullable<decimal> OrionStationEnd { get; set; }
        [DisplayName("GIS Start")]
        [DisplayFormat(DataFormatString = "{0:0+00}", ApplyFormatInEditMode = false)]
        public Nullable<decimal> GISStationBegin { get; set; }
        [DisplayName("GIS End")]
        [DisplayFormat(DataFormatString = "{0:0+00}", ApplyFormatInEditMode = false)]
        public Nullable<decimal> GISStationEnd { get; set; }
        [DisplayName("Current MAOP")]
        public Nullable<int> CurrentMAOP { get; set; }
        [DisplayName("Builder")]
        public Nullable<Int64> BuilderID { get; set; }
        [DisplayName("QC")]
        public Nullable<Int64> QCID { get; set; }
        [DisplayName("Engineer")]
        public Nullable<Int64> EngineerID { get; set; }
        [DisplayName("Final Engineer")]
        public Nullable<Int64> FinalEngineerID { get; set; }
        [DisplayName("Annual Reviewer")]
        public Nullable<Int64> AnnualReviewerID { get; set; }
        [DisplayName("Created By")]
        public Int64 CreatedBy_UserID { get; set; }
        [DisplayName("Created Date/Time")]
        public DateTime CreatedOn { get; set; }
        [DisplayName("Modified By")]
        public Int64 ModifiedBy_UserID { get; set; }
        [DisplayName("Modified Date/Time")]
        public DateTime ModifiedOn { get; set; }
        [DisplayName("Valve Section Status")]
        public Nullable<int> ValveSectionStatusID { get; set; }
        [DisplayName("PFL Length")]
        [DisplayFormat(DataFormatString = "{0:0}", ApplyFormatInEditMode = false)]
        public Nullable<decimal> PFLLength { get; set; }
        [DisplayName("Orion vs. PFL Length Discrepancy")]
        [DisplayFormat(DataFormatString = "{0:0.0}", ApplyFormatInEditMode = false)]
        public Nullable<decimal> LengthDiscrepancyPlus { get; set; }
        [DisplayName("Length Discrepancy -")]
        [DisplayFormat(DataFormatString = "{0:0.0}", ApplyFormatInEditMode = false)]
        public Nullable<decimal> LengthDiscrepancyMinus { get; set; }
        [DisplayName("Is Segmentation Dirty")]
        public Nullable<bool> IsSegmentationDirty { get; set; }
        [DisplayName("Crossings Status")]
        public string CrossingsStatus { get; set; }

        [DisplayName("Station System")]
        public PipeSystem PipeSystem { get; set; }
        [DisplayName("Station")]
        public Pipeline Pipeline { get; set; }
        [DisplayName("Status")]
        public ValveSectionStatus ValveSectionStatus { get; set; }
        
        //[DisplayName("Display")]
        //public DisplayGroup DisplayGroup { get; set; }
        //[DisplayName("Builder")]
        //public User Builder { get; set; }
    }

    public class ValveSectionCrossings
    {
        [DisplayName("Missing1")]
        public Nullable<bool> missing1 { get; set; }
        [DisplayName("Missing2")]
        public Nullable<bool> missing2 { get; set; }
        public string CrossingStatus { get; set; }
    }

    public class ValveSectionAdmin
    {
        [DisplayName("Circuit")]
        public ValveSection ValveSection { get; set; }
        //[DisplayName("Display")]
        //public DisplayGroup DisplayGroup { get; set; }
        [DisplayName("Station System")]
        public PipeSystem PipeSystem { get; set; }
        [DisplayName("Station")]
        public Pipeline Pipeline { get; set; }
        [DisplayName("Builder")]
        public User Builder { get; set; }
        [DisplayName("QC")]
        public User QC { get; set; }
        [DisplayName("Engineer")]
        public User Engineer { get; set; }
        [DisplayName("FinalEngineer")]
        public User FinalEngineer { get; set; }
        [DisplayName("Status")]
        public ValveSectionStatus ValveStatus { get; set; }
    }
    public class ValveSectionQC
    {
        public ValveSectionQCAssigned ValveSectionQCAssigned { get; set; }
        public ValveSectionQCPool ValveSectionQCPool { get; set; }
    }
    public class ValveSectionQCAssigned
    {
        [DisplayName("Circuit")]
        public ValveSection ValveSection { get; set; }
        [DisplayName("Station System")]
        public PipeSystem PipeSystem { get; set; }
        [DisplayName("Station")]
        public Pipeline Pipeline { get; set; }
        [DisplayName("Length")]
        public decimal Length { get; set; }
        [DisplayName("Number of Features")]
        public int NumberOfFeatures { get; set; }
        [DisplayName("Status")]
        public ValveSectionStatus ValveStatus { get; set; }
        [DisplayName("Ready for QC")]
        public DateTime ReadyForQC { get; set; }
        [DisplayName("Builder")]
        public User Builder { get; set; }
    }
    public class ValveSectionQCPool
    {
        [DisplayName("Circuit")]
        public ValveSection ValveSection { get; set; }
        [DisplayName("Pipe System")]
        public PipeSystem PipeSystem { get; set; }
        [DisplayName("Pipeline")]
        public Pipeline Pipeline { get; set; }
        [DisplayName("Length")]
        public decimal? Length { get; set; }
        [DisplayName("Number of Features")]
        public int NumberOfFeatures { get; set; }
        [DisplayName("Status")]
        public ValveSectionStatus ValveStatus { get; set; }
        [DisplayName("Ready for QC")]
        public DateTime ReadyForQC { get; set; }
        [DisplayName("Builder")]
        public User Builder { get; set; }
    }
    public class ValveSectionEngineering
    {
        public ValveSectionEngineeringAssigned ValveSectionEngineeringAssigned { get; set; }
        public ValveSectionEngineeringPool ValveSectionEngineeringPool { get; set; }
    }
    public class ValveSectionEngineeringAssigned
    {
        [DisplayName("Circuit")]
        public ValveSection ValveSection { get; set; }
        [DisplayName("Station System")]
        public PipeSystem PipeSystem { get; set; }
        [DisplayName("Station")]
        public Pipeline Pipeline { get; set; }
        [DisplayName("Builder")]
        public User Users { get; set; }
        [DisplayName("Status")]
        public ValveSectionStatus ValveStatus { get; set; }
    }
    public class ValveSectionEngineeringPool
    {
        [DisplayName("Circuit")]
        public ValveSection ValveSection { get; set; }
        [DisplayName("Station System")]
        public PipeSystem PipeSystem { get; set; }
        [DisplayName("Station")]
        public Pipeline Pipeline { get; set; }
        [DisplayName("Builder")]
        public User Users { get; set; }
        [DisplayName("Status")]
        public ValveSectionStatus ValveStatus { get; set; }
    }
    public class ValveSectionFinalEngineering
    {
        public ValveSectionFinalEngineeringAssigned ValveSectionFinalEngineeringAssigned { get; set; }
        public ValveSectionFinalEngineeringPool ValveSectionFinalEngineeringPool { get; set; }
    }
    public class ValveSectionFinalEngineeringAssigned
    {
        [DisplayName("Circuit")]
        public ValveSection ValveSection { get; set; }
        [DisplayName("Station System")]
        public PipeSystem PipeSystem { get; set; }
        [DisplayName("Station")]
        public Pipeline Pipeline { get; set; }
        [DisplayName("Builder")]
        public User Users { get; set; }
        [DisplayName("Status")]
        public ValveSectionStatus ValveStatus { get; set; }
    }
    public class ValveSectionFinalEngineeringPool
    {
        [DisplayName("Circuit")]
        public ValveSection ValveSection { get; set; }
        [DisplayName("Station System")]
        public PipeSystem PipeSystem { get; set; }
        [DisplayName("Station")]
        public Pipeline Pipeline { get; set; }
        [DisplayName("Builder")]
        public User Users { get; set; }
        [DisplayName("Status")]
        public ValveSectionStatus ValveStatus { get; set; }
 
        public IEnumerable<SelectListItem> PipeSystemData { get; set; }
        public int PipeSystemDataID { get; set; }

        public IEnumerable<SelectListItem> PipelineData { get; set; }
        public int PipelineDataID { get; set; }
    }
    public class ValveSectionAnnualReview
    {
        public ValveSectionAnnualReviewAssigned ValveSectionAnnualReviewAssigned { get; set; }
        public ValveSectionAnnualReviewPool ValveSectionAnnualReviewPool { get; set; }
    }
    public class ValveSectionAnnualReviewAssigned
    {
        [DisplayName("Circuit")]
        public ValveSection ValveSection { get; set; }
        [DisplayName("Station System")]
        public PipeSystem PipeSystem { get; set; }
        [DisplayName("Station")]
        public Pipeline Pipeline { get; set; }
        [DisplayName("Builder")]
        public User Users { get; set; }
        [DisplayName("Status")]
        public ValveSectionStatus ValveStatus { get; set; }
    }
    public class ValveSectionAnnualReviewPool
    {
        [DisplayName("Circuit")]
        public ValveSection ValveSection { get; set; }
        [DisplayName("Station System")]
        public PipeSystem PipeSystem { get; set; }
        [DisplayName("Station")]
        public Pipeline Pipeline { get; set; }
        [DisplayName("Builder")]
        public User Users { get; set; }
        [DisplayName("Status")]
        public ValveSectionStatus ValveStatus { get; set; }

        public IEnumerable<SelectListItem> PipeSystemData { get; set; }
        public int PipeSystemDataID { get; set; }

        public IEnumerable<SelectListItem> PipelineData { get; set; }
        public int PipelineDataID { get; set; }
    }
    public class ValveSectionCertificationApproved
    {
        public ValveSectionCertificationApprovedAssigned ValveSectionCertificationApprovedAssigned { get; set; }
        public ValveSectionCertificationApprovedPool ValveSectionCertificationApprovedPool { get; set; }
    }
    public class ValveSectionCertificationApprovedAssigned
    {
        [DisplayName("Circtui")]
        public ValveSection ValveSection { get; set; }
        [DisplayName("Station System")]
        public PipeSystem PipeSystem { get; set; }
        [DisplayName("Station")]
        public Pipeline Pipeline { get; set; }
        [DisplayName("Builder")]
        public User Users { get; set; }
        [DisplayName("Status")]
        public ValveSectionStatus ValveStatus { get; set; }
    }
    public class ValveSectionCertificationApprovedPool
    {
        [DisplayName("Circuit")]
        public ValveSection ValveSection { get; set; }
        [DisplayName("Station System")]
        public PipeSystem PipeSystem { get; set; }
        [DisplayName("Station")]
        public Pipeline Pipeline { get; set; }
        [DisplayName("Builder")]
        public User Users { get; set; }
        [DisplayName("Status")]
        public ValveSectionStatus ValveStatus { get; set; }

        public IEnumerable<SelectListItem> PipeSystemData { get; set; }
        public int PipeSystemDataID { get; set; }

        public IEnumerable<SelectListItem> PipelineData { get; set; }
        public int PipelineDataID { get; set; }
    }

    public class PipelineFeatureListDBContext : DbContext
    {
        public DbSet<ValveSection> ValveSection { get; set; }
        public DbSet<ValveSectionFeature> ValveSectionFeatures { get; set; }

        // Admin Tables
        public DbSet<Feature> Features { get; set; }
        public DbSet<CurrentClassLocation> CurrentClassLocations { get; set; }
        public DbSet<HCA> HCAs { get; set; }
        public DbSet<ConstructionType> ConstructionTypes { get; set; }
        public DbSet<RecordIdentifier> RecordIdentifiers { get; set; }
        public DbSet<OutsideDiameter> OutsideDiameters { get; set; }
        public DbSet<SeamType> SeamTypes { get; set; }
        public DbSet<SpecRating> SpecRatings { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<ANSIRating> ANSIRatings { get; set; }
        public DbSet<MaterialType> MaterialTypes { get; set; }
        public DbSet<BendRadius> BendRadiuses { get; set; }
        public DbSet<Orientation> Orientations { get; set; }
        public DbSet<StandardEntry> StandardEntries { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<PipeType> PipeTypes { get; set; }
        public DbSet<PipeSystem> PipeSystems { get; set; }
        public DbSet<Pipeline> Pipelines { get; set; }
        public DbSet<CoatingType> CoatingTypes { get; set; }
        public DbSet<DocumentType> DocumentTypes { get; set; }
        public DbSet<DocumentRecord> DocumentRecords { get; set; }
        public DbSet<GroupClassification> GroupClassifications { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UsersType> UsersTypes { get; set; }
        public DbSet<DocumentMat> DocumentMats { get; set; }
        public DbSet<ValveSectionStatus> ValveSectionStatus { get; set; }
        public DbSet<ValveSectionErrorLevel> ValveSectionErrorLevels { get; set; }
        public DbSet<ValveSectionError> ValveSectionErrors { get; set; }
        public DbSet<FeatureIssue> FeatureIssues { get; set; }
        public DbSet<FeatureError> FeatureErrors { get; set; }
        public DbSet<ReportingGroup> ReportingGroups { get; set; }
        public DbSet<DisplayGroup> DisplayGroups { get; set; }
        public DbSet<WorkflowAction> WorkflowActions { get; set; }
        public DbSet<WorkflowHistory> WorkflowHistories { get; set; }
        public DbSet<ManufacturerType> ManufacturerTypes { get; set; }
        public DbSet<WorkflowRule> WorkflowRules { get; set; }
        public DbSet<DynamicSegmentationRecord> DynamicSegmentationRecords { get; set; }
        public DbSet<DynamicSegmentation> DynamicSegmentations { get; set; }
        public DbSet<UploadDirectory> UploadDirectories { get; set; }
        public DbSet<CertificationApproved> CertificationApproveds { get; set; }
        public DbSet<ReportEmail> ReportEmails { get; set; }

        public DbSet<Report> Reports { get; set; }
        public DbSet<ClientReport> ClientReports { get; set; }
        public DbSet<ClientImportList> ClientImportLists { get; set; }
        public DbSet<ClientReportReportList> ClientReportReportLists { get; set; }
        public DbSet<ClientReportPipeSystem> ClientReportPipeSystems { get; set; }
        public DbSet<ClientReportPipeline> ClientReportPipelines { get; set; }
        public DbSet<ClientReportImportList> ClientReportImportLists { get; set; }
        
        
    }
}