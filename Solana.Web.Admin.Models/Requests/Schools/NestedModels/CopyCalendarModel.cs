namespace Solana.Web.Admin.Models.Requests.Schools.NestedModels
{
    public class CopyCalendarModel
    {
        public int FromAdmSiteId { get; set; }
        public int[] ToAdmSiteIds { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string SchoolStartDate { get; set; }
        public string SchoolEndDate { get; set; }
    }
}