using System.Collections.Generic;
using System.Threading.Tasks;
using Solana.Web.Admin.Models.Requests.CNImport;
using Solana.Web.Admin.Models.Responses;
using Solana.Web.Admin.Models.Responses.CNImport.NestedModels;

namespace Solana.Web.Admin.BLL.Interfaces
{
    public interface ICNImportLogic
    {
        Task<GetCNImportResponse> GetCNImportViewModel(string contentPath);
        Task<string> GetCurrentCNVersion();
        Task<List<MenCnResultsHeaderModel>> GetImportResultsHeader();
        Task<List<MenCnResultsDetailModel>> GetImportResultsDetails(int menCnResultsHeaderID);
        void RunImportJob(PostRunImportRequest request);
    }
}
