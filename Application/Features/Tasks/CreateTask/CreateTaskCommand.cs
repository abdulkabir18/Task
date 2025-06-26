using Application.DTOs;
using MediatR;

namespace Application.Features.Tasks.CreateTask
{
    public class CreateTaskCommand : IRequest<Result<Guid>>
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime DueDate { get; set; }
    }
}
