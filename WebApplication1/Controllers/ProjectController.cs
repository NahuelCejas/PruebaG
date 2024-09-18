using Application.Exceptions;
using Application.Interfaces.Service;
using Application.Request;
using Application.Response;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CRM.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectServices _projectService;

        public ProjectController(IProjectServices projectService)
        {
            _projectService = projectService;
        }

        /// <param name="name">Optional. Filter by project name.</param>
        /// <param name="campaign">Optional. Filter by campaign type ID.</param>
        /// <param name="client">Optional. Filter by client ID.</param>
        /// <param name="offset">Optional. Skip the specified number of records (used for pagination).</param>
        /// <param name="size">Optional. Limit the number of records returned (used for pagination).</param>
        /// <response code="200">Success</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<Application.Response.Project>), 200)]
        [SwaggerOperation(Summary = "Retrieves a list of projects based on the provided filters such as project name, campaign type, client, with optional pagination parameters.")]
        public async Task<ActionResult> GetProjects(
            [FromQuery] string? name,
            [FromQuery] int? campaign,
            [FromQuery] int? client,
            [FromQuery] int? offset,
            [FromQuery] int? size)
        {
            var result = await _projectService.GetProjects(name, campaign, client, offset, size);
            return new JsonResult(result) { StatusCode = 200 };
        }
        
        /// <param name="request">The details of the project to be created.</param>
        /// <response code="201">Success</response>    
        [HttpPost]
        [ProducesResponseType(typeof(ProjectDetails[]), 201)]
        [ProducesResponseType(typeof(ApiError), 400)]
        [SwaggerOperation(Summary = "Creates a new project with the specified details.")]
        public async Task<IActionResult> CreateProject(ProjectRequest request)
        {
            try
            {
                var result = await _projectService.CreateProject(request);
                return new JsonResult(result) { StatusCode = 201 };
            }
            catch (BadRequest ex)
            {
                return new JsonResult(new ApiError { Message = ex.Message }) { StatusCode = 400 };
            }
        }

        /// <param name="id">The unique identifier of the project.</param>
        /// <response code="200">Success</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProjectDetails), 200)]
        [ProducesResponseType(typeof(ApiError), 404)]
        [SwaggerOperation(Summary = "Retrieves detailed information about a specific project by its ID.")]
        public async Task<IActionResult> GetProjectById([FromRoute] Guid id)
        {
            try
            {
                var result = await _projectService.GetProjectById(id);
                return new JsonResult(result) { StatusCode = 200 };
            }
            catch (NotFoundException ex)
            {
                return new JsonResult(new ApiError { Message = ex.Message }) { StatusCode = 404 };
            }
        }

        /// <param name="id">The unique identifier of the project.</param>
        /// <param name="request">The details of the interaction to be added.</param>
        /// <response code="201">Success</response>
        [HttpPatch("{id}/interactions")] //[HttpOptions("{id}/interactions")]
        [ProducesResponseType(typeof(Interactions), 201)]
        [ProducesResponseType(typeof(ApiError), 400)]
        [SwaggerOperation(Summary = "Adds a new interaction to an existing project.")]
        public async Task<IActionResult> AddInteraction([FromRoute] Guid id, [FromBody] InteractionsRequest request)
        {
            try
            {
                var result = await _projectService.AddInteraction(id, request);
                return new JsonResult(result) { StatusCode = 201 };
            }
            catch (BadRequest ex)
            {
                return new JsonResult(new ApiError { Message = ex.Message }) { StatusCode = 400 };
            }
        }

        /// <param name="id">The unique identifier of the project.</param>
        /// <param name="request">The details of the task to be added.</param>
        /// <response code="201">Success</response>
        [HttpPatch("{id}/tasks")]  //[HttpOptions("{id}/tasks")]
        [ProducesResponseType(typeof(Tasks), 201)]
        [ProducesResponseType(typeof(ApiError), 400)]
        [SwaggerOperation(Summary = "Adds a new task to an existing project.")]
        public async Task<IActionResult> AddTask([FromRoute] Guid id, [FromBody] TasksRequest request)
        {
            try
            {
                var result = await _projectService.AddTask(id, request);
                return new JsonResult(result) { StatusCode = 201 };
            }
            catch (BadRequest ex)
            {
                return new JsonResult(new ApiError { Message = ex.Message }) { StatusCode = 400 };
            }
        }

        /// <param name="id">The unique identifier of the task to be updated.</param>
        /// <param name="request">The updated details of the task.</param>
        /// <response code="200">Success</response> 
        [HttpPut("/api/v1/Tasks/{id}")] //[HttpOptions("/api/v1/Tasks/{id}")]
        [ProducesResponseType(typeof(Tasks), 200)]
        [ProducesResponseType(typeof(ApiError), 400)]
        [SwaggerOperation(Summary = "Updates an existing task with the specified details.")]
        public async Task<IActionResult> UpdateTask([FromRoute] Guid id, [FromBody] TasksRequest request)
        {
            try
            {
                var result = await _projectService.UpdateTask(id, request);
                return new JsonResult(result) { StatusCode = 200 };
            }
            catch (BadRequest ex)
            {
                return new JsonResult(new ApiError { Message = ex.Message }) { StatusCode = 400 };
            }
        }
    }
}