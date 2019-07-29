using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Solana.Web.Admin.BLL.Interfaces;
using Solana.Web.Admin.Models.Requests.IntegrationJobs;
using Solana.Web.Admin.Models.Responses.IntegrationJobs;

namespace Solana.Web.Admin.API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class IntegrationJobsController : ControllerBase
    {
        private readonly IIntegrationJobsLogic _integrationJobsLogic;

        public IntegrationJobsController(IIntegrationJobsLogic integrationJobsLogic)
        {
            _integrationJobsLogic = integrationJobsLogic; 
        }

        /// <summary>
        /// Gets the jobs.
        /// old controller: ReadJobList
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<GetJobsResponse>> GetJobs()
        {
            return await _integrationJobsLogic.GetJobs();
        }

        /// <summary>
        /// Gets the job history.
        /// old controller: ReadHistory
        /// </summary>
        /// <param name="jobId">The job identifier.</param>
        /// <returns></returns>
        [HttpGet("History")]
        public async Task<ActionResult<GetJobHistoryResponse>> GetJobHistory(int jobId)
        {
            return await _integrationJobsLogic.GetJobHistory(jobId);
        }

        /// <summary>
        /// Gets the job errors.
        /// old controller: ReadJobErrorList
        /// </summary>
        /// <param name="jobId">The job identifier.</param>
        /// <returns></returns>
        [HttpGet("Errors")]
        public async Task<ActionResult<GetJobErrorsResponse>> GetJobErrors(int jobId)
        {
            return await _integrationJobsLogic.GetJobErrors(jobId);
        }

        /// <summary>
        /// Gets the integration job details.
        /// old controller: GetIntegrationJobDetails
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpGet("IntegrationJobDetails")]
        public async Task<ActionResult<GetIntegrationJobDetailsResponse>> GetIntegrationJobDetails(GetIntegrationJobDetailsRequest request)
        {
            return await _integrationJobsLogic.GetIntegrationJobDetails(request);
        }

        /// <summary>
        /// Launches the job.
        /// old controller: LaunchJob
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpPost("LaunchJob")]
        public ActionResult<bool> LaunchJob(PostLaunchJobRequest request)
        {
            return _integrationJobsLogic.LaunchJob(request);
        }

        /// <summary>
        /// Deletes the job.
        /// old controller: DeleteJob
        /// </summary>
        /// <param name="jobId">The job identifier.</param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteJob(int jobId)
        {
            return await _integrationJobsLogic.DeleteJob(jobId);
        }

        /// <summary>
        /// Saves the job details.
        /// old controller: SaveJobDetails
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<PostJobDetailsResponse>> SaveJobDetails(PostJobDetailsRequest request)
        {
            return await _integrationJobsLogic.SaveJobDetails(request);
        }

        /// <summary>
        /// Validates the name.
        /// old controller: ValidateName
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpPost("ValidateName")]
        public async Task<ActionResult<bool>> ValidateName(PostValidateNameRequest request)
        {
            return await _integrationJobsLogic.ValidateName(request);
        }

        /// <summary>
        /// Gets the eligibilities.
        /// old controller: GetEligibilities
        /// </summary>
        /// <returns></returns>
        [HttpGet("Eligibilities")]
        public async Task<ActionResult<GetEligibilitiesResponse>> GetEligibilities()
        {
            return await _integrationJobsLogic.GetEligibilities();
        }

        /// <summary>
        /// Gets the vendors.
        /// old controller: GetVendors
        /// </summary>
        /// <returns></returns>
        [HttpGet("Vendors")]
        public async Task<ActionResult<GetVendorsResponse>> GetVendors()
        {
            return await _integrationJobsLogic.GetVendors();
        }

        /// <summary>
        /// Gets the integration map types.
        /// old controller: GetIntegrationMapTypes
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpGet("IntegrationMapTypes")]
        public async Task<ActionResult<GetIntegrationMapTypesResponse>> GetIntegrationMapTypes(GetIntegrationMapTypesRequest request)
        {
            return await _integrationJobsLogic.GetIntegrationMapTypes(request);
        } 
    } 
}
