using System.Collections.Generic;
using Solana.Web.Admin.Models.Requests.GlobalOptions.NestedModels;

namespace Solana.Web.Admin.Models.Requests.GlobalOptions
{
    public class PutMenLeftoverCodesRequest
    {
        public ICollection<MenLeftoverCodeSaveModel> Items { get; set; }
    }
}
