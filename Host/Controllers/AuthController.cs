using Application.Features.Auth.Login;
using Application.Features.Auth.Register;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Features.ApplicationLogs.GetAllLogs;
using Application.Features.Auth.GetAllUsers;
using Application.Features.Auth.GetUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.RateLimiting;
using Swashbuckle.AspNetCore.Annotations;

namespace Host.Controllers
{
    [ApiController]
    [Route("api/users")]
    [ApiVersion("1.0")]
    [EnableRateLimiting("fixed")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet("all")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUsers([FromQuery] int page = 1, [FromQuery] int size = 10)
        {
            var result = await _mediator.Send(new GetAllUsersQuery { PageNumber = page, PageSize = size });
            return Ok(result);
        }

        [Authorize]
        [HttpGet("me")]
        [SwaggerOperation(Summary = "Get current user profile")]
        public async Task<IActionResult> GetProfile()
        {
            var result = await _mediator.Send(new GetMyProfileQuery());
            return Ok(result);
        }


        [HttpGet("logs")]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "Admin: View application logs")]
        public async Task<IActionResult> GetLogs()
        {
            var result = await _mediator.Send(new GetAllLogsQuery());
            return Ok(result);
        }


        [HttpPost]
        [SwaggerOperation(Summary = "Register a new user")]
        public async Task<IActionResult> Register([FromBody] RegisterCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetProfile), new { id = result }, result);
        }

        [HttpPost("auth/login")]
        [SwaggerOperation(Summary = "User login")]
        public async Task<IActionResult> Login([FromBody] LoginQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
