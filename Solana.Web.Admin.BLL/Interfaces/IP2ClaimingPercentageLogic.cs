using System.Collections.Generic;
using System.Threading.Tasks;
using Solana.Web.Admin.Models.Requests.P2ClaimingPercentage.NestedModels;
using Solana.Web.Admin.Models.Responses.P2ClaimingPercentage.NestedModels;

namespace Solana.Web.Admin.BLL.Interfaces
{
    public interface IP2ClaimingPercentageLogic
    {
        Task<List<AccP2RateViewModel>> GetAccP2Rates();
        Task<List<int>> SaveAccP2Rates(IEnumerable<AccP2RateSaveModel> requestItems);
    }
}
