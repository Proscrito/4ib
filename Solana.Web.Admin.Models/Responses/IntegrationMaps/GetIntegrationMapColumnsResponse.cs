using System.Collections.Generic;
using Solana.Web.Admin.Models.Responses.IntegrationMaps.NestedModels;

namespace Solana.Web.Admin.Models.Responses.IntegrationMaps
{
    public class GetIntegrationMapColumnsResponse
    {
        public ICollection<IntegrationMapColumnViewModel> Items { get; set; }
    }
}
