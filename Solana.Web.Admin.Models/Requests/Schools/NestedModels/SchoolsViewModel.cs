namespace Solana.Web.Admin.Models.Requests.Schools.NestedModels
{
    public class SchoolsViewModel
    {
        public int AdmSiteId { get; set; }
        public string SiteId { get; set; }
        public string SiteDescription { get; set; }
        public bool IsActive { get; set; }
        public int InvSiteTypeId { get; set; }
        public string State { get; set; }
    }
}