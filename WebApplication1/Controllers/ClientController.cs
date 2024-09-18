using Application.Exceptions;
using Application.Interfaces.Service;
using Application.Request;
using Application.Response;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CRM.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly IClientServices _clientService;

        public ClientController(IClientServices clientService)
        {
            _clientService = clientService;
        }
        
        /// <response code="200">Success</response>    
        [HttpGet] //[HttpOptions]
        [ProducesResponseType(typeof(Clients[]), 200)]
        [SwaggerOperation(Summary = "Retrieves a list of all clients.")]
        public async Task<IActionResult> GetListClients()
        {
            var result = await _clientService.GetAll();
            return new JsonResult(result) { StatusCode = 200 };
        }

        /// <param name="request">The details of the client to be created.</param>
        /// <response code="201">Success</response>
        [HttpPost]
        [ProducesResponseType(typeof(Clients[]), 201)]
        [ProducesResponseType(typeof(ApiError), 400)]
        [SwaggerOperation(Summary = "Creates a new client with the provided details.")]
        public async Task<IActionResult> CreateClient(ClientsRequest request)
        {
            try
            {
                var result = await _clientService.CreateClient(request);
                return new JsonResult(result) { StatusCode = 201 };
            }
            catch (BadRequest ex)
            {
                return new JsonResult(new ApiError { Message = ex.Message }) { StatusCode = 400 };
            }
        }
    }
}