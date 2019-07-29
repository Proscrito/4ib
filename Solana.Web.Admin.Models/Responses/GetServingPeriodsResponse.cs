using System.Collections.Generic;

namespace Solana.Web.Admin.Models.Responses
{
    public class GetServingPeriodsResponse
    {
        public IList<GetServingPeriod> ServingPeriods { get; set; }
    }
}
