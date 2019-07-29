namespace Solana.Web.Admin.Models.Responses
{
    public class GetAvailableSiteSummaryResponse
    {
        public int AdmSiteID { get; set; }
        public int SchoolGroupId { get; set; }
        public string SchoolGroupName { get; set; }
        public string SiteDescription { get; set; }
        public string SiteID { get; set; }
    }
}
