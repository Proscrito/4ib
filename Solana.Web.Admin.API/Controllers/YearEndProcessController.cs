using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Solana.Web.Admin.BLL.Interfaces;
using Solana.Web.Admin.Models.Requests.YearEndProcess;
using Solana.Web.Admin.Models.Responses.YearEndProcess;
using System;
using System.Threading.Tasks;

namespace Solana.Web.Admin.API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class YearEndProcessController : ControllerBase
    {
        private readonly IYearEndProcessLogic _yearEndProcessLogic;
        
        public YearEndProcessController(IYearEndProcessLogic yearEndProcessLogic)
        {
            _yearEndProcessLogic = yearEndProcessLogic;
        }

        /// <summary>
        /// Gets the year end process set up options.
        /// Old: Index
        /// </summary>
        /// <param name="admSiteId">The adm site identifier.</param>
        /// <returns></returns>
        [HttpGet("SetUpOptions")]
        public async Task<GetYearEndProcessSetUpOptionsResponse> GetYearEndProcessSetUpOptions(int admSiteId)
        {
            return await _yearEndProcessLogic.GetYearEndProcessSetUpOptions(admSiteId);
        }

        /// <summary>
        /// Loads the schools.
        /// Old: LoadSchools
        /// </summary>
        /// <param name="isRolloverDone">if set to <c>true</c> [is rollover done].</param>
        /// <returns></returns>
        [HttpGet("LoadSchools")]
        public async Task<LoadSchoolsResponse> LoadSchools(bool isRolloverDone)
        {
            return await _yearEndProcessLogic.LoadSchools(isRolloverDone);
        }

        /// <summary>
        /// Loads the end of year preview.
        /// Old: LoadEOYPreview
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpGet("Preview")]
        public LoadEndOfYearPreviewResponse LoadEndOfYearPreview([FromQuery]LoadEndOfYearPreviewRequest request)
        {
            return _yearEndProcessLogic.LoadEndOfYearPreview(request);
        }

        /// <summary>
        /// Gets the total preview.
        /// Old: ReadTotalPreview
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpGet("TotalPreview")]
        public GetTotalPreviewResponse GetTotalPreview([FromQuery]GetTotalPreviewRequest request)
        {
            return _yearEndProcessLogic.GetTotalPreview(request);
        }

        /// <summary>
        /// Gets the end of year eligibility counts.
        /// Old: ReadEOYEligibilityCounts
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpGet("EligibilityCounts")]
        public GetEndOfYearEligibilityCountsResponse GetEndOfYearEligibilityCounts([FromQuery]GetEndOfYearEligibilityCountsRequest request)
        {
            return _yearEndProcessLogic.GetEndOfYearEligibilityCounts(request);
        }

        /// <summary>
        /// Gets the school preview.
        /// Old: ReadSchoolPreview
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpGet("SchoolPreview")]
        public GetSchoolPreviewResponse GetSchoolPreview([FromQuery]GetSchoolPreviewRequest request)
        {
            return _yearEndProcessLogic.GetSchoolPreview(request);
        }

        /// <summary>
        /// Gets the grade site promotions.
        /// Old: ReadSchool
        /// </summary>
        /// <returns></returns>
        [HttpGet("GradeSitePromotions")]
        public async Task<GetGradeSitePromotionsResponse> GetGradeSitePromotions()
        {
            return await _yearEndProcessLogic.GetGradeSitePromotions();
        }

        /// <summary>
        /// Gets the site alternative dates.
        /// Old: ReadSiteAltDates
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpGet("SiteAlternativeDates")]
        public async Task<GetSiteAlternativeDatesResponse> GetSiteAlternativeDates([FromQuery]GetSiteAlternativeDatesRequest request)
        {
            return await _yearEndProcessLogic.GetSiteAlternativeDates(request);
        }

        /// <summary>
        /// Gets the site start dates preview.
        /// Old: ReadSiteStartDatesPreview
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpGet("SiteStartDatesPreview")]
        public async Task<GetSiteStartDatesPreviewResponse> GetSiteStartDatesPreview([FromQuery]GetSiteStartDatesPreviewRequest request)
        {
            return await _yearEndProcessLogic.GetSiteStartDatesPreview(request);
        }

        /// <summary>
        /// Gets the schools list.
        /// Old: GetSchoolList
        /// </summary>
        /// <returns></returns>
        [HttpGet("SchoolsList")]
        public async Task<GetSchoolsListResponse> GetSchoolsList()
        {
            return await _yearEndProcessLogic.GetSchoolsList();
        }

        /// <summary>
        /// Gets the temporary status exp date.
        /// Old: GetTempExpDate
        /// </summary>
        /// <param name="startDate">The start date.</param>
        /// <returns></returns>
        [HttpGet("TempStatusExpDate")]
        public string GetTempStatusExpDate(DateTime startDate)
        {
            return _yearEndProcessLogic.GetTempStatusExpDate(startDate);
        }

        /// <summary>
        /// Saves the grade site promotions.
        /// Old: SaveGradeSitePromotions
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpPut("GradeSitePromotions")]
        public async Task<SaveGradeSitePromotionsResponse> SaveGradeSitePromotions(SaveGradeSitePromotionsRequest request)
        {
            return await _yearEndProcessLogic.SaveGradeSitePromotions(request);
        }

        /// <summary>
        /// Saves the year end process setup.
        /// Old: SaveYearEndProcessSetup
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpPut("Setup")]
        public async Task<SaveYearEndProcessSetupResponse> SaveYearEndProcessSetup(SaveYearEndProcessSetupRequest request)
        {
            return await _yearEndProcessLogic.SaveYearEndProcessSetup(request);
        }

        /// <summary>
        /// Saves the school alternative start dates.
        /// Old: SaveSchoolAltDates
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpPut("SchoolAlternativeStartDates")]
        public async Task SaveSchoolAlternativeStartDates(SaveSchoolAlternativeStartDatesRequest request)
        {
            await _yearEndProcessLogic.SaveSchoolAlternativeStartDates(request);
        }

        /// <summary>
        /// Executes the end of year.
        /// Old: ExecuteEOY
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpPost("Execute")]
        public int ExecuteEndOfYear(ExecuteEndOfYearRequest request)
        {
            return _yearEndProcessLogic.ExecuteEndOfYear(request.SchoolYear);
        }
    }
}
