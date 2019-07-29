using Solana.Web.Admin.Models.Responses.YearEndProcess.NestedModels;
using System.Collections.Generic;

namespace Solana.Web.Admin.Models.Responses.YearEndProcess
{
    public class GetSiteStartDatesPreviewResponse
    {
        public ICollection<StartDatePreviewModel> Items { get; set; }
    }
}
