using System.Collections.Generic;
using Solana.Web.Admin.Models.Responses.GlobalOptions.NestedModels;

namespace Solana.Web.Admin.Models.Responses.GlobalOptions
{
    public class GetAgeGroupsResponse
    {
        public ICollection<MenAgeGroupsModel> Items { get; set; }
    }
}