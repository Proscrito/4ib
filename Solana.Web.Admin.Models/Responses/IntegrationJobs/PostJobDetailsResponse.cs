using System;
using System.Collections.Generic;
using Horizon.Common.Repository.Legacy.Models.Adm;
using Horizon.Common.Repository.Legacy.Models.Common;

namespace Solana.Web.Admin.Models.Responses.IntegrationJobs
{
    public class PostJobDetailsResponse
    {
        public virtual ICollection<AdmIntegrationJobsFile> AdmIntegrationJobsFiles { get; set; }
        public virtual AdmIntegrationMap AdmIntegrationMap { get; set; }
        public virtual ICollection<AdmIntegrationJobCriteriaColumn> AdmIntegrationJobCriteriaColumns { get; set; }
        public bool IsNutriscan { get; set; }
        public int GradeSchoolChangeThreshhold { get; set; }
        public bool IsOverrideGradeSchoolChangeThreshhold { get; set; }
        public string FileFtpPassword { get; set; }
        public string FileFtpUserName { get; set; }
        public string FileLocation { get; set; }
        public IntegrationJobFileSource FileSourceType { get; set; }
        public bool IsAutomated { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsAppImport { get; set; }
        public bool IsDCImport { get; set; }
        public bool IsUpdateOnly { get; set; }
        public DateTime? LastRun { get; set; }
        public int ThreshholdErrorsPercent { get; set; }
        public bool IsThreshholdErrors { get; set; }
        public int ThreshholdNewRecordsPercent { get; set; }
        public bool IsThreshholdNewRecords { get; set; }
        public bool IsInactivateObjectsNotInSource { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public int AdmIntegrationMapID { get; set; }
        public int AdmIntegrationJobID { get; set; }
        public virtual ICollection<AdmIntegrationResult> AdmIntegrationResults { get; set; }
        public virtual ICollection<AdmScheduledTask> AdmScheduledTasks { get; set; }
    }
}