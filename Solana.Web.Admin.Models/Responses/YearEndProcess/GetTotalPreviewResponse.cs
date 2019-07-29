using Solana.Web.Admin.Models.Responses.YearEndProcess.NestedModels;
using System.Collections.Generic;

namespace Solana.Web.Admin.Models.Responses.YearEndProcess
{
    public class GetTotalPreviewResponse
    {
        public ICollection<TotalPreviewModel> Items { get; set; }
    }
}
