using System.Collections.Generic;
using System.Threading.Tasks;
using Solana.Web.Admin.Models.Requests.SiteTypes;
using Solana.Web.Admin.Models.Responses.SiteTypes;

namespace Solana.Web.Admin.BLL.Interfaces
{
    public interface ISiteTypesLogic
    {
        Task<ReadSiteTypesModelResponse> Read();

        Task<int> Create(CreateSiteTypeRequest request);

        Task<int> Update(UpdateSiteTypeRequest request);

        Task<int> Delete(DeleteSiteTypeRequest request);

        Task<List<KeyValuePair<string, string>>> Validate(CreateSiteTypeRequest request);
    }
}
