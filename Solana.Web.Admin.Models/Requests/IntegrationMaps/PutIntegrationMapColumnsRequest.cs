using System.Collections.Generic;
using Solana.Web.Admin.Models.Requests.IntegrationMaps.NestedModels;

namespace Solana.Web.Admin.Models.Requests.IntegrationMaps
{
    public class PutIntegrationMapColumnsRequest
    {
        public int MapId { get; set; }
        public ICollection<IntegrationMapColumnSaveModel> Items { get; set; }
    }
}
