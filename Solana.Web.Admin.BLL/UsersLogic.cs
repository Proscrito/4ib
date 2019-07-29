using AutoMapper;
using Horizon.Common.Repository.Legacy;
using Horizon.Common.Repository.Legacy.Models.Adm;
using Microsoft.AspNetCore.JsonPatch;
using Solana.Web.Admin.BLL.Interfaces;
using Solana.Web.Admin.Models;
using Solana.Web.Admin.Models.Requests;
using Solana.Web.Admin.Models.Responses;

namespace Solana.Web.Admin.BLL
{
    public class UsersLogic : IUsersLogic
    {
        private readonly ISolanaRepository _repo;
        private readonly IMapper _autoMapper;

        public UsersLogic(ISolanaRepository repo, IMapper autoMapper)
        {
            _repo = repo;
            _autoMapper = autoMapper;
        }

        public GetAdmUserResponse GetAdmUser(string userName, bool isActive, bool isDeleted, bool allowLogin)
        {
            var admUser = _repo.Get<AdmUser>(u => u.UserLogin != null && u.UserLogin == userName && u.Active == isActive && u.IsDeleted == isDeleted && u.AllowLogin == allowLogin);

            //map the data model to the API response model
            return _autoMapper.Map<GetAdmUserResponse>(admUser);
        }

        public GetAdmUserByIdResponse GetAdmUserById(int admUserID)
        {
            var admUser = _repo.Get<AdmUser>(u => u.AdmUserID == admUserID);

            //map the data model to the API response model
            return _autoMapper.Map<GetAdmUserByIdResponse>(admUser);
        }

        public GetAdmUserPreferenceResponse GetAdmUserPreference(int admUserID)
        {
            var admUserPreference = _repo.Get<AdmUserPreference>(p => p.AdmUserID == admUserID);

            //map the data model to the API response model
            return _autoMapper.Map<GetAdmUserPreferenceResponse>(admUserPreference);
        }

        public PatchAdmUserResponse PatchAdmUser(int admUserID, JsonPatchDocument<PatchAdmUser> jsonPatchDocument)
        {
            var response = new PatchAdmUserResponse();

            var admUser = _repo.Find<AdmUser>(admUserID); //get the original database object
            response.Original = _autoMapper.Map<PatchAdmUser>(admUser); //map database object to business object

            var patched = _autoMapper.Map<PatchAdmUser>(admUser); 
            jsonPatchDocument.ApplyTo(patched); //apply the patch
            _autoMapper.Map(patched, admUser); //map the business object back to the database object

            _repo.Update(admUser); //apply the changes to the database
            response.Patched = patched;

            return response;
        }

        public PostAdmUsersActivityResponse PostAdmUserActivity(PostAdmUsersActivityRequest postAdmUsersActivityRequest)
        {
            var admUsersActivity = _autoMapper.Map<AdmUsersActivity>(postAdmUsersActivityRequest); //map the business object/DTO to the data model
            var createdAdmUsersActivity = _repo.Create(admUsersActivity);

            return _autoMapper.Map<PostAdmUsersActivityResponse>(createdAdmUsersActivity);
        }

        public PutAdmUserResponse PutAdmUser(PutAdmUserRequest putAdmUserRequest)
        {
            throw new System.NotImplementedException();
        }
    }
}
