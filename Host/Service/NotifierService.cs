using Application.Abstractions;
using Host.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace Host.Service
{
    public class Notifier : INotifier
    {
        private readonly IHubContext<NotificationHub> _hubContext;

        public Notifier(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task SendNotificationAsync(Guid userId, string message)
        {
            await _hubContext.Clients
                .Group(userId.ToString())
                .SendAsync("ReceiveNotification", message);
        }
    }

}
