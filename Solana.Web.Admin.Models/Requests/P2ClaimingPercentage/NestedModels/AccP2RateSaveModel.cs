namespace Solana.Web.Admin.Models.Requests.P2ClaimingPercentage.NestedModels
{
    public class AccP2RateSaveModel
    {
        public int AccP2RateID { get; set; }
        public int AdmSiteID { get; set; }
        public string SchoolDescription { get; set; }
        public bool IsBreakfastOnly { get; set; }
        public decimal BRFreeRate { get; set; }
        public decimal BRReducedRate { get; set; }
        public decimal BRFullPayRate { get; set; }
        public decimal LUFreeRate { get; set; }
        public decimal LUReducedRate { get; set; }
        public decimal LUFullPayRate { get; set; }
    }
}