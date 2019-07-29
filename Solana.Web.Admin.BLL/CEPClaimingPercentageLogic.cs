using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Horizon.Common.Repository.Legacy;
using Horizon.Common.Repository.Legacy.Models.Acc;
using Horizon.Common.Repository.Legacy.Models.Adm;
using Horizon.Common.Repository.Legacy.Models.View;
using Solana.Web.Admin.BLL.Interfaces;
using Solana.Web.Admin.Models.Requests.CEPClaimingPercentage;
using Solana.Web.Admin.Models.Responses.CEPClaimingPercentage.NestedModels;

namespace Solana.Web.Admin.BLL
{
    public class CEPClaimingPercentageLogic : ICEPClaimingPercentageLogic
    {
        private readonly ISolanaRepository _repository;
        private readonly IMapper _autoMapper;

        public CEPClaimingPercentageLogic(ISolanaRepository repository, IMapper autoMapper)
        {
            _repository = repository;
            _autoMapper = autoMapper;
        }

        public async Task<int> SaveCEPClaimRates(PutCEPClaimRatesRequest cepClaimRatesRequest)
        {
            AccCEPRates claimPct;

            switch (cepClaimRatesRequest.CepOption)
            {
                case 1:
                    claimPct = await _repository.GetAsync<AccCEPRates>(w => w.AdmSiteID == cepClaimRatesRequest.AdmSiteId);
                    break;
                case 2:
                    claimPct = await _repository.GetAsync<AccCEPRates>(w => w.AdmSchoolGroupID == cepClaimRatesRequest.AdmSiteId);
                    break;
                default:
                    claimPct = await _repository.GetAsync<AccCEPRates>(w => w.AdmSchoolGroupID == null && w.AdmSiteID == null);
                    break;
            }

            if (claimPct != null)
            {
                claimPct.CEPFreeRate = cepClaimRatesRequest.CepFreeRate;
                claimPct.CEPFullPayRate = cepClaimRatesRequest.CepFullPayRate;

                var updated = await _repository.UpdateAsync(claimPct);
                return updated.AccCEPRateID;
            }

            Debug.WriteLine($"AccCEPRates not found in database. CepOption - {cepClaimRatesRequest.CepOption}, AdmSiteId - {cepClaimRatesRequest.AdmSiteId}", nameof(GetAdmSitesCEPClaimingPercent));
            throw new ApplicationException("AccCEPRates not found in database");
        }

        public async Task<List<AdmSitesCEPClaimingPercent>> GetAdmSitesCEPClaimingPercent(int admUserID)
        {
            var admGlobalOptions = await _repository.GetListAsync<AdmGlobalOption>();

            if (!admGlobalOptions.Any())
            {
                Debug.WriteLine("No AdmGlobalOption found in database.", nameof(GetAdmSitesCEPClaimingPercent));
                throw new ApplicationException("No AdmGlobalOption found in database");
            }

            var admGlobalOption = admGlobalOptions.First();

            var optionList = await _repository.GetListAsync<view_AccCEPRates>(p => p.CEPSchoolOption == (int)admGlobalOption.CEPSchoolOption);

            return _autoMapper.Map<List<AdmSitesCEPClaimingPercent>>(optionList);
        }
    }
}
