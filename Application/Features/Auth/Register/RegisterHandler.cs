using Application.Abstractions;
using Application.DTOs.Auth;
using Domain.Entities;
using Domain.Enums;
using Domain.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Register
{
    public class RegisterHandler : IRequestHandler<RegisterCommand, AuthResponse>
    {
        private readonly IUserRepository _userRepo;
        private readonly IJwtService _jwtService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher<User> _passwordHasher;

        public RegisterHandler(IUserRepository userRepo, IJwtService jwtService, IUnitOfWork unitOfWork, IPasswordHasher<User> passwordHasher)
        {
            _userRepo = userRepo;
            _jwtService = jwtService;
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
        }

        public async Task<AuthResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var user = new User
            {
                FullName = request.FullName,
                Email = Email.Create(request.Email),
                Role = Role.User,
            };

            user.PasswordHash = _passwordHasher.HashPassword(user, request.Password);
            await _userRepo.AddAsync(user);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var token = _jwtService.GenerateToken(user.Id, user.Email, user.Role.ToString());

            return new AuthResponse { Email = user.Email, Token = token, Role = user.Role.ToString() };
        }
    }
}
