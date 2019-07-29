namespace Solana.Web.Admin.Models.Requests.YearEndProcess
{
    public class GetTotalPreviewRequest
    {
        public bool PurgeGrads { get; set; }
        public bool PromoteStuds { get; set; }
        public int BalanceOption { get; set; }
        public int AdmSiteID { get; set; }
        public bool ShouldGetAllSchools { get; set; }
        public bool IsBeforeRollover { get; set; }
    }
}
