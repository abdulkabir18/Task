using Application.Abstractions;
using Application.DTOs.Auth;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Auth.Login
{
    public class LoginHandler : IRequestHandler<LoginQuery, AuthResponse>
    {
        private readonly IUserRepository _userRepo;
        private readonly IJwtService _jwtService;
        private readonly IPasswordHasher<User> _passwordHasher;

        public LoginHandler(IUserRepository userRepo, IJwtService jwtService, IPasswordHasher<User> passwordHasher)
        {
            _userRepo = userRepo;
            _jwtService = jwtService;
            _passwordHasher = passwordHasher;
        }

        public async Task<AuthResponse> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepo.GetByEmailAsync(request.Email);
            if (user == null)
                throw new UnauthorizedAccessException("Invalid credentials");

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);
            if (result == PasswordVerificationResult.Failed)
                throw new UnauthorizedAccessException("Invalid credentials");

            var token = _jwtService.GenerateToken(user.Id, user.Email, user.Role.ToString());
            return new AuthResponse { Email = user.Email, Token = token, Role = user.Role.ToString() };
        }
    }
}
