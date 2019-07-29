namespace Solana.Web.Admin.Models.Requests.Schools.NestedModels
{
    public class SiteOptionsModel
    {
        public bool IsClaimChildSitesSeparately { get; set; }
        public bool IsRefundAllowed { get; set; }
        public decimal SiteAttendanceFactor { get; set; }
        public bool IsCEP { get; set; }
        public bool IsDontInactivateMissingStudent { get; set; }
        public bool IsSevereNeed { get; set; }
        public bool IsWarehouse { get; set; }
        public bool IsProvisionSite { get; set; }
        public int? BaseYearStart { get; set; }
        public int? BaseYearEnrollment { get; set; }
        public int? BaseYearFree { get; set; }
        public int? BaseYearReduced { get; set; }
    }
}