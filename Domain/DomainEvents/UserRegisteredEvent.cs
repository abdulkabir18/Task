using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DomainEvents
{
    public class UserRegisteredEvent : INotification
    {
        public Guid UserId { get; set; }
        public string Email { get; set; } = string.Empty;
    }
}
