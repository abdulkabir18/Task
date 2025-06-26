using Application.DTOs;
using Application.DTOs.Auth;
using MediatR;

namespace Application.Features.Auth.GetUser
{
    public class GetMyProfileQuery : IRequest<Result<UserDto>> { }
   
}
