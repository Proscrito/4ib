using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Solana.Web.Admin.BLL.Interfaces;
using Solana.Web.Admin.Models.Responses;

namespace Solana.Web.Admin.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SitesController : ControllerBase
    {
        private ISitesLogic _sitesLogic;

        public SitesController(ISitesLogic sitesLogic)
        {
            _sitesLogic = sitesLogic;
        }

        [HttpGet("AdmSite")]
        public ActionResult<GetAdmSiteResponse> GetAdmSite(int admSiteID)
        {
            return _sitesLogic.GetAdmSite(admSiteID);
        }

        [HttpGet("AllowsRefunds")]
        public ActionResult<bool> GetAllowsRefunds(int admSiteID)
        {
            return _sitesLogic.GetAllowsRefunds(admSiteID);
        }

        [HttpGet("AvailableSiteSummaries")]
        public ActionResult<IList<GetAvailableSiteSummaryResponse>> GetAvailableSiteSummaries(int admUserID, bool showInact, bool includeDistrict, bool excludeCEP = false, bool CEPOnly = false, int? MenAgeGroupID = null, bool showSchoolGroups = false, bool showAllSelection = false)
        {
            return _sitesLogic.GetAvailableSiteSummaries(admUserID, showInact, includeDistrict, excludeCEP, CEPOnly, MenAgeGroupID, showSchoolGroups, showAllSelection).ToList();
        }

        [HttpGet("ServingPeriods")]
        public ActionResult<GetServingPeriodsResponse> GetServingPeriods()
        {
            return _sitesLogic.GetServingPeriods();
        }
    }
}