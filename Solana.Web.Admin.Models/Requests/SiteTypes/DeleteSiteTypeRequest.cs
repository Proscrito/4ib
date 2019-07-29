namespace Solana.Web.Admin.Models.Requests.SiteTypes
{
    public class DeleteSiteTypeRequest
    {
        public int InvSiteTypeId { get; set; }
        public string Description { get; set; }
        public int? ParentInvSiteTypeId { get; set; }
        public bool ClaimsSecondBreakfast { get; set; }
        public bool DailyEntry { get; set; }
    }
}
