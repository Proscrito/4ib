using System.Collections.Generic;
using Solana.Web.Admin.Models.Responses.IntegrationJobs.NestedModels;

namespace Solana.Web.Admin.Models.Responses.IntegrationJobs
{
    public class GetIntegrationMapTypesResponse
    {
        public ICollection<JobSelectListItem> Types { get; set; }
    }
}