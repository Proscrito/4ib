using System;

namespace Solana.Web.Admin.Models.Responses.IntegrationJobs.NestedModels
{
    public class IntegrationJobHistory
    {
        public int AdmIntegrationJobID { get; set; }
        public int AdmIntegrationResultID { get; set; }
        public int AdmUserID { get; set; }
        public string UserName { get; set; }
        public int NumberOfErrors { get; set; }
        public int NumberOfNewRecordsCreated { get; set; }
        public int NumberOfRecordsImported { get; set; }
        public int NumberOfRecordsExported { get; set; }
        public string ImportFileNames { get; set; }
        public int AdmIntegrationJobsFileID { get; set; }
        public DateTime EndTime { get; set; }
    }
}
