using System.Collections.Generic;
using Horizon.Common.Repository.Legacy.Models.Common;
using Solana.Web.Admin.Models.Requests.IntegrationJobs.NestedModels;
using JobCriteraColumn = Solana.Web.Admin.Models.Responses.IntegrationJobs.NestedModels.JobCriteraColumn;

namespace Solana.Web.Admin.Models.Requests.IntegrationJobs
{
    public class PostJobDetailsRequest
    {
        public int AdmIntegrationJobID { get; set; }
        public bool IsDCImport { get; set; }
        public int? admSiteId { get; set; }
        public string FileLocation { get; set; }
        public string FileFtpUserName { get; set; }
        public string FileFtpPassword { get; set; }
        public bool IsImport { get; set; }
        public IntegrationMapType MapType { get; set; }
        public int AdmIntegrationMapID { get; set; }
        public string JobName { get; set; }
        public string JobDescription { get; set; }
        public bool IsInactivateObjectsNotInSource { get; set; }
        public bool IsThreshholdNewRecords { get; set; }
        public int ThreshholdNewRecordsPercent { get; set; }
        public bool IsThreshholdErrors { get; set; }
        public int ThreshholdErrorsPercent { get; set; }
        public bool IsUpdateOnly { get; set; }
        public bool IsAppImport { get; set; }
        public bool IsAutomated { get; set; }
        public IntegrationJobFileSource FileSourceType { get; set; }
        public bool IsOverrideGradeSchoolChangeThreshhold { get; set; }
        public ICollection<JobCriteraColumn> JobCriteriaColumns { get; set; }
        public TaskModel TaskModel { get; set; }
        public int AdmUserID { get; set; }
    }
}