using System.ComponentModel.DataAnnotations;

namespace Solana.Web.Admin.Models.Requests.Schools.NestedModels
{
    public class UserSitesViewModel
    {
        public UserSitesViewModel()
        {
            JustMySchools = true;
        }

        [Display(Name = "School")]
        public int AdmUserId { get; set; }
        public int DefaultAdmSiteId { get; set; }
        public bool RemoveCentralOffice { get; set; }
        public bool JustMySchools { get; set; }
    }
}
