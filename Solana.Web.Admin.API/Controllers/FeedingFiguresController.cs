using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Solana.Web.Admin.BLL.Interfaces;
using Solana.Web.Admin.Models.Requests.FeedingFigures;
using Solana.Web.Admin.Models.Responses.FeedingFigures;

namespace Solana.Web.Admin.API.Controllers
{
    //TODO: add Authorize attribute for verifying JWT
    //[Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class FeedingFiguresController : ControllerBase
    {
        /// <summary>
        /// The feeding figures logic
        /// </summary>
        private readonly IFeedingFiguresLogic _feedingFiguresLogic;

        /// <summary>
        /// Initializes a new instance of the <see cref="FeedingFiguresController"/> class.
        /// </summary>
        /// <param name="feedingFiguresLogic">The feeding figures.</param>
        public FeedingFiguresController(IFeedingFiguresLogic feedingFiguresLogic)
        {
            _feedingFiguresLogic = feedingFiguresLogic;
        }

        /// <summary>
        /// Gets the feeding figures.
        /// </summary>
        /// <returns></returns>
        [HttpGet("FeedingFigures")]
        public async Task<ActionResult<GetFeedingFigureListResponse>> GetFeedingFigures()
        {
            return await _feedingFiguresLogic.GetFeedingFigures();
        }

        /// <summary>
        /// Posts the feeding figure.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpPost("FeedingFigure")]
        public async Task<ActionResult<int>> PostFeedingFigure(PostFeedingFiguresRequest request)
        {
            return await _feedingFiguresLogic.SaveFeedingFigure(request);
        }
    }
}