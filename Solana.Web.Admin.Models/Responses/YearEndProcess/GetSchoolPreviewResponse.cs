using Solana.Web.Admin.Models.Responses.YearEndProcess.NestedModels;
using System.Collections.Generic;

namespace Solana.Web.Admin.Models.Responses.YearEndProcess
{
    public class GetSchoolPreviewResponse
    {
        public ICollection<SchoolPreviewModel> Items { get; set; }
    }
}
