using System.Collections.Generic;
using Solana.Web.Admin.Models.Responses.IntegrationJobs.NestedModels;

namespace Solana.Web.Admin.Models.Responses.IntegrationJobs
{
    public class GetJobsResponse
    {
        public ICollection<IntegrationJob> Jobs { get; set; }
    }
}
