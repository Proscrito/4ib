using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Solana.Web.Admin.BLL.Interfaces;
using Solana.Web.Admin.Models.Requests.IntegrationMaps;
using Solana.Web.Admin.Models.Responses.IntegrationMaps;

namespace Solana.Web.Admin.API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class IntegrationMapsController : ControllerBase
    {
        private readonly ILogger<CNImportController> _logger;
        private readonly IIntegrationMapsLogic _integrationMapsLogic;

        public IntegrationMapsController(IIntegrationMapsLogic integrationMapsLogic, ILogger<CNImportController> logger)
        {
            _integrationMapsLogic = integrationMapsLogic;
            _logger = logger;
        }

        //GetIntegrationMapTypes - no database queries, will stay on MVC side. Or, most likely, will gone at all.

        //for GetTranslations and ReadTranslations
        [HttpGet("Translations")]
        public ActionResult<GetTranslationsResponse> GetTranslations()
        {
            _logger.LogInformation($"{nameof(IntegrationMapsController)}.{nameof(GetTranslations)}");
            return new GetTranslationsResponse
            {
                Items = _integrationMapsLogic.GetTranslations()
            };
        }

        //save details
        [HttpPut("IntegrationMap")]
        public async Task<ActionResult<PutIntegrationMapsResponse>> PutIntegrationMap(PutIntegrationMapsRequest request)
        {
            _logger.LogInformation($"{nameof(IntegrationMapsController)}.{nameof(PutIntegrationMap)} params: ({JsonConvert.SerializeObject(request, Formatting.Indented)})");
            return await _integrationMapsLogic.SaveIntegrationMap(request);
        }

        [HttpGet("")]
        public async Task<ActionResult<GetIntegrationMapResponse>> GetIntegrationMap([FromQuery]GetIntegrationMapRequest request)
        {
            _logger.LogInformation($"{nameof(IntegrationMapsController)}.{nameof(GetIntegrationMap)} params: ({JsonConvert.SerializeObject(request, Formatting.Indented)})");
            return await _integrationMapsLogic.GetIntegrationMap(request);
        }

        //MapGrid_Read
        [HttpGet("List")]
        public async Task<ActionResult<GetIntegrationMapsResponse>> GetIntegrationMaps()
        {
            _logger.LogInformation($"{nameof(IntegrationMapsController)}.{nameof(GetIntegrationMaps)}");
            return new GetIntegrationMapsResponse
            {
                Items = await _integrationMapsLogic.GetIntegrationMaps()
            };
        }

        //MapGrid_Delete
        [HttpDelete("")]
        public async Task<ActionResult<GetIntegrationMapResponse>> DeleteIntegrationMap(int id)
        {
            _logger.LogInformation($"{nameof(IntegrationMapsController)}.{nameof(DeleteIntegrationMap)} params: ({id})");
            return await _integrationMapsLogic.DeleteIntegrationMap(id);
        }

        [HttpPut("IntegrationMapColumns")]
        public async Task<ActionResult> PutIntegrationMapColumns(PutIntegrationMapColumnsRequest request)
        {
            _logger.LogInformation($"{nameof(IntegrationMapsController)}.{nameof(PutIntegrationMapColumns)} params: ({JsonConvert.SerializeObject(request, Formatting.Indented)})");
            await _integrationMapsLogic.SaveIntegrationMapColumns(request);
            return new EmptyResult();
        }

        //Columns_Read
        [HttpGet("IntegrationMapColumns")]
        public async Task<ActionResult<GetIntegrationMapColumnsResponse>> GetIntegrationMapColumns(int mapId)
        {
            _logger.LogInformation($"{nameof(IntegrationMapsController)}.{nameof(GetIntegrationMapColumns)} params: ({mapId})");
            return new GetIntegrationMapColumnsResponse
            {
                Items = await _integrationMapsLogic.GetIntegrationMapColumns(mapId)
            };
        }

        [HttpPut("AppTriageFile")]
        public async Task<ActionResult<int>> PutAppTriageFile(PutAppTriageFileRequest request)
        {
            _logger.LogInformation($"{nameof(IntegrationMapsController)}.{nameof(PutAppTriageFile)} params: ({JsonConvert.SerializeObject(request, Formatting.Indented)})");
            return await _integrationMapsLogic.SaveAppTriageFile(request);
        }

        [HttpDelete("AppTriageFile")]
        public async Task<ActionResult> DeleteAppTriageFile(int fileId)
        {
            _logger.LogInformation($"{nameof(IntegrationMapsController)}.{nameof(GetIntegrationMapColumns)} params: ({fileId})");
            await _integrationMapsLogic.DeleteAppTriageFile(fileId);
            return new EmptyResult();
        }
    }
}
