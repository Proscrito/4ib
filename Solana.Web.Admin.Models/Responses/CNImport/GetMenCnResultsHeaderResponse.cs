using System.Collections.Generic;
using Solana.Web.Admin.Models.Responses.CNImport.NestedModels;

namespace Solana.Web.Admin.Models.Responses.CNImport
{
    public class GetMenCnResultsHeaderResponse
    {
        public IList<MenCnResultsHeaderModel> Items { get; set; }
    }
}
