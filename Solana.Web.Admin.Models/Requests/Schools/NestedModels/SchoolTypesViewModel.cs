using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Solana.Web.Admin.Models.Requests.Schools.NestedModels
{
    public class SchoolTypesViewModel
    {
        public SchoolTypesViewModel()
        {
            this.Schools = new List<SchoolsViewModel>();
        }

        [Display(Name = "Type")]
        public int InvSiteTypeId { get; set; }
        public string InvSiteTypeDescription { get; set; }
        public List<SchoolsViewModel> Schools { get; set; }
    }
}
