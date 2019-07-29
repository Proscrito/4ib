using System.Collections.Generic;
using Solana.Web.Admin.Models.Responses.CEPClaimingPercentage.NestedModels;

namespace Solana.Web.Admin.Models.Responses.CEPClaimingPercentage
{
    public class GetAdmSitesCEPClaimingPercentResponse
    {
        public IList<AdmSitesCEPClaimingPercent> Items { get; set; }
    }
}
