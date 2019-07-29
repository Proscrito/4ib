using System.Collections.Generic;
using System.Threading.Tasks;
using Solana.Web.Admin.Models.Requests.IntegrationMaps;
using Solana.Web.Admin.Models.Responses.IntegrationMaps;
using Solana.Web.Admin.Models.Responses.IntegrationMaps.NestedModels;

namespace Solana.Web.Admin.BLL.Interfaces
{
    public interface IIntegrationMapsLogic
    {
        List<TranslationViewModel> GetTranslations();
        Task<PutIntegrationMapsResponse> SaveIntegrationMap(PutIntegrationMapsRequest request);
        Task<GetIntegrationMapResponse> GetIntegrationMap(GetIntegrationMapRequest request);
        Task SaveIntegrationMapColumns(PutIntegrationMapColumnsRequest request);
        Task<List<IntegrationMapColumnViewModel>> GetIntegrationMapColumns(int mapId);
        Task<int> SaveAppTriageFile(PutAppTriageFileRequest request);
        Task DeleteAppTriageFile(int fileId);
        Task<ICollection<IntegrationMapViewModel>> GetIntegrationMaps();
        Task<GetIntegrationMapResponse> DeleteIntegrationMap(int id);
    }
}