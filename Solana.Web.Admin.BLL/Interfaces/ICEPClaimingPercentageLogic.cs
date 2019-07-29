using System.Collections.Generic;
using System.Threading.Tasks;
using Solana.Web.Admin.Models.Requests.CEPClaimingPercentage;
using Solana.Web.Admin.Models.Responses.CEPClaimingPercentage.NestedModels;

namespace Solana.Web.Admin.BLL.Interfaces
{
    public interface ICEPClaimingPercentageLogic
    {
        Task<int> SaveCEPClaimRates(PutCEPClaimRatesRequest cepClaimRatesRequest);
        Task<List<AdmSitesCEPClaimingPercent>> GetAdmSitesCEPClaimingPercent(int admUserID);
    }
}
