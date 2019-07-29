using System.Collections.Generic;
using System.Threading.Tasks;
using Solana.Web.Admin.Models.Requests.SchoolGroups;
using Solana.Web.Admin.Models.Responses.SchoolGroups;
using Solana.Web.Admin.Models.Responses.SchoolGroups.NestedModels;

namespace Solana.Web.Admin.BLL.Interfaces
{
    public interface ISchoolGroupsLogic
    {
        Task<List<AdmSchoolGroupViewModel>> GetAdmSchoolGroups();
        Task<List<AdmSchoolGroupSiteViewModel>> GetAdmSchoolGroupSites(GetAdmSchoolGroupSiteRequest request);
        Task<GetAdmSchoolGroupResponse> GetAdmSchoolGroup(int id);
        Task DeleteAdmSchoolGroup(int id);
        Task<bool> IsUniqueGroupName(GetIsUniqueGroupNameRequest request);
        Task<int> SaveAdmSchoolGroup(PutAdmSchoolGroupRequest request);
    }
}
