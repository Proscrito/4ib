using Horizon.Common.Models.Responses;
using System.Collections.Generic;

namespace Solana.Web.Admin.Services.Interfaces
{
    public interface ISitesService
    {
        GetAdmSiteResponse GetAdmSite(int admSiteID);
        IList<GetAvailableSiteSummaryResponse> GetAvailableSiteSummaries(int admUserID, bool showInact, bool includeDistrict, bool excludeCEP, bool CEPOnly, int? MenAgeGroupID, bool showSchoolGroups, bool showAllSelection);
    }
}
