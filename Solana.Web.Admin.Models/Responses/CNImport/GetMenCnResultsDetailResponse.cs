using System.Collections.Generic;
using Solana.Web.Admin.Models.Responses.CNImport.NestedModels;

namespace Solana.Web.Admin.Models.Responses.CNImport
{
    public class GetMenCnResultsDetailResponse
    {
        public IList<MenCnResultsDetailModel> Items { get; set; }
    }
}
