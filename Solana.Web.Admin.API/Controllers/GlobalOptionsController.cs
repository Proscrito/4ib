using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Solana.Web.Admin.BLL.Interfaces;
using Solana.Web.Admin.Models.Requests.GlobalOptions;
using Solana.Web.Admin.Models.Responses.GlobalOptions;

namespace Solana.Web.Admin.API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class GlobalOptionsController : ControllerBase
    {
        private readonly IGlobalOptionsLogic _globalOptionsLogic;
        private readonly ILogger<GlobalOptionsController> _logger;

        public GlobalOptionsController(IGlobalOptionsLogic globalOptionsLogic, ILogger<GlobalOptionsController> logger)
        {
            _globalOptionsLogic = globalOptionsLogic;
            _logger = logger;
        }

        //For Index()
        [HttpGet("")]
        public async Task<ActionResult<GetAdmGlobalOptionsResponse>> GetAdmGlobalOptions()
        {
            _logger.LogInformation($"{nameof(GlobalOptionsController)} - {nameof(GetAdmGlobalOptions)}");
            return await _globalOptionsLogic.GetAdmGlobalOptions();
        }

        [HttpPut("")]
        public async Task<ActionResult<PutAdmGlobalOptionsResponse>> PutAdmGlobalOptions(PutAdmGlobalOptionsRequest request)
        {
            _logger.LogInformation($"{nameof(GlobalOptionsController)} - {nameof(PutAdmGlobalOptions)}");
            return await _globalOptionsLogic.SaveAdmGlobalOption(request);
        }

        [HttpGet("MenAgeGroups")]
        public async Task<ActionResult<GetAgeGroupsResponse>> GetMenAgeGroups()
        {
            _logger.LogInformation($"{nameof(GlobalOptionsController)} - {nameof(GetMenAgeGroups)}");
            return new GetAgeGroupsResponse
            {
                Items = await _globalOptionsLogic.GetMenAgeGroups()
            };
        }

        [HttpGet("IsGenerateAppNumbers")]
        public async Task<ActionResult<bool>> GetIsGenerateAppNumbers()
        {
            _logger.LogInformation($"{nameof(GlobalOptionsController)} - {nameof(GetIsGenerateAppNumbers)}");
            return await _globalOptionsLogic.GetIsGenerateAppNumbers();
        }

        [HttpGet("PosReasons")]
        public async Task<ActionResult<GetPosReasonsResponse>> GetPosReasons()
        {
            _logger.LogInformation($"{nameof(GlobalOptionsController)} - {nameof(GetPosReasons)}");
            return new GetPosReasonsResponse
            {
                Items = await _globalOptionsLogic.GetPosReasons()
            };
        }

        [HttpDelete("PosReason")]
        public async Task<ActionResult> DeletePosReason(int id)
        {
            _logger.LogInformation($"{nameof(GlobalOptionsController)} - {nameof(DeletePosReason)}");
            await _globalOptionsLogic.DeletePosReason(id);
            return new EmptyResult();
        }

        //IsUniqueReason in the old code, but naming is inaccurate
        [HttpGet("IsReasonExist")]
        public async Task<ActionResult<bool>> GetIsReasonExist(string reason)
        {
            _logger.LogInformation($"{nameof(GlobalOptionsController)} - {nameof(GetIsReasonExist)}");
            return await _globalOptionsLogic.GetIsReasonExist(reason);
        }

        [HttpPut("PosReason")]
        public async Task<ActionResult<int>> PutPosReason(string reason)
        {
            _logger.LogInformation($"{nameof(GlobalOptionsController)} - {nameof(PutPosReason)}");
            return await _globalOptionsLogic.SavePosReason(reason);
        }

        [HttpGet("MenLeftoverCodes")]
        public async Task<ActionResult<GetMenLeftoverCodesResponse>> GetMenLeftoverCodes()
        {
            _logger.LogInformation($"{nameof(GlobalOptionsController)} - {nameof(GetMenLeftoverCodes)}");
            return new GetMenLeftoverCodesResponse
            {
                Items = await _globalOptionsLogic.GetMenLeftoverCodes()
            };
        }

        [HttpGet("AdjustmentReasons")]
        public async Task<ActionResult<GetInvTransactionTypesResponse>> GetInvTransactionTypes()
        {
            _logger.LogInformation($"{nameof(GlobalOptionsController)} - {nameof(GetInvTransactionTypes)}");
            return new GetInvTransactionTypesResponse
            {
                Items = await _globalOptionsLogic.GetInvTransactionTypes()
            };
        }

        [HttpDelete("MenLeftoverCode")]
        public async Task<ActionResult> DeleteMenLeftoverCode(int id)
        {
            _logger.LogInformation($"{nameof(GlobalOptionsController)} - {nameof(DeleteMenLeftoverCode)}");
            await _globalOptionsLogic.DeleteMenLeftoverCode(id);
            return new EmptyResult();
        }

        [HttpPut("MenLeftoverCodes")]
        public async Task<ActionResult<PutMenLeftoverCodesResponse>> PutMenLeftoverCodes(PutMenLeftoverCodesRequest request)
        {
            _logger.LogInformation($"{nameof(GlobalOptionsController)} - {nameof(PutMenLeftoverCodes)}");
            return new PutMenLeftoverCodesResponse
            {
                IdCollection = await _globalOptionsLogic.SaveMenLeftoverCodes(request.Items)
            };
        }

        [HttpPut("InvTransactionTypes")]
        public async Task<ActionResult<PutInvTransactionTypesResponse>> PutInvTransactionTypes(PutInvTransactionTypesRequest request)
        {
            _logger.LogInformation($"{nameof(GlobalOptionsController)} - {nameof(PutInvTransactionTypes)}");
            return new PutInvTransactionTypesResponse
            {
                IdCollection = await _globalOptionsLogic.SaveInvTransactionTypes(request.Items)
            };
        }

        [HttpDelete("InvTransactionType")]
        public async Task<ActionResult> DeleteInvTransactionType(int id)
        {
            _logger.LogInformation($"{nameof(GlobalOptionsController)} - {nameof(DeleteInvTransactionType)}");
            await _globalOptionsLogic.DeleteInvTransactionType(id);
            return new EmptyResult();
        }
    }
}
