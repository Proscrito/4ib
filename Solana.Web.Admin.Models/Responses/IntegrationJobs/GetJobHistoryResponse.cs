using System.Collections.Generic;
using Solana.Web.Admin.Models.Responses.IntegrationJobs.NestedModels;

namespace Solana.Web.Admin.Models.Responses.IntegrationJobs
{
    public class GetJobHistoryResponse
    {
        public ICollection<IntegrationJobHistory> JobHistories { get; set; }
    }
}