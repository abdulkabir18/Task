using Application.Abstractions;
using Application.DTOs;
using Application.DTOs.Auth;
using MediatR;

namespace Application.Features.Auth.GetUser
{
    public class GetMyProfileHandler(ICurrentUserService currentUserService,IUserRepository userRepository) : IRequestHandler<GetMyProfileQuery, Result<UserDto>>
    {
        public async Task<Result<UserDto>> Handle(GetMyProfileQuery request, CancellationToken cancellationToken)
        {
             if (currentUserService == null || currentUserService.UserId == Guid.Empty)
             {
                 return  Result<UserDto>.Failure("User not authenticated.");
             }
             var user = await userRepository.GetByIdAsync(currentUserService.UserId);
             if (user == null)
             {
              return Result<UserDto>.Failure("User not found.");
             }

            var userDto = new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                FullName = user.FullName,
                CreatedAt = user.CreatedAt
            };
            return Result<UserDto>.Success(userDto);

        }
    }
}
