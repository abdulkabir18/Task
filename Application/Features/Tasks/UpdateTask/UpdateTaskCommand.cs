using Application.DTOs;
using MediatR;

namespace Application.Features.Tasks.UpdateTask
{
    public class UpdateTaskCommand : IRequest<Result<string>>
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }
    }
}