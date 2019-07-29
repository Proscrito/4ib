using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Solana.Web.Admin.BLL.Interfaces;
using Solana.Web.Admin.Models.Requests.OnlineApplication;
using Solana.Web.Admin.Models.Responses.OnlineApplications;

namespace Solana.Web.Admin.API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class OnlineApplicationsController : ControllerBase
    {
        private readonly IOnlineApplicationsLogic _logic;
        private readonly ILogger<OnlineApplicationsController> _logger;

        public OnlineApplicationsController(IOnlineApplicationsLogic logic, ILogger<OnlineApplicationsController> logger)
        {
            _logic = logic;
            _logger = logger;
        }

        //old controller: Index()
        [HttpGet("AdmFroOptions")]
        public async Task<ActionResult<GetAdmFroOptionsResponse>> GetAdmFroOptions()
        {
            _logger.LogInformation($"{nameof(OnlineApplicationsController)}.{nameof(GetAdmFroOptions)}");
            return await _logic.GetAdmFroOptions();
        }

        [HttpPut]
        public async Task<ActionResult> PutAdmFroOptions(PutAdmFroOptionsRequest request)
        {
            _logger.LogInformation($"{nameof(OnlineApplicationsController)}.{nameof(PutAdmFroOptions)} params: ({JsonConvert.SerializeObject(request, Formatting.Indented)})");
            await _logic.SaveAdmFroOptions(request);
            return Ok();
        }
    }
}
