using System.Collections.ObjectModel;
using Solana.Web.Admin.Models.Responses.IntegrationJobs.NestedModels;

namespace Solana.Web.Admin.Models.Responses.IntegrationJobs
{
    public class GetEligibilitiesResponse
    {
        public Collection<JobEligibility> Eligibilities { get; set; }
    }
}