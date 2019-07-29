using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Horizon.Common.Repository.Legacy;
using Horizon.Common.Repository.Legacy.Models.Acc;
using Horizon.Common.Repository.Legacy.Models.Adm;
using Horizon.Common.Repository.Legacy.Models.View;
using Solana.Web.Admin.BLL.Interfaces;
using Solana.Web.Admin.Models.Requests.P2ClaimingPercentage.NestedModels;
using Solana.Web.Admin.Models.Responses.P2ClaimingPercentage.NestedModels;

namespace Solana.Web.Admin.BLL
{
    public class P2ClaimingPercentageLogic : IP2ClaimingPercentageLogic
    {
        private readonly ISolanaRepository _repository;
        private readonly IMapper _autoMapper;

        public P2ClaimingPercentageLogic(ISolanaRepository repository, IMapper autoMapper)
        {
            _repository = repository;
            _autoMapper = autoMapper;
        }

        public async Task<List<AccP2RateViewModel>> GetAccP2Rates()
        {
            var accP2Rates = await _repository.GetListAsync<view_AccP2Rates>();
            return _autoMapper.Map<List<AccP2RateViewModel>>(accP2Rates.OrderBy(x => x.SchoolDescription));
        }

        public async Task<List<int>> SaveAccP2Rates(IEnumerable<AccP2RateSaveModel> requestItems)
        {
            var idCollection = new List<int>();

            foreach (var accP2RateSaveModel in requestItems)
            {
                if (accP2RateSaveModel.IsBreakfastOnly)
                {
                    accP2RateSaveModel.LUFreeRate = 0;
                    accP2RateSaveModel.LUReducedRate = 0;
                    accP2RateSaveModel.LUFullPayRate = 0;
                }

                var admOptions = await _repository.GetAsync<AdmSitesOption>(x => x.AdmSiteID == accP2RateSaveModel.AdmSiteID);
                var accP2Rate = await _repository.FindAsync<AccP2Rates>(accP2RateSaveModel.AccP2RateID) ?? new AccP2Rates(); 

                accP2Rate = _autoMapper.Map(accP2RateSaveModel, accP2Rate);

                //audit fields CreatedBy, ModifiedDate etc. should be set in a generic place, not each place we use repo for create/update
                if (accP2RateSaveModel.AccP2RateID == 0)
                {
                    accP2Rate.EffectiveDate = DateTime.Parse($"7/1/{admOptions?.BaseYearStart ?? DateTime.Now.Year}");
                    await _repository.CreateAsync(accP2Rate);
                }
                else
                {
                    await _repository.UpdateAsync(accP2Rate);
                }

                idCollection.Add(accP2Rate.AccP2RateID);
            }

            return idCollection;
        }
    }
}