using System.Threading.Tasks;
using Solana.Web.Admin.Models.Requests.IntegrationJobs;
using Solana.Web.Admin.Models.Responses.IntegrationJobs;

namespace Solana.Web.Admin.BLL.Interfaces
{
    public interface IIntegrationJobsLogic
    {
        Task<GetJobsResponse> GetJobs();
        Task<GetJobHistoryResponse> GetJobHistory(int jobId);
        Task<GetJobErrorsResponse> GetJobErrors(int resultId);
        Task<GetIntegrationJobDetailsResponse> GetIntegrationJobDetails(GetIntegrationJobDetailsRequest request);
        bool LaunchJob(PostLaunchJobRequest request);
        Task<bool> DeleteJob(int jobId);
        Task<PostJobDetailsResponse> SaveJobDetails(PostJobDetailsRequest request);
        Task<bool> ValidateName(PostValidateNameRequest request);
        Task<GetEligibilitiesResponse> GetEligibilities();
        Task<GetVendorsResponse> GetVendors();
        Task<GetIntegrationMapTypesResponse> GetIntegrationMapTypes(GetIntegrationMapTypesRequest request); 
    }
}
