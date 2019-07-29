using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Solana.Web.Admin.BLL.Interfaces;
using Solana.Web.Admin.Models.Requests.P2ClaimingPercentage;
using Solana.Web.Admin.Models.Responses.P2ClaimingPercentage;

namespace Solana.Web.Admin.API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class P2ClaimingPercentageController : ControllerBase
    {
        private readonly IP2ClaimingPercentageLogic _logic;
        private readonly ILogger<P2ClaimingPercentageController> _logger;

        public P2ClaimingPercentageController(IP2ClaimingPercentageLogic logic, ILogger<P2ClaimingPercentageController> logger)
        {
            _logic = logic;
            _logger = logger;
        }

        //old controller: ReadData
        [HttpGet("AccP2Rates")]
        public async Task<ActionResult<GetAccP2RatesResponse>> GetAccP2Rates()
        {
            _logger.LogInformation($"{nameof(P2ClaimingPercentageController)}.{nameof(GetAccP2Rates)}");
            return new GetAccP2RatesResponse
            {
                Items = await _logic.GetAccP2Rates()
            };
        }

        //old controller: SaveP2ClaimRate
        [HttpPut("AccP2Rates")]
        public async Task<ActionResult<PutAccP2RatesResponse>> PutAccP2Rates(PutAccP2RatesRequest request)
        {
            _logger.LogInformation($"{nameof(P2ClaimingPercentageController)}.{nameof(PutAccP2Rates)} params: ({JsonConvert.SerializeObject(request, Formatting.Indented)})");
            return new PutAccP2RatesResponse
            {
                IdCollection = await _logic.SaveAccP2Rates(request.Items)
            };
        }
    }
}
