using System.Collections.Generic;
using System.Threading.Tasks;
using Solana.Web.Admin.Models.Requests.Schools;
using Solana.Web.Admin.Models.Requests.Schools.NestedModels;

namespace Solana.Web.Admin.BLL.Interfaces
{
    public interface ISchoolLogic
    {
        Task AppViewsAdmUsersPreferencesInit(int admUserId, string id = "");
        Task<int> GetParentLevel(int admSiteid);
        Task<int> GetSiteIdInUse(string siteId, int admSiteId);
        Task<int> GetSiteNameInUse(string siteName, int admSiteId);
        Task<string> GetStateInfo(int admSiteId);
        Task SaveSchoolsInformation(PutSchoolsInformationRequest model);
        Task<List<KeyValuePair<string, string>>> ValidateSchoolData(PutSchoolsInformationRequest request);
        Task<List<GradeTransfer>> GetGradeTransferList(int id);
        Task<List<ServingSites>> GetServingSite(int id);
        Task<List<SchoolTypesViewModel>> GetSchoolTypes();
        Task<List<AdmSiteModel>> GetSchoolNames(string searchValue);
        Task<List<AdmSiteModel>> GetSchoolSiteIDs(string searchValue);
        Task<int> CreateMissingSchoolCalendar(PostMissingSchoolCalendarRequest request);
        Task<int> CopySiteCalendar(PutSiteCalendarRequest model);
    }
}
