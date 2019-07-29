using Solana.Web.Admin.Models.Responses;
using System.Collections.Generic;

namespace Solana.Web.Admin.BLL.Interfaces
{
    public interface ISitesLogic
    {
        GetAdmSiteResponse GetAdmSite(int admSiteID);
        bool GetAllowsRefunds(int admSiteID);
        IList<GetAvailableSiteSummaryResponse> GetAvailableSiteSummaries(int admUserID, bool showInact, bool includeDistrict, bool excludeCEP, bool CEPOnly, int? MenAgeGroupID, bool showSchoolGroups, bool showAllSelection);
        GetServingPeriodsResponse GetServingPeriods();
    }
}
