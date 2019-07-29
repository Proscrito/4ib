using System.Collections.Generic;
using Solana.Web.Admin.Models.Responses.ServingPeriods.NestedModels;

namespace Solana.Web.Admin.Models.Requests.ServingPeriods
{
    public class PutServingPeriodsRequest
    {
        public ICollection<ServingPeriod> Periods { get; set; }
    }
}