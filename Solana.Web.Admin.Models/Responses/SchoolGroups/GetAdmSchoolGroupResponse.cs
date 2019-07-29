using System.Collections.Generic;
using Solana.Web.Admin.Models.Responses.SchoolGroups.NestedModels;

namespace Solana.Web.Admin.Models.Responses.SchoolGroups
{
    public class GetAdmSchoolGroupResponse
    {
        public int AdmSchoolGroupID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsCEPGroup { get; set; }
        public bool ShowCEP { get; set; }
        //not sure if we want to keep that, it is not used in the old code...
        public ICollection<AdmSchoolGroupSiteViewModel> AdmSchoolGroupSites { get; set; }
    }
}
