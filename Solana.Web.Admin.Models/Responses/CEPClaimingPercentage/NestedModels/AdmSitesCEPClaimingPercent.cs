namespace Solana.Web.Admin.Models.Responses.CEPClaimingPercentage.NestedModels
{
    public class AdmSitesCEPClaimingPercent
    {
        public int AdmSiteSchoolGroupID { get; set; }
        public string AdmSiteSchoolGroupName { get; set; }
        public decimal? CEPFreeRate { get; set; }
        public decimal? CEPFullPayRate { get; set; }
        public int CEPSchoolOption { get; set; }
        public int CEPOptionType { get; set; }
    }
}
