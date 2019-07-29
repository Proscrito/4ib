using System.Collections.Generic;
using Solana.Web.Admin.Models.Responses.SchoolGroups.NestedModels;

namespace Solana.Web.Admin.Models.Responses.SchoolGroups
{
    public class GetAdmSchoolGroupsResponse
    {
        public ICollection<AdmSchoolGroupViewModel> Items { get; set; }
    }
}
