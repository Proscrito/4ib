using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Solana.Web.Admin.BLL.Interfaces;
using Solana.Web.Admin.Models;
using Solana.Web.Admin.Models.Requests;
using Solana.Web.Admin.Models.Responses;

namespace Solana.Web.Admin.API.Controllers
{
    //TODO: add Authorize attribute for verifying JWT
    //[Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUsersLogic _usersLogic;

        public UsersController(IUsersLogic usersLogic)
        {
            _usersLogic = usersLogic;
        }

        [HttpGet("AdmUser")]
        public ActionResult<GetAdmUserResponse> GetAdmUser(string userName, bool isActive, bool isDeleted, bool allowLogin)
        {
            return _usersLogic.GetAdmUser(userName, isActive, isDeleted, allowLogin);
        }

        [HttpGet("AdmUserById")]
        public ActionResult<GetAdmUserByIdResponse> GetAdmUserById(int admUserID)
        {
            return _usersLogic.GetAdmUserById(admUserID);
        }

        /// <summary>
        /// Updates an AdmUser record, whichever AdmUserID is specified
        /// </summary>
        /// <param name="putAdmUserRequest">the AdmUser record to be updated</param>
        /// <returns>the updated AdmUser record</returns>
        [HttpPut("AdmUser")]
        public ActionResult<PutAdmUserResponse> PutAdmUser(PutAdmUserRequest putAdmUserRequest)
        {
            return _usersLogic.PutAdmUser(putAdmUserRequest);
        }

        /// <summary>
        /// Patches an AdmUser record, whichever AdmUserID is specified
        /// </summary>
        /// <param name="admUserID">the AdmUser's ID to be patched</param>
        /// <param name="jsonPatchDocument">the patch containing changes to be applied</param>
        /// <returns>the original and patched AdmUser records</returns>
        [HttpPatch("AdmUser/{admUserID}")]
        public ActionResult<PatchAdmUserResponse> PatchAdmUser(int admUserID, JsonPatchDocument<PatchAdmUser> jsonPatchDocument)
        {
            return _usersLogic.PatchAdmUser(admUserID, jsonPatchDocument);
        }

        /// <summary>
        /// Creates an AdmUserActivity record
        /// </summary>
        /// <param name="postAdmUsersActivityRequest">the AdmUserActivity record to be created</param>
        /// <returns>the created AdmUserActivity record</returns>
        [HttpPost("AdmUserActivity")]
        public ActionResult<PostAdmUsersActivityResponse> PostAdmUserActivity(PostAdmUsersActivityRequest postAdmUsersActivityRequest)
        {
            return _usersLogic.PostAdmUserActivity(postAdmUsersActivityRequest);
        }

        [HttpGet("AdmUserPreference")]
        public ActionResult<GetAdmUserPreferenceResponse> GetAdmUserPreference(int admUserID)
        {
            return _usersLogic.GetAdmUserPreference(admUserID);
        }
    }
}