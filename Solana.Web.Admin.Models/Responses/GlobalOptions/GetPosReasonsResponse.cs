using System.Collections.Generic;
using Solana.Web.Admin.Models.Responses.GlobalOptions.NestedModels;

namespace Solana.Web.Admin.Models.Responses.GlobalOptions
{
    public class GetPosReasonsResponse
    {
        public ICollection<PosReasonModel> Items { get; set; }
    }
}
