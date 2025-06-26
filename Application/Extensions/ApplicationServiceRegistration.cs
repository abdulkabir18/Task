using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Extensions
{
    using Application.Validators;
    using FluentValidation;
    using FluentValidation.AspNetCore;
    using MediatR;
    using Microsoft.Extensions.DependencyInjection;
    using System.Reflection;

    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // For MediatR v12+
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining<LoginValidator>();
           

            return services;
        }
    }


}
