using System.Threading.Tasks;
using Solana.Web.Admin.Models.Requests.FeedingFigures;
using Solana.Web.Admin.Models.Responses.FeedingFigures;

namespace Solana.Web.Admin.BLL.Interfaces
{
    public interface IFeedingFiguresLogic
    {
        Task<GetFeedingFigureListResponse> GetFeedingFigures();
        Task<int> SaveFeedingFigure(PostFeedingFiguresRequest request);
    }
}
