using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Solana.Web.Admin.BLL.Interfaces;
using Solana.Web.Admin.Models.Requests.Schools;
using Solana.Web.Admin.Models.Requests.Schools.NestedModels;

namespace Solana.Web.Admin.API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolController : ControllerBase
    {
        private readonly ISchoolLogic _logic;

        public SchoolController(ISchoolLogic logic)
        {
            _logic = logic;
        }

        /// <summary>
        /// Index
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut] //OLD name Index
        public ActionResult PutAppViewsAdmUsersPreferences(PutAppViewsAdmUsersPreferencesRequest request)
        {
            _logic.AppViewsAdmUsersPreferencesInit(request.AdmUserId, request.Id);

            return Ok();
        }

        /// <summary>
        /// ParentManagementLevel
        /// </summary>
        /// <param name="parentSiteId"></param>
        /// <returns></returns>
        [HttpGet("ParentManagementLevel")]
        public async Task<ActionResult<int>> GetParentManagementLevel(int parentSiteId)
        {
            return await _logic.GetParentLevel(parentSiteId);
        }

        /// <summary>
        /// DupSiteIDs
        /// </summary>
        /// <param name="siteId"></param>
        /// <param name="admSiteId"></param>
        /// <returns></returns>
        [HttpGet("DupSiteIDs")]
        public async Task<ActionResult<int>> GetDupSiteIDs(string siteId, int admSiteId)
        {
            return await _logic.GetSiteIdInUse(siteId, admSiteId);
        }

        /// <summary>
        /// DupSiteNames
        /// </summary>
        /// <param name="siteName"></param>
        /// <param name="admSiteId"></param>
        /// <returns></returns>
        [HttpGet("DupSiteNames")]
        public async Task<ActionResult<int>> GetDupSiteNames(string siteName, int admSiteId)
        {
            return await _logic.GetSiteNameInUse(siteName, admSiteId);
        }

        /// <summary>
        /// GetStateInfo
        /// </summary>
        /// <param name="admSiteId"></param>
        /// <returns></returns>
        [HttpGet("GetStateInfo")]
        public async Task<ActionResult<string>> GetStateInfo(int admSiteId)
        {
            return await _logic.GetStateInfo(admSiteId);
        }

        /// <summary>
        /// PutSchoolsInformation
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("SchoolsInformation")]
        public async Task<ActionResult> PutSchoolsInformation(PutSchoolsInformationRequest model) //OLD SaveSchoolsInformation
        {
            var validationResult = await _logic.ValidateSchoolData(model);
            foreach (var validation in validationResult)
            {
                ModelState.AddModelError(validation.Key, validation.Value);
            }

            if (ModelState.IsValid)
            {
                await _logic.SaveSchoolsInformation(model);
                return Ok();
            }

            return BadRequest(ModelState);
        }

        /// <summary>
        /// GradeTransferList
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GradeTransferList")]
        public async Task<ActionResult<List<GradeTransfer>>> GetGradeTransferList(int id)
        {
            return await _logic.GetGradeTransferList(id);
        }

        /// <summary>
        /// OtherServingSiteList
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("OtherServingSiteList")]
        public async Task<ActionResult<List<ServingSites>>> GetOtherServingSiteList(int id)
        {
            return await _logic.GetServingSite(id);
        }

        /// <summary>
        /// SchoolTypes
        /// </summary>
        /// <returns></returns>
        [HttpGet("SchoolTypes")]
        public async Task<ActionResult<List<SchoolTypesViewModel>>> GetSchoolTypes()
        {
            return await _logic.GetSchoolTypes();
        }

        /// <summary>
        /// SchoolNames
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        [HttpGet("SchoolNames")]
        public async Task<ActionResult<List<AdmSiteModel>>> GetSchoolNames(string searchValue)
        {
            return await _logic.GetSchoolNames(searchValue);
        }

        /// <summary>
        /// SchoolSiteIDs
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        [HttpGet("SchoolSiteIDs")]
        public async Task<ActionResult<List<AdmSiteModel>>> GetSchoolSiteIDs(string searchValue)
        {
            return await _logic.GetSchoolSiteIDs(searchValue);
        }

        /// <summary>
        /// PostMissingSchoolCalendar
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("MissingSchoolCalendar")]
        public async Task<ActionResult<int>> PostMissingSchoolCalendar(PostMissingSchoolCalendarRequest request) //OLD CreateMissingSchoolCalendar
        {
            return await _logic.CreateMissingSchoolCalendar(request);
        }

        /// <summary>
        /// CopySiteCalendar
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("SiteCalendar")]
        public async Task<ActionResult<int>> PutSiteCalendar(PutSiteCalendarRequest request) //OLD CopySiteCalendar
        {
            return await _logic.CopySiteCalendar(request);
        }
    }
}
