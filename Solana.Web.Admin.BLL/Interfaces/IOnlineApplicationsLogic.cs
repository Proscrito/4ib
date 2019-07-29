using System.Threading.Tasks;
using Solana.Web.Admin.Models.Requests.OnlineApplication;
using Solana.Web.Admin.Models.Responses.OnlineApplications;

namespace Solana.Web.Admin.BLL.Interfaces
{
    public interface IOnlineApplicationsLogic
    {
        Task<GetAdmFroOptionsResponse> GetAdmFroOptions();
        Task SaveAdmFroOptions(PutAdmFroOptionsRequest request);
    }
}
