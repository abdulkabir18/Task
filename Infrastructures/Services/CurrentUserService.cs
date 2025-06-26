using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructures.Services
{
    using Application.Abstractions;
    using Microsoft.AspNetCore.Http;
    using System.Security.Claims;

    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _http;

        public CurrentUserService(IHttpContextAccessor http)
        {
            _http = http;
        }

        public Guid UserId =>
            Guid.TryParse(_http.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier), out var id) ? id : Guid.Empty;

        public string Role =>
            _http.HttpContext?.User.FindFirstValue(ClaimTypes.Role) ?? "User";

        public string Email =>
            _http.HttpContext?.User.FindFirstValue(ClaimTypes.Name) ?? string.Empty;
    }

}
