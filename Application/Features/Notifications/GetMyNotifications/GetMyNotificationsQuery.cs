using Application.DTOs.Notifications;
using MediatR;

namespace Application.Features.Notifications.GetMyNotifications
{
    public class GetMyNotificationsQuery : IRequest<List<NotificationDto>> { }
}
