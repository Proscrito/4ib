using System.Collections.Generic;
using Solana.Web.Admin.Models.Responses.FeedingFigures.NestedModels;

namespace Solana.Web.Admin.Models.Responses.FeedingFigures
{
    public class GetFeedingFigureListResponse
    {
        public List<FeedingFigure> FeedingFigures { get; set; }
    }
}
