using System.Collections.Generic;
using Solana.Web.Admin.Models.Requests.GlobalOptions.NestedModels;

namespace Solana.Web.Admin.Models.Requests.GlobalOptions
{
    public class PutInvTransactionTypesRequest
    {
        public ICollection<InvTransactionTypeSaveModel> Items { get; set; }
    }
}
