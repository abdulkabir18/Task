using Application.Abstractions;
using Application.DTOs;
using Application.DTOs.Tasks;
using MediatR;

namespace Application.Features.Auth.GetCompletedTask
{
    public class GetMyCompletedTasksQueryHandler : IRequestHandler<GetMyCompletedTasksQuery, PaginatedResult<TaskDto>>
    {
        private readonly ITaskRepository _taskRepo;
        private readonly ICurrentUserService _user;
        private readonly IMemoryCacheService _cache;

        public GetMyCompletedTasksQueryHandler(
            ITaskRepository taskRepo,
            ICurrentUserService user,
            IMemoryCacheService cache)
        {
            _taskRepo = taskRepo;
            _user = user;
            _cache = cache;
        }

        public async Task<PaginatedResult<TaskDto>> Handle(GetMyCompletedTasksQuery request, CancellationToken cancellationToken)
        {
            string cacheKey = $"tasks:user:{_user.UserId}:completed:{request.Page}:{request.PageSize}";
            var cached = await _cache.GetAsync<PaginatedResult<TaskDto>>(cacheKey);
            if (cached is not null) return cached;

            var (tasks, total) = await _taskRepo.GetCompletedTasksAsync(_user.UserId, request.Page, request.PageSize);

            var result = new PaginatedResult<TaskDto>
            {
                Items = tasks.Select(t => new TaskDto
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    DueDate = t.DueDate,
                    Status = t.Status.ToString(),
                }).ToList(),
                PageNumber = request.Page,
                PageSize = request.PageSize,
                TotalCount = total
            };

            await _cache.SetAsync(cacheKey, result, TimeSpan.FromMinutes(3));
            return result;
        }
    }

}
