using System.Collections.Generic;
using Solana.Web.Admin.Models.Responses.IntegrationMaps.NestedModels;

namespace Solana.Web.Admin.Models.Responses.IntegrationMaps
{
    public class GetTranslationsResponse
    {
        public ICollection<TranslationViewModel> Items { get; set; }
    }
}