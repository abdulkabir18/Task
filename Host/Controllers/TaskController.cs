using Microsoft.AspNetCore.Mvc;
using Application.Features.Auth.GetCompletedTask;
using Application.Features.Tasks.CreateTask;
using Application.Features.Tasks.Delete;
using Application.Features.Tasks.GetMyTasks;
using Application.Features.Tasks.UpdateTask;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.RateLimiting;
using Swashbuckle.AspNetCore.Annotations;

namespace Host.Controllers
{
    [ApiController]
    [Route("api/tasks")]
    [ApiVersion("1.0")]
    [Authorize]
    [EnableRateLimiting("fixed")]  
    public class TasksController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TasksController(IMediator mediator)
        {
            _mediator = mediator;
        }

     
        [HttpPost]
        [SwaggerOperation(Summary = "Create a new task")]
        
        public async Task<IActionResult> Create([FromBody] CreateTaskCommand command)
        {
            var result = await _mediator.Send(command);
            return result.Succeeded ? Ok(result) : BadRequest(result.Message);
        }

        
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update an existing task")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTaskCommand command)
        {
            command.Id = id;
            var result = await _mediator.Send(command);
            return result.Succeeded ? Ok(result) : BadRequest(result.Message);
        }

         
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete a task")]
         
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _mediator.Send(new DeleteTaskCommand { TaskId = id });
            return result.Succeeded ? Ok(result) : BadRequest(result.Message);
        }

         
        [HttpGet]
        [SwaggerOperation(Summary = "Get my tasks (paginated)")]
        
        public async Task<IActionResult> GetMyTasks([FromQuery] int page = 1, [FromQuery] int size = 10)
        {
            var result = await _mediator.Send(new GetMyTasksQuery { Page = page, PageSize = size });
            return Ok(result);
        }

        
        [HttpGet("completed")]
        [SwaggerOperation(Summary = "Get my completed tasks (paginated)")]
        public async Task<IActionResult> GetCompleted([FromQuery] int page = 1, [FromQuery] int size = 10)
        {
            var result = await _mediator.Send(new GetMyCompletedTasksQuery { Page = page, PageSize = size });
            return Ok(result);
        }
    }


}
