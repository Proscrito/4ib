using System.Collections.Generic;
using Solana.Web.Admin.Models.Responses.SchoolGroups.NestedModels;

namespace Solana.Web.Admin.Models.Requests.SchoolGroups
{
    public class PutAdmSchoolGroupRequest
    {
        public int AdmSchoolGroupID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsCEPGroup { get; set; }
        public bool ShowCEP { get; set; }
        public ICollection<AdmSchoolGroupSiteSaveModel> AdmSchoolGroupSites { get; set; }
    }
}
