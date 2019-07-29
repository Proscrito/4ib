using Horizon.Common.Models.Requests;
using Horizon.Common.Models.Responses;
using Solana.Web.Admin.BLL.Interfaces;
using Solana.Web.Admin.Services.Interfaces;

namespace Solana.Web.Admin.Services
{
    public class UsersService: IUsersService
    {
        private IUsersLogic _usersLogic;

        public UsersService(IUsersLogic usersLogic)
        {
            _usersLogic = usersLogic;
        }

        public GetAdmUserResponse GetAdmUser(string userName, bool isActive, bool isDeleted, bool allowLogin)
        {
            return _usersLogic.GetAdmUser(userName, isActive, isDeleted, allowLogin);
        }

        public GetAdmUserPreferenceResponse GetAdmUserPreference(int admUserID)
        {
            return _usersLogic.GetAdmUserPreference(admUserID);
        }

        public PostAdmUsersActivityResponse PostAdmUserActivity(PostAdmUsersActivityRequest postAdmUsersActivityRequest)
        {
            return _usersLogic.PostAdmUserActivity(postAdmUsersActivityRequest);
        }

        public PutAdmUserResponse PutAdmUser(PutAdmUserRequest putAdmUserRequest)
        {
            return _usersLogic.PutAdmUser(putAdmUserRequest);
        }
    }
}
