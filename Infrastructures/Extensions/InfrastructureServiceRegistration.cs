using Application.Abstractions;
using Infrastructures.Caching;
using Infrastructures.DBaseContext;
using Infrastructures.Repositories;
using Infrastructures.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructures.Extensions
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            var connStr = config.GetConnectionString("DevString")!;

            services.AddDbContext<AppDbContext>(options =>
                options.UseMySQL(connStr, ServerVersion.AutoDetect(connStr))
                       .EnableDetailedErrors()
                       .EnableSensitiveDataLogging());


            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<ILogRepository, LogRepository>();
            services.AddScoped<IMemoryCacheService, MemoryCacheService>();

            services.AddMemoryCache();

            return services;
        }
    }

}
