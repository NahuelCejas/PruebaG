using Application.Interfaces.Service;
using Application.Response;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CRM.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CampaignTypeController : ControllerBase
    {
        private readonly ICampaignTypeServices _campaignTypeService;

        public CampaignTypeController(ICampaignTypeServices campaignTypeService)
        {
            _campaignTypeService = campaignTypeService;
        }

        /// <response code="200">Success</response>
        /// <returns>Success</returns>
        [HttpGet]
        [ProducesResponseType(typeof(GenericResponse[]), 200)]
        [SwaggerOperation(Summary = "Retrieves a list of all campaign types.")]
        public async Task<IActionResult> GetListCampaignTypes()
        {
            var result = await _campaignTypeService.GetAll();
            return new JsonResult(result) { StatusCode = 200 };
        }
    }
}