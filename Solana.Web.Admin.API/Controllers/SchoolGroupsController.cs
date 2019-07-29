using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Solana.Web.Admin.BLL.Interfaces;
using Solana.Web.Admin.Models.Requests.SchoolGroups;
using Solana.Web.Admin.Models.Responses.SchoolGroups;

namespace Solana.Web.Admin.API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolGroupsController : ControllerBase
    {
        private readonly ILogger<SchoolGroupsController> _logger;
        private readonly ISchoolGroupsLogic _logic;

        public SchoolGroupsController(ISchoolGroupsLogic logic, ILogger<SchoolGroupsController> logger)
        {
            _logic = logic;
            _logger = logger;
        }

        //old controller: Details
        [HttpGet("")]
        public async Task<ActionResult<GetAdmSchoolGroupResponse>> GetAdmSchoolGroup(int id)
        {
            _logger.LogInformation($"{nameof(SchoolGroupsController)}.{nameof(GetAdmSchoolGroup)} params: ({id})");
            return await _logic.GetAdmSchoolGroup(id);
        }

        //old controller: Delete
        [HttpDelete("")]
        public async Task<ActionResult> DeleteAdmSchoolGroup(int id)
        {
            _logger.LogInformation($"{nameof(SchoolGroupsController)}.{nameof(GetAdmSchoolGroup)} params: ({id})");
            await _logic.DeleteAdmSchoolGroup(id);
            return Ok();
        }

        //old controller: Read
        [HttpGet("List")]
        public async Task<ActionResult<GetAdmSchoolGroupsResponse>> GetAdmSchoolGroups()
        {
            _logger.LogInformation($"{nameof(SchoolGroupsController)} - {nameof(GetAdmSchoolGroups)}");
            return new GetAdmSchoolGroupsResponse
            {
                Items = await _logic.GetAdmSchoolGroups()
            };
        }

        //old controller: GetSchools
        [HttpGet("AdmSchoolGroupSites")]
        public async Task<ActionResult<GetAdmSchoolGroupSitesResponse>> GetAdmSchoolGroupSites([FromQuery]GetAdmSchoolGroupSiteRequest request)
        {
            _logger.LogInformation($"{nameof(SchoolGroupsController)}.{nameof(GetAdmSchoolGroupSites)} params: ({JsonConvert.SerializeObject(request, Formatting.Indented)})");
            return new GetAdmSchoolGroupSitesResponse
            {
                Items = await _logic.GetAdmSchoolGroupSites(request)
            };
        }

        //old controller: IsUniqueGroupName
        [HttpGet("IsUniqueGroupName")]
        public async Task<ActionResult<bool>> GetIsUniqueGroupName(GetIsUniqueGroupNameRequest request)
        {
            _logger.LogInformation($"{nameof(SchoolGroupsController)}.{nameof(GetIsUniqueGroupName)} params: ({JsonConvert.SerializeObject(request, Formatting.Indented)})");
            return await _logic.IsUniqueGroupName(request);
        }

        //old controller: Save
        [HttpPut("")]
        public async Task<ActionResult<int>> PutAdmSchoolGroup(PutAdmSchoolGroupRequest request)
        {
            _logger.LogInformation($"{nameof(SchoolGroupsController)}.{nameof(PutAdmSchoolGroup)} params: ({JsonConvert.SerializeObject(request, Formatting.Indented)})");
            return await _logic.SaveAdmSchoolGroup(request);
        }
    }
}
