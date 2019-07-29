using System.Threading.Tasks;
using Solana.Web.Admin.Models.Requests.ServingPeriods;
using Solana.Web.Admin.Models.Responses.ServingPeriods;

namespace Solana.Web.Admin.BLL.Interfaces
{
    public interface IServingPeriodsLogic
    {
        Task<GetServingPeriodsResponse> GetServingPeriods();
        Task<PutServingPeriodsResponse> SaveServingPeriods(PutServingPeriodsRequest request);
        Task<bool> DeleteServingPeriods(int id);
    }
}
