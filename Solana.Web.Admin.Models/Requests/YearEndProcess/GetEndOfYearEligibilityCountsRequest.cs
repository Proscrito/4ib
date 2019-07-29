namespace Solana.Web.Admin.Models.Requests.YearEndProcess
{
    public class GetEndOfYearEligibilityCountsRequest
    {
        public bool PurgeGrads { get; set; }
        public bool PurgeInactives { get; set; }
        public bool IsBeforeRollover { get; set; }
        public int AdmSiteID { get; set; }
        public bool ShouldGetAllSchools { get; set; }
        public bool PromoteStuds { get; set; }
    }
}
