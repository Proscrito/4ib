using System.Collections.Generic;
using Solana.Web.Admin.Models.Responses.ServingPeriods.NestedModels;

namespace Solana.Web.Admin.Models.Responses.ServingPeriods
{
    public class GetServingPeriodsResponse
    {
        public ICollection<ServingPeriod> ServingPeriods { get; set; }
    }
}
