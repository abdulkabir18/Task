

using MediatR;

namespace Domain.DomainEvents;

    public class TaskCreatedEvent : INotification
    {
        public Guid TaskId { get; set; }
        public Guid UserId { get; set; }
        public string TaskTitle { get; set; } = string.Empty;
    }

