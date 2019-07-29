namespace Solana.Web.Admin.Models.Responses.YearEndProcess.NestedModels
{
    public class GradeSitePromotionModel
    {
        public int AdmSiteID { get; set; }
        public string SiteID { get; set; }
        public string SiteDescription { get; set; }
        public int? PromotingPosGradeID { get; set; }
        public int? PromoteToAdmSiteID { get; set; }
        public int? LastUpdatedBy { get; set; }
        public string LastUpdatedDate { get; set; }
        public string ErrorMsgString { get; set; }
        public bool? IsProvisionSite { get; set; }
        public bool? IsCEP { get; set; }
        public int? BaseYearStart { get; set; }
        public bool PromoteToIsCEP { get; set; }
        public bool PromoteToIsProvisionSite { get; set; }
        public int? PromoteToBaseYearStart { get; set; }
    }
}
