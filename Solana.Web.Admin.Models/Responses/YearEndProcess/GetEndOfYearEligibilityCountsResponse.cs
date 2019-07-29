using Solana.Web.Admin.Models.Responses.YearEndProcess.NestedModels;
using System.Collections.Generic;

namespace Solana.Web.Admin.Models.Responses.YearEndProcess
{
    public class GetEndOfYearEligibilityCountsResponse
    {
        public ICollection<EligibilityCountModel> Items { get; set; }
    }
}
