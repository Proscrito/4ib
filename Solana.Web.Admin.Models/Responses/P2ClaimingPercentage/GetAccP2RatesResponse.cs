using System.Collections.Generic;
using Solana.Web.Admin.Models.Responses.P2ClaimingPercentage.NestedModels;

namespace Solana.Web.Admin.Models.Responses.P2ClaimingPercentage
{
    public class GetAccP2RatesResponse
    {
        public ICollection<AccP2RateViewModel> Items { get; set; }
    }
}