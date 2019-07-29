using Solana.Web.Admin.Models.Requests.YearEndProcess;
using Solana.Web.Admin.Models.Responses.YearEndProcess;
using System;
using System.Threading.Tasks;

namespace Solana.Web.Admin.BLL.Interfaces
{
    public interface IYearEndProcessLogic
    {
        Task<GetYearEndProcessSetUpOptionsResponse> GetYearEndProcessSetUpOptions(int admSiteId);
        Task<LoadSchoolsResponse> LoadSchools(bool isRolloverDone);
        LoadEndOfYearPreviewResponse LoadEndOfYearPreview(LoadEndOfYearPreviewRequest request);
        GetTotalPreviewResponse GetTotalPreview(GetTotalPreviewRequest request);
        GetEndOfYearEligibilityCountsResponse GetEndOfYearEligibilityCounts(GetEndOfYearEligibilityCountsRequest request);
        GetSchoolPreviewResponse GetSchoolPreview(GetSchoolPreviewRequest request);
        Task<GetGradeSitePromotionsResponse> GetGradeSitePromotions();
        Task<GetSiteAlternativeDatesResponse> GetSiteAlternativeDates(GetSiteAlternativeDatesRequest request);
        Task<GetSiteStartDatesPreviewResponse> GetSiteStartDatesPreview(GetSiteStartDatesPreviewRequest request);
        Task<GetSchoolsListResponse> GetSchoolsList();
        string GetTempStatusExpDate(DateTime startDate);
        Task<SaveGradeSitePromotionsResponse> SaveGradeSitePromotions(SaveGradeSitePromotionsRequest request);
        Task<SaveYearEndProcessSetupResponse> SaveYearEndProcessSetup(SaveYearEndProcessSetupRequest request);
        Task SaveSchoolAlternativeStartDates(SaveSchoolAlternativeStartDatesRequest request);
        int ExecuteEndOfYear(int schoolYear);
    }
}
