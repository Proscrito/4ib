using System.Collections.Generic;
using System.Threading.Tasks;
using Solana.Web.Admin.Models.Requests.GlobalOptions;
using Solana.Web.Admin.Models.Requests.GlobalOptions.NestedModels;
using Solana.Web.Admin.Models.Responses.GlobalOptions;
using Solana.Web.Admin.Models.Responses.GlobalOptions.NestedModels;
using MenLeftoverCodeModel = Solana.Web.Admin.Models.Responses.GlobalOptions.NestedModels.MenLeftoverCodeModel;

namespace Solana.Web.Admin.BLL.Interfaces
{
    public interface IGlobalOptionsLogic
    {
        Task<GetAdmGlobalOptionsResponse> GetAdmGlobalOptions();
        Task<PutAdmGlobalOptionsResponse> SaveAdmGlobalOption(PutAdmGlobalOptionsRequest request);
        Task<List<MenAgeGroupsModel>> GetMenAgeGroups();
        Task<bool> GetIsGenerateAppNumbers();
        Task<List<PosReasonModel>> GetPosReasons();
        Task DeletePosReason(int id);
        Task<bool> GetIsReasonExist(string reason);
        Task<int> SavePosReason(string reason);
        Task<List<MenLeftoverCodeModel>> GetMenLeftoverCodes();
        Task<List<InvTransactionTypeModel>> GetInvTransactionTypes();
        Task DeleteMenLeftoverCode(int id);
        Task<List<int>> SaveMenLeftoverCodes(IEnumerable<MenLeftoverCodeSaveModel> menLeftoverCodeModels);
        Task<List<int>> SaveInvTransactionTypes(IEnumerable<InvTransactionTypeSaveModel> invTransactionTypeSaveModels);
        Task DeleteInvTransactionType(int id);
    }
}
