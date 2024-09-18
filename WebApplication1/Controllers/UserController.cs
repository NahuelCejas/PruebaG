using Application.Interfaces.Service;
using Application.Response;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CRM.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _userService;
        public UserController(IUserServices userService)
        {
            _userService = userService;
        }
        
        /// <response code="200">Success</response>
        [HttpGet]
        [ProducesResponseType(typeof(Users[]), 200)]
        [SwaggerOperation(Summary = "Retrieves a list of all users.")]
        public async Task<IActionResult> GetListUsers()
        {
            var result = await _userService.GetAll();
            return new JsonResult(result) { StatusCode = 200 };
        }
    }
}