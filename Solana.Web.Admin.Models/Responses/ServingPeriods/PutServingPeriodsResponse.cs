using System.Collections.Generic;
using Solana.Web.Admin.Models.Responses.ServingPeriods.NestedModels;

namespace Solana.Web.Admin.Models.Responses.ServingPeriods
{
    public class PutServingPeriodsResponse
    {
        public string Error { get; set; }
        public bool Success { get; set; }
        public ICollection<ServingPeriod> Periods { get; set; }
    }
}