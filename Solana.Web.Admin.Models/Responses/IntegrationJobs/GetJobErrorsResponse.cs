using System.Collections.Generic;
using System.Collections.ObjectModel;
using Solana.Web.Admin.Models.Responses.IntegrationJobs.NestedModels;

namespace Solana.Web.Admin.Models.Responses.IntegrationJobs
{
    public class GetJobErrorsResponse
    {
        public ICollection<IntegrationJobResult> JobErrors { get; set; }
    }
}