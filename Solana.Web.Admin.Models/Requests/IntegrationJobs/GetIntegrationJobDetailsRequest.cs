namespace Solana.Web.Admin.Models.Requests.IntegrationJobs
{
    public class GetIntegrationJobDetailsRequest
    {
        public int? Id { get; set; }
        public bool? IsImport { get; set; }
        public string MapTypeName { get; set; } = null;
        public int JobId { get; set; }
        public int CurrentAdmUserId { get; set; }
    }
}