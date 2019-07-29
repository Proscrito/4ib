namespace Solana.Web.Admin.Models.Requests.IntegrationJobs
{
    public class GetIntegrationMapTypesRequest
    {
        public bool IsImport { get; set; }
        public int AdmSiteId { get; set; }
    }
}