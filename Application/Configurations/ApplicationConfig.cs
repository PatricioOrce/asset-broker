using Application.Services;
using Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Configurations
{
    public static class ApplicationConfig
    {
        public static void AddApplication(this IServiceCollection service) 
        {
            service.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(ApplicationConfig).Assembly));
            service.AddScoped<IAuthService, AuthService>();
            service.AddScoped<IOrderService, OrderService>();


        }
    }
}
