using System.Collections.Generic;
using Solana.Web.Admin.Models.Responses.SchoolGroups.NestedModels;

namespace Solana.Web.Admin.Models.Responses.SchoolGroups
{
    public class GetAdmSchoolGroupSitesResponse
    {
        public ICollection<AdmSchoolGroupSiteViewModel> Items { get; set; }
    }
}
