using Application.DTOs;
using Application.DTOs.Tasks;
using MediatR;

namespace Application.Features.Tasks.GetMyTasks
{
    public class GetMyTasksQuery : IRequest<PaginatedResult<TaskDto>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
