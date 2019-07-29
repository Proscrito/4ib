using System.Collections.Generic;
using System.Threading.Tasks;
using Solana.Web.Admin.Models.Requests.ManagementLevels.NestedModels;
using Solana.Web.Admin.Models.Responses.ManagementLevels.NestedModels;

namespace Solana.Web.Admin.BLL.Interfaces
{
    public interface IManagementLevelsLogic
    {
        Task<List<AdmManagementLevelViewModel>> GetAdmManagementLevels();
        Task SaveAdmManagementLevels(IEnumerable<AdmManagementLevelSaveModel> requestItems);
    }
}
