namespace Solana.Web.Admin.Models.Responses.FeedingFigures.NestedModels
{
    public class FeedingFigure
    {
        public int AdmSiteId { get; set; }
        public string SchoolName { get; set; }
        public int? Breakfast { get; set; }
        public int? Lunch { get; set; }
        public int? Snack { get; set; }
        public int? Supper { get; set; }
    }
}
