using Application.Abstractions;
using Application.DTOs;
using Application.DTOs.Auth;
using MediatR;

namespace Application.Features.Auth.GetAllUsers
{
    public class GetAllUsersHandler : IRequestHandler<GetAllUsersQuery, PaginatedResult<UserDto>>
    {
        private readonly IUserRepository _repo;
        private readonly IMemoryCacheService _cache;

        public GetAllUsersHandler(IUserRepository repo, IMemoryCacheService cache)
        {
            _repo = repo;
            _cache = cache;
        }

        public async Task<PaginatedResult<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            string cacheKey = $"users:{request.PageNumber}:{request.PageSize}";
            var cached = await _cache.GetAsync<PaginatedResult<UserDto>>(cacheKey);
            if (cached is not null) return cached;

            var (users, count) = await _repo.GetAllPaginatedAsync(request.PageNumber, request.PageSize);
            var result = new PaginatedResult<UserDto>
            {
                Items = users.Select(u => new UserDto
                {
                    Id = u.Id,
                    Email = u.Email,
                    FullName = u.FullName,
                    CreatedAt = u.CreatedAt
                }).ToList(),
                TotalCount = count,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };

            await _cache.SetAsync(cacheKey, result, TimeSpan.FromMinutes(3));
            return result;
        }
    }

}
