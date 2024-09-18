using Application.Interfaces.Service;
using Application.Response;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CRM.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TaskStatusController : ControllerBase
    {
        private readonly ITaskStatusServices _taskStatusService;

        public TaskStatusController(ITaskStatusServices taskStatusService)
        {
            _taskStatusService = taskStatusService;
        }

        /// <response code="200">Success</response>
        [HttpGet]
        [ProducesResponseType(typeof(GenericResponse[]), 200)]
        [SwaggerOperation(Summary = "Retrieves a list of all task statuses.")]
        public async Task<IActionResult> GetListTaskStatus()
        {
            var result = await _taskStatusService.GetAll();
            return new JsonResult(result) { StatusCode = 200 };
        }
    }
}