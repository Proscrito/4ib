using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Horizon.Common.Repository.Legacy.Models.Common;
using Solana.Web.Admin.Models.Responses.IntegrationJobs.NestedModels;

namespace Solana.Web.Admin.Models.Responses.IntegrationJobs
{
    public class GetIntegrationJobDetailsResponse
    {
        public int AdmIntegrationJobID { get; set; }
        public int AdmIntegrationMapID { get; set; }
        public int MapID { get; set; }
        [Required(ErrorMessage = "Please Enter Job Name")]
        public string JobName { get; set; }
        [Required(ErrorMessage = "Please Enter Job Description")]
        public string JobDescription { get; set; }
        public bool IsInactivateObjectsNotInSource { get; set; }
        public bool IsThreshholdNewRecords { get; set; }
        public int ThreshholdNewRecordsPercent { get; set; }
        public bool IsThreshholdErrors { get; set; }
        public int ThreshholdErrorsPercent { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? LastRun { get; set; }
        public bool IsUpdateOnly { get; set; }
        public bool IsDCImport { get; set; }
        public bool IsAppImport { get; set; }
        public int FileID { get; set; }
        public bool IsAutomated { get; set; }
        public IntegrationJobFileSource FileSourceType { get; set; }
        public string FileLocation { get; set; }
        public string FileFtpUserName { get; set; }
        public string FileFtpPassword { get; set; }
        public bool IsImport { get; set; }
        public int MapType { get; set; }
        public bool IsOverrideGradeSchoolChangeThreshhold { get; set; }
        public int GradeSchoolChangeThreshhold { get; set; }
        public ICollection<IntegrationMapItem> Maps { get; set; }
        public string CNIPsVendorNumber { get; set; }
        public string CNIPsCDSNumber { get; set; }
        public string CNIPsLicenseNumber { get; set; }
        public string CNIPsSponsor { get; set; }
        public string CNIPsID { get; set; }
        //model for binding scheduled task information
        public AdmScheduledTaskItem TaskModel { get; set; }

        public ICollection<JobCriteraColumn> JobCriteriaColumns { get; set; }
    }
}