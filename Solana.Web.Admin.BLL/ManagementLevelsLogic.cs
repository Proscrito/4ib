using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Horizon.Common.Repository.Legacy;
using Horizon.Common.Repository.Legacy.Models.Adm;
using Solana.Web.Admin.BLL.Interfaces;
using Solana.Web.Admin.Models.Requests.ManagementLevels.NestedModels;
using Solana.Web.Admin.Models.Responses.ManagementLevels.NestedModels;

namespace Solana.Web.Admin.BLL
{
    public class ManagementLevelsLogic : IManagementLevelsLogic
    {
        private readonly ISolanaRepository _repository;
        private readonly IMapper _autoMapper;

        public ManagementLevelsLogic(ISolanaRepository repository, IMapper autoMapper)
        {
            _repository = repository;
            _autoMapper = autoMapper;
        }

        public async Task<List<AdmManagementLevelViewModel>> GetAdmManagementLevels()
        {
            var admManagementLevels = await _repository.GetListAsync<AdmManagementLevel>();

            return _autoMapper.Map<List<AdmManagementLevelViewModel>>(admManagementLevels.OrderBy(x => x.ManagementLevelOrder).ToList());
        }

        public async Task SaveAdmManagementLevels(IEnumerable<AdmManagementLevelSaveModel> requestItems)
        {
            var existingIds = new List<int>();
            
            foreach (var item in requestItems)
            {
                //update
                if (item.AdmManagementLevelID != 0)
                {
                    existingIds.Add(item.AdmManagementLevelID);
                    var existingAdmManagementLevel = await _repository.FindAsync<AdmManagementLevel>(item.AdmManagementLevelID);

                    if (existingAdmManagementLevel != null)
                    {
                        existingAdmManagementLevel.Description = item.Description;
                        existingAdmManagementLevel.ManagementLevelOrder = item.ManagementLevelOrder;
                        await _repository.UpdateAsync(existingAdmManagementLevel);
                    }
                }
                //insert
                else
                {
                    var newAdmManagementLevel = _autoMapper.Map<AdmManagementLevel>(item);
                    await _repository.CreateAsync(newAdmManagementLevel);
                }
            }

            //delete
            var deletedItems = await _repository.GetListAsync<AdmManagementLevel>(x => !existingIds.Contains(x.AdmManagementLevelID));
            await _repository.DeleteAsync(deletedItems);
        }
    }
}