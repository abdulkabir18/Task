using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Notifications.MarkAsRead
{
    public class MarkAsReadCommand : IRequest<bool>
    {
        public Guid NotificationId { get; set; }
    }
}
