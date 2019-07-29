using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Solana.Web.Admin.BLL.Interfaces;
using Solana.Web.Admin.Models.Requests.CEPClaimingPercentage;
using Solana.Web.Admin.Models.Responses.CEPClaimingPercentage;

namespace Solana.Web.Admin.API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class CEPClaimingPercentageController : ControllerBase
    {
        private readonly ILogger<CEPClaimingPercentageController> _logger;
        private readonly ICEPClaimingPercentageLogic _cepClaimingPercentageLogic;

        public CEPClaimingPercentageController(ICEPClaimingPercentageLogic cepClaimingPercentageLogic, ILogger<CEPClaimingPercentageController> logger)
        {
            _logger = logger;
            _cepClaimingPercentageLogic = cepClaimingPercentageLogic;
        }
        /// <summary>
        /// Save CEPClaimRates
        /// </summary>
        /// <param name="cepClaimRatesRequest"></param>
        /// <returns></returns>
        [HttpPut("CEPClaimRate")]
        public async Task<ActionResult<int>> PutCEPClaimRate(PutCEPClaimRatesRequest cepClaimRatesRequest)
        {
            _logger.LogInformation($"{nameof(CEPClaimingPercentageController)}.{nameof(PutCEPClaimRate)} params: ({JsonConvert.SerializeObject(cepClaimRatesRequest, Formatting.Indented)})");
            return await _cepClaimingPercentageLogic.SaveCEPClaimRates(cepClaimRatesRequest);
        }
        
        /// <summary>
        /// Returns AdmSitesCEPClaimingPercent list
        /// Legacy: ReadData()
        /// </summary>
        /// <param name="admUserID"></param>
        /// <returns></returns>
        [HttpGet("AdmSitesCEPClaimingPercent")]
        public async Task<ActionResult<GetAdmSitesCEPClaimingPercentResponse>> GetAdmSitesCEPClaimingPercent(int admUserID)
        {
            _logger.LogInformation($"{nameof(CEPClaimingPercentageController)}.{nameof(GetAdmSitesCEPClaimingPercent)} params: ({admUserID})");
            return new GetAdmSitesCEPClaimingPercentResponse
            {
                Items = await _cepClaimingPercentageLogic.GetAdmSitesCEPClaimingPercent(admUserID)
            };
        }
    }
}
