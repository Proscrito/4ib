using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Solana.Web.Admin.BLL.Interfaces;
using Solana.Web.Admin.Models.Requests.SiteTypes;
using Solana.Web.Admin.Models.Responses.SiteTypes;

namespace Solana.Web.Admin.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SiteTypesController : ControllerBase
    {
        private readonly ISiteTypesLogic _siteTypesLogic;

        public SiteTypesController(ISiteTypesLogic siteTypesLogic)
        {
            _siteTypesLogic = siteTypesLogic;
        }

        [HttpGet]
        public async Task<ActionResult<ReadSiteTypesModelResponse>> Read()
        {
            return await _siteTypesLogic.Read();
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateSiteTypeRequest request)
        {
            var validationResult = await _siteTypesLogic.Validate(request);
            foreach (var validation in validationResult)
            {
                ModelState.AddModelError(validation.Key, validation.Value);
            }

            if (ModelState.IsValid)
            {
                return await _siteTypesLogic.Create(request);
            }

            return BadRequest(ModelState);
        }

        [HttpPut]
        public async Task<ActionResult<int>> Update(UpdateSiteTypeRequest request)
        {
            return await _siteTypesLogic.Update(request);
        }

        [HttpDelete]
        public async Task<ActionResult<int>> Delete(DeleteSiteTypeRequest request)
        {
            return await _siteTypesLogic.Delete(request);
        }
    }
}