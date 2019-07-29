namespace Solana.Web.Admin.Models.Responses.IntegrationJobs.NestedModels
{
    public class IntegrationJob
    {
        public int AdmIntegrationJobID { get; set; }
        public string Name { get; set; }
        public string MapTypeName { get; set; }
        public string JobType { get; set; }
        public bool IsDeleted { get; set; }
    }
}
