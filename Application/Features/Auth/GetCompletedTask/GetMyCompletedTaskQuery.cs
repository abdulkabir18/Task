using Application.DTOs;
using Application.DTOs.Tasks;
using MediatR;

namespace Application.Features.Auth.GetCompletedTask
{
    public record GetMyCompletedTasksQuery : IRequest<PaginatedResult<TaskDto>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
