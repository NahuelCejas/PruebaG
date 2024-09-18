using Application.Interfaces.Service;
using Application.Response;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CRM.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class InteractionTypesController : ControllerBase
    {
        private readonly IInteractionTypeServices _interactionTypeServices;

        public InteractionTypesController(IInteractionTypeServices interactionTypeServices)
        {
            _interactionTypeServices = interactionTypeServices;
        }    
      
        /// <response code="200">Success</response>
        [HttpGet]
        [ProducesResponseType(typeof(GenericResponse[]), 200)]
        [SwaggerOperation(Summary = "Retrieves a list of all interaction types.")]
        public async Task<IActionResult> GetListInteractionTypes()
        {
            var result = await _interactionTypeServices.GetAll();
            return new JsonResult(result) { StatusCode = 200 };
        }
    }
}