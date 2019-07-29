using System.Collections.Generic;
using Solana.Web.Admin.Models.Responses.GlobalOptions.NestedModels;

namespace Solana.Web.Admin.Models.Responses.GlobalOptions
{
    public class GetInvTransactionTypesResponse
    {
        public ICollection<InvTransactionTypeModel> Items { get; set; }
    }
}
