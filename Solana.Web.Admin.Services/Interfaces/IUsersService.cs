using Horizon.Common.Models.Requests;
using Horizon.Common.Models.Responses;

namespace Solana.Web.Admin.Services.Interfaces
{
    public interface IUsersService
    {
        GetAdmUserResponse GetAdmUser(string userName, bool isActive, bool isDeleted, bool allowLogin);
        GetAdmUserPreferenceResponse GetAdmUserPreference(int admUserID);
        PostAdmUsersActivityResponse PostAdmUserActivity(PostAdmUsersActivityRequest postAdmUsersActivityRequest);
        PutAdmUserResponse PutAdmUser(PutAdmUserRequest putAdmUserRequest);
    }
}
