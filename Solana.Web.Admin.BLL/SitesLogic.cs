using AutoMapper;
using Horizon.Common.Repository.Legacy;
using Horizon.Common.Repository.Legacy.Models.Adm;
using Solana.Web.Admin.BLL.Interfaces;
using Solana.Web.Admin.Models;
using Solana.Web.Admin.Models.Responses;
using System.Collections.Generic;
using System.Linq;

namespace Solana.Web.Admin.BLL
{
    public class SitesLogic : ISitesLogic
    {
        private readonly ISolanaRepository _repo;
        private readonly IMapper _autoMapper;

        public SitesLogic(ISolanaRepository repo, IMapper autoMapper)
        {
            _repo = repo;
            _autoMapper = autoMapper;
        }

        public GetAdmSiteResponse GetAdmSite(int admSiteID)
        {
            var admSite = _repo.Find<AdmSite>(admSiteID);

            //map the data model to the API response model
            return _autoMapper.Map<GetAdmSiteResponse>(admSite);
        }

        public bool GetAllowsRefunds(int admSiteID)
        {
            var refund = _repo.GetQueryable<AdmSitesOption>(w => w.AdmSiteID == admSiteID && w.IsRefundAllowed == true);
            return refund.Count() > 0;
        }

        public IList<GetAvailableSiteSummaryResponse> GetAvailableSiteSummaries(int admUserID, bool showInact, bool includeDistrict, bool excludeCEP, bool CEPOnly, int? MenAgeGroupID, bool showSchoolGroups, bool showAllSelection)
        {
            //TODO: take the business logic outsite of usp_GetSitesSummaries and do it here
            var availableSiteSummaries = _repo.StoredProcList<AdmSitesSelectorListSummary>
                                                (StoredProcs.usp_GetSitesSummaries,
                                                new
                                                {
                                                    AdmUserID = admUserID,
                                                    ShowInactive = showInact,
                                                    IncludeDistrict = includeDistrict,
                                                    ExcludeCEP = excludeCEP,
                                                    CEPOnly,
                                                    MenAgeGroupID,
                                                    SchoolGroups = showSchoolGroups,
                                                    ShowAllSelection = showAllSelection
                                                });

            //map the data model to the API response model
            return _autoMapper.Map<List<GetAvailableSiteSummaryResponse>>(availableSiteSummaries);
        }

        public GetServingPeriodsResponse GetServingPeriods()
        {
            var servingPeriods = _repo.GetQueryable<AdmServingPeriod>().ToList();
            var response = new GetServingPeriodsResponse
            {
                ServingPeriods = _autoMapper.Map<List<GetServingPeriod>>(servingPeriods)
            };

            return response;
        }
    }
}
