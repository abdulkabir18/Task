using Microsoft.AspNetCore.Mvc;
using Application.Features.Notifications.GetMyNotifications;
using Application.Features.Notifications.MarkAsRead;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Annotations;

namespace Host.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/notifications")]
    [ApiVersion("1.0")]
    public class NotificationController : ControllerBase
    {
        private readonly IMediator _mediator;
        public NotificationController(IMediator mediator) => _mediator = mediator;

        [Authorize]
        [SwaggerOperation(Summary = "Get My Notifications", Description = "Retrieves the notifications for the authenticated user.")]
        [HttpGet]
        public async Task<IActionResult> GetMyNotifications()
            => Ok(await _mediator.Send(new GetMyNotificationsQuery()));

        [Authorize]
        [SwaggerOperation(Summary = "Mark Notification as Read", Description = "Marks a specific notification as read for the authenticated user.")]
        [HttpPatch("{id}/read")]
        public async Task<IActionResult> MarkAsRead(Guid id)
            => Ok(await _mediator.Send(new MarkAsReadCommand { NotificationId = id }));
    }

}
