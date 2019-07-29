using Solana.Web.Admin.Models.Requests.YearEndProcess.NestedModels;
using System.Collections.Generic;

namespace Solana.Web.Admin.Models.Requests.YearEndProcess
{
    public class SaveGradeSitePromotionsRequest
    {
        public int AdmSiteId { get; set; }
        public List<GradeSitePromotionModel> Promotions { get; set; }
    }
}
