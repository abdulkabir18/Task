using Application.DTOs;
using Application.DTOs.Auth;
using MediatR;

namespace Application.Features.Auth.GetAllUsers
{
    public class GetAllUsersQuery : IRequest<PaginatedResult<UserDto>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
