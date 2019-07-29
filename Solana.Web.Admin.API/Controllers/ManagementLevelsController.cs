using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Solana.Web.Admin.BLL.Interfaces;
using Solana.Web.Admin.Models.Requests.ManagementLevels;
using Solana.Web.Admin.Models.Responses.ManagementLevels;

namespace Solana.Web.Admin.API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ManagementLevelsController : ControllerBase
    {
        private readonly IManagementLevelsLogic _logic;
        private readonly ILogger<ManagementLevelsController> _logger;

        public ManagementLevelsController(IManagementLevelsLogic logic, ILogger<ManagementLevelsController> logger)
        {
            _logic = logic;
            _logger = logger;
        }

        //old controller: Index()
        [HttpGet("List")]
        public async Task<ActionResult<GetAdmManagementLevelsResult>> GetAdmManagementLevels()
        {
            _logger.LogInformation($"{nameof(ManagementLevelsController)}.{nameof(GetAdmManagementLevels)}");
            return new GetAdmManagementLevelsResult
            {
                Items = await _logic.GetAdmManagementLevels()
            };
        }

        //old controller: UpdateManagementLevels
        [HttpPut("List")]
        public async Task<ActionResult> PutAdmManagementLevels(PutAdmManagementLevelsRequest request)
        {
            _logger.LogInformation($"{nameof(ManagementLevelsController)}.{nameof(PutAdmManagementLevels)} params: ({JsonConvert.SerializeObject(request, Formatting.Indented)})");
            await _logic.SaveAdmManagementLevels(request.Items);
            return Ok();
        }
    }
}
