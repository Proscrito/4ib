using System.ComponentModel.DataAnnotations;

namespace Solana.Web.Admin.Models.Requests.SiteTypes
{
    public class CreateSiteTypeRequest
    {
        public int InvSiteTypeId { get; set; }
        [Required(ErrorMessage = "Please Enter Description")]
        [StringLength(50, ErrorMessage = "Description should be 50 chars or less")]
        public string Description { get; set; }
        public int? ParentInvSiteTypeId { get; set; }
        public bool ClaimsSecondBreakfast { get; set; }
        public bool DailyEntry { get; set; }
    }
}
