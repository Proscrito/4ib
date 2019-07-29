using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Solana.Web.Admin.BLL.Interfaces;
using Solana.Web.Admin.Models.Requests.ServingPeriods;
using Solana.Web.Admin.Models.Responses.ServingPeriods;

namespace Solana.Web.Admin.API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ServingPeriodsController : ControllerBase
    { 
        private readonly IServingPeriodsLogic _logic;

        public ServingPeriodsController(IServingPeriodsLogic servingPeriodsLogic)
        {
            _logic = servingPeriodsLogic; 
        }

        /// <summary>
        /// Gets the serving periods.
        /// old: Read
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<GetServingPeriodsResponse>> GetServingPeriods()
        {
            return await _logic.GetServingPeriods();
        }

        /// <summary>
        /// Saves the serving periods.
        /// old: Save
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult<PutServingPeriodsResponse>> SaveServingPeriods(PutServingPeriodsRequest request)
        {
            return await _logic.SaveServingPeriods(request);
        }

        /// <summary>
        /// Deletes the serving periods.
        /// old: Delete
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteServingPeriods(int id)
        {
            return await _logic.DeleteServingPeriods(id);
        }
    }
}
