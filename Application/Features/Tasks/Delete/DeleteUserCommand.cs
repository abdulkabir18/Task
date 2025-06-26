using Application.DTOs;
using MediatR;

namespace Application.Features.Tasks.Delete
{
    public class DeleteTaskCommand : IRequest<Result<string>>
    {
        public Guid TaskId { get; set; }
    }
}
