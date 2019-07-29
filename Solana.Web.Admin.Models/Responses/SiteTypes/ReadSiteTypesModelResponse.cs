using System.Collections.Generic;
using Solana.Web.Admin.Models.Responses.SiteTypes.NestedModels;

namespace Solana.Web.Admin.Models.Responses.SiteTypes
{
    public class ReadSiteTypesModelResponse
    {
        public ICollection<SiteTypeModel> Items { get; set; }
    }
}
