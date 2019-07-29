namespace Solana.Web.Admin.Models.Requests.FeedingFigures
{
    public class PostFeedingFiguresRequest
    {
        public int AdmSiteId { get; set; }
        public string FeedingName { get; set; }
        public int? FeedingNumber { get; set; }
    }
}
