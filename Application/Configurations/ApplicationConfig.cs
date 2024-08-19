using Application.Services;
using Application.UseCases.OdersOperation.Commands.Create;
using Domain.Interfaces;
using FluentValidation;
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
            
            //Services
            service.AddScoped<IAuthService, AuthService>();
            service.AddScoped<IOrderService, OrderService>();
            service.AddScoped<IAssetService, AssetService>();

            //Validators
            service.AddTransient<IValidator<CreateOrderCommandHandlerRequest>, CreateOrderCommandValidator>();




        }
    }
}
