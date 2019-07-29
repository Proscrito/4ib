using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Solana.Web.Admin.BLL.Interfaces;
using Solana.Web.Admin.Models.Requests.CNImport;
using Solana.Web.Admin.Models.Responses;
using Solana.Web.Admin.Models.Responses.CNImport;

namespace Solana.Web.Admin.API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class CNImportController : ControllerBase
    {
        private readonly ILogger<CNImportController> _logger;
        private readonly ICNImportLogic _importLogic;

        public CNImportController(ICNImportLogic importLogic, ILogger<CNImportController> logger)
        {
            _importLogic = importLogic;
            _logger = logger;
        }

        /// <summary>
        /// Returns the relative path to the version text file
        /// </summary>
        /// <returns></returns>
        [HttpGet("CurrentCNVersionFileName")]
        public ActionResult<string> GetCurrentCNVersionFileName()
        {
            _logger.LogInformation($"{nameof(CNImportController)}.{nameof(GetCurrentCNVersionFileName)}");
            //shouldn't here be any logic to get this from db or config?
            return "~/Content/CNData/_CNVersion.txt";
        }

        /// <summary>
        /// Returns available CN version from the version text file
        /// </summary>
        /// <param name="contentPath"></param>
        /// <returns></returns>
        [HttpPost("AvailableCNVersionFromContent")]
        public async Task<ActionResult<GetCNImportResponse>> GetAvailableCNVersionFromContent(string contentPath)
        {
            _logger.LogInformation($"{nameof(CNImportController)}.{nameof(GetAvailableCNVersionFromContent)} params: ({contentPath})");
            return await _importLogic.GetCNImportViewModel(contentPath);
        }

        /// <summary>
        /// Returns current CN version
        /// </summary>
        /// <returns></returns>
        [HttpGet("CurrentCNVersion")]
        public async Task<ActionResult<string>> GetCurrentCNVersion()
        {
            _logger.LogInformation($"{nameof(CNImportController)}.{nameof(GetCurrentCNVersion)}");
            return await _importLogic.GetCurrentCNVersion();
        }

        /// <summary>
        /// Returns started import job specification
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("RunImport")]
        public ActionResult PostRunImport(PostRunImportRequest request)
        {
            _logger.LogInformation($"{nameof(CNImportController)}.{nameof(PostRunImport)} params: ({JsonConvert.SerializeObject(request, Formatting.Indented)})");
            _importLogic.RunImportJob(request);
            return new EmptyResult();
        }

        /// <summary>
        /// Returns all import results
        /// </summary>
        /// <returns></returns>
        [HttpGet("ImportResults")]
        public async Task<ActionResult<GetMenCnResultsHeaderResponse>> GetImportResults()
        {
            _logger.LogInformation($"{nameof(CNImportController)}.{nameof(GetImportResults)}");
            return new GetMenCnResultsHeaderResponse
            {
                Items = await _importLogic.GetImportResultsHeader()
            };
        }

        /// <summary>
        /// Returns import details for the particular import
        /// </summary>
        /// <param name="menCnResultsHeaderID"></param>
        /// <returns></returns>
        [HttpGet("ImportDetails")]
        public async Task<ActionResult<GetMenCnResultsDetailResponse>> GetImportDetails(int menCnResultsHeaderID)
        {
            _logger.LogInformation($"{nameof(CNImportController)}.{nameof(GetImportDetails)} params: ({menCnResultsHeaderID})");
            return new GetMenCnResultsDetailResponse
            {
                Items = await _importLogic.GetImportResultsDetails(menCnResultsHeaderID)
            };
        }
    }
}
