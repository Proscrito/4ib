using System.Collections.Generic;
using Solana.Web.Admin.Models.Requests.P2ClaimingPercentage.NestedModels;

namespace Solana.Web.Admin.Models.Requests.P2ClaimingPercentage
{
    public class PutAccP2RatesRequest
    {
        public ICollection<AccP2RateSaveModel> Items { get; set; }
    }
}