using AutoMapper;
using Horizon.Common.Repository.Legacy;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Horizon.Common.Repository.Legacy.Models.Inv;
using Solana.Web.Admin.BLL.Interfaces;
using Solana.Web.Admin.Models.Requests.SiteTypes;
using Solana.Web.Admin.Models.Responses.SiteTypes;
using Solana.Web.Admin.Models.Responses.SiteTypes.NestedModels;

namespace Solana.Web.Admin.BLL
{
    public class SiteTypesLogic: ISiteTypesLogic
    {
        private readonly ISolanaRepository _repo;
        private readonly IMapper _autoMapper;

        public SiteTypesLogic(ISolanaRepository repo, IMapper autoMapper)
        {
            _repo = repo;
            _autoMapper = autoMapper;
        }

        public async Task<ReadSiteTypesModelResponse> Read()
        {
            var invSiteType = await _repo.GetListAsync<InvSiteType>();
            var invSiteTypeModels = _autoMapper.Map<List<SiteTypeModel>>(invSiteType);
            return new ReadSiteTypesModelResponse {Items = invSiteTypeModels};
        }

        public async Task<int> Create(CreateSiteTypeRequest request)
        {
            var invSiteType = _autoMapper.Map<InvSiteType>(request);
            await _repo.CreateAsync(invSiteType);
            return invSiteType.InvSiteTypeID;
        }

        public async Task<int> Update(UpdateSiteTypeRequest request)
        {
            var invSiteType = _autoMapper.Map<InvSiteType>(request);
            await _repo.UpdateAsync(invSiteType);
            return invSiteType.InvSiteTypeID;
        }

        public async Task<int> Delete(DeleteSiteTypeRequest request)
        {
            var invSiteType = _autoMapper.Map<InvSiteType>(request);
            var resId = invSiteType.InvSiteTypeID;
            await _repo.DeleteAsync(invSiteType);
            return resId;
        }

        public async Task<List<KeyValuePair<string, string>>> Validate(CreateSiteTypeRequest request)
        {
            var res = new List<KeyValuePair<string, string>>();

            var modelWithDescription = await _repo.GetListAsync<InvSiteType>(t => t.Description == request.Description);
            if (modelWithDescription.Any())
            {
                res.Add(new KeyValuePair<string, string>("Description", "Duplicate Description"));
            }

            return res;
        }
    }
}
