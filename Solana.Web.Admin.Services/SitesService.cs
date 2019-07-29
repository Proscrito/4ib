using Horizon.Common.Models.Responses;
using Solana.Web.Admin.BLL.Interfaces;
using Solana.Web.Admin.Services.Interfaces;
using System.Collections.Generic;

namespace Solana.Web.Admin.Services
{
    public class SitesService : ISitesService
    {
        private readonly ISitesLogic _sitesLogic;

        public SitesService(ISitesLogic sitesLogic)
        {
            _sitesLogic = sitesLogic;
        }
        public GetAdmSiteResponse GetAdmSite(int admSiteID)
        {
            return _sitesLogic.GetAdmSite(admSiteID);
        }

        public IList<GetAvailableSiteSummaryResponse> GetAvailableSiteSummaries(int admUserID, bool showInact, bool includeDistrict, bool excludeCEP, bool CEPOnly, int? MenAgeGroupID, bool showSchoolGroups, bool showAllSelection)
        {
            return _sitesLogic.GetAvailableSiteSummaries(admUserID, showInact, includeDistrict, excludeCEP, CEPOnly, MenAgeGroupID, showSchoolGroups, showAllSelection);
        }
    }
}
