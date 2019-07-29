namespace Solana.Web.Admin.Models.Requests.IntegrationJobs
{
    public class PostLaunchJobRequest
    {
        public int JobId { get; set; }
        public int FileId { get; set; }
        public int AdmUserId { get; set; }
        public int CustomerId { get; set; }
    }
}