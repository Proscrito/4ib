using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Horizon.Common.Repository.Legacy;
using Horizon.Common.Repository.Legacy.Models.Acc;
using Horizon.Common.Repository.Legacy.Models.Adm;
using Horizon.Common.Repository.Legacy.Models.Common;
using Solana.Web.Admin.BLL.Interfaces;
using Solana.Web.Admin.Models.Requests.SchoolGroups;
using Solana.Web.Admin.Models.Responses.SchoolGroups;
using Solana.Web.Admin.Models.Responses.SchoolGroups.NestedModels;

namespace Solana.Web.Admin.BLL
{
    public class SchoolGroupsLogic : ISchoolGroupsLogic
    {
        private readonly ISolanaRepository _repository;
        private readonly IMapper _autoMapper;

        public SchoolGroupsLogic(ISolanaRepository repository, IMapper autoMapper)
        {
            _repository = repository;
            _autoMapper = autoMapper;
        }

        public async Task<List<AdmSchoolGroupViewModel>> GetAdmSchoolGroups()
        {
            var admSchoolGroups = await _repository.GetListAsync<AdmSchoolGroup>();
            return _autoMapper.Map<List<AdmSchoolGroupViewModel>>(admSchoolGroups);
        }

        public async Task<List<AdmSchoolGroupSiteViewModel>> GetAdmSchoolGroupSites(GetAdmSchoolGroupSiteRequest request)
        {
            IList<AdmSite> admSites;

            //I know it looks much shorter than in old site code, but it works in the same way, I tested it on the real data
            //it always returns the same set as old code
            if (request.IsCep)
            {
                admSites = await _repository.GetListAsync<AdmSite>(x => x.IsActive && x.AdmSchoolGroups.All(y => !y.IsCEPGroup));
            }
            else
            {
                admSites = await _repository.GetListAsync<AdmSite>(x => x.IsActive);
            }

            //using automapper here will be tripple more code, because we need to make a profile and then use additionally parametrized Map<> function
            return admSites.Select(x => new AdmSchoolGroupSiteViewModel
            {
                AdmSiteID = x.AdmSiteID,
                SchoolID = x.SiteID,
                SchoolName = x.SiteDescription,
                //due to the repository limitation we will have 1 additional db request per AdmSite here
                //we should have smarter repository to avoid this
                //in old site there are even more requests however, even using the IQueriable ...
                IsSelected = x.AdmSchoolGroups.Any(y => y.AdmSchoolGroupID == request.Id),
                IsCEPSchool = request.IsCep
            }).ToList();
        }

        public async Task<GetAdmSchoolGroupResponse> GetAdmSchoolGroup(int id)
        {
            var admSchoolGroup = await _repository.FindAsync<AdmSchoolGroup>(id);
            var admGlobalOption = await _repository.GetAsync<AdmGlobalOption>(x => true); //should be only one
            var result = _autoMapper.Map<GetAdmSchoolGroupResponse>(admSchoolGroup);

            result.ShowCEP = admGlobalOption.CEPSchoolOption == CEPSchoolOption.SchoolGroups;

            return result;
        }

        public async Task DeleteAdmSchoolGroup(int id)
        {
            var group = await _repository.FindAsync<AdmSchoolGroup>(id);

            if (group != null)
            {
                if (group.IsCEPGroup)
                {
                    //this looks weird to me, there is an FK in the DB which prevents group deletion if any AccRate is linked...
                    var accCEPRates = await _repository.GetListAsync<AccCEPRates>(x => x.AdmSchoolGroupID == id);

                    if (accCEPRates.Any())
                    {
                        var accCEPRate = accCEPRates.FirstOrDefault();
                        await _repository.DeleteAsync(accCEPRate);
                    }
                }

                await _repository.DeleteAsync(group);
                _repository.SaveChanges();
            }
        }

        public async Task<bool> IsUniqueGroupName(GetIsUniqueGroupNameRequest request)
        {
            //MS SQL is not case sensitive and we already Trim on save
            var group = await _repository.GetAsync<AdmSchoolGroup>(x => x.AdmSchoolGroupID != request.Id && x.Name == request.Name.Trim());

            return group == null;
        }

        public async Task<int> SaveAdmSchoolGroup(PutAdmSchoolGroupRequest request)
        {
            await Validate(request);
            AdmSchoolGroup admSchoolGroup;

            if (request.AdmSchoolGroupID == 0)
            {
                admSchoolGroup = await CreateAdmSchoolGroup(request);
            }
            else
            {
                admSchoolGroup = await UpdateAdmSchoolGroup(request);
            }
            
            _repository.SaveChanges();

            if (request.IsCEPGroup)
            {
                await PopulateAccCEPRates(admSchoolGroup.AdmSchoolGroupID);
            }

            return admSchoolGroup.AdmSchoolGroupID;
        }

        private async Task PopulateAccCEPRates(int admSchoolGroupID)
        {
            var createPct = await _repository.GetAsync<AccCEPRates>(x => x.AdmSchoolGroupID == admSchoolGroupID);

            if (createPct == null)
            {
                createPct = new AccCEPRates
                {
                    AdmSchoolGroupID = admSchoolGroupID,
                    CEPFreeRate = 0,
                    CEPFullPayRate = 0
                };

                await _repository.CreateAsync(createPct);
            }

            _repository.SaveChanges();
        }

        private async Task<AdmSchoolGroup> UpdateAdmSchoolGroup(PutAdmSchoolGroupRequest request)
        {
            var group = await _repository.FindAsync<AdmSchoolGroup>(request.AdmSchoolGroupID);

            if (group == null)
            {
                return await CreateAdmSchoolGroup(request);
            }

            group.Name = request.Name.Trim();
            group.Description = request.Description?.Trim() ?? "";
            group.IsCEPGroup = request.IsCEPGroup;

            //the logic is to rewrite the adm sites collection with incoming one
            //it is very tangled logic for this action in the old code which gives no additional profit, just useless...
            group.AdmSites.Clear();

            foreach (var groupSite in request.AdmSchoolGroupSites.Where(x => x.IsSelected))
            {
                var admSite = await _repository.FindAsync<AdmSite>(groupSite.AdmSiteID);

                if (admSite != null)
                {
                    group.AdmSites.Add(admSite);
                }
            }

            return group;
        }

        private async Task<AdmSchoolGroup> CreateAdmSchoolGroup(PutAdmSchoolGroupRequest request)
        {
            var admSchoolGroup = new AdmSchoolGroup
            {
                Name = request.Name.Trim(),
                Description = request.Description?.Trim() ?? "",
                IsCEPGroup = request.IsCEPGroup
            };

            foreach (var groupSite in request.AdmSchoolGroupSites.Where(x => x.IsSelected))
            {
                var admSite = await _repository.FindAsync<AdmSite>(groupSite.AdmSiteID);

                if (admSite != null)
                {
                    admSchoolGroup.AdmSites.Add(admSite);
                }
            }

            await _repository.CreateAsync(admSchoolGroup);
            return admSchoolGroup;
        }

        private async Task Validate(PutAdmSchoolGroupRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                Debug.WriteLine("PutAdmSchoolGroupRequest is invalid: empty Name field");
                throw new InvalidOperationException("Name should not be empty");
            }

            var isUniqueName = await IsUniqueGroupName(new GetIsUniqueGroupNameRequest
            {
                Id = request.AdmSchoolGroupID,
                Name = request.Name
            });

            if (!isUniqueName)
            {
                Debug.WriteLine("PutAdmSchoolGroupRequest is invalid: Name already exists");
                throw new InvalidOperationException("Name should be unique");
            }
        }
    }
}