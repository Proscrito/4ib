using Microsoft.AspNetCore.JsonPatch;
using Solana.Web.Admin.Models;
using Solana.Web.Admin.Models.Requests;
using Solana.Web.Admin.Models.Responses;

namespace Solana.Web.Admin.BLL.Interfaces
{
    public interface IUsersLogic
    {
        GetAdmUserResponse GetAdmUser(string userName, bool isActive, bool isDeleted, bool allowLogin);
        GetAdmUserByIdResponse GetAdmUserById(int admUserID);
        GetAdmUserPreferenceResponse GetAdmUserPreference(int admUserID);
        PatchAdmUserResponse PatchAdmUser(int admUserID, JsonPatchDocument<PatchAdmUser> jsonPatchDocument);
        PostAdmUsersActivityResponse PostAdmUserActivity(PostAdmUsersActivityRequest postAdmUsersActivityRequest);
        PutAdmUserResponse PutAdmUser(PutAdmUserRequest putAdmUserRequest);
    }
}
