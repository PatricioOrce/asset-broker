using Application.Services;
using Domain.Interfaces;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Configuration
{
    public static class ConfigureInfrastructure
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AccountDbContext>(options =>
               options.UseSqlServer(configuration.GetConnectionString("AccountManagement")));
            services.AddDbContext<CoreDbContext>(options =>
               options.UseSqlServer(configuration.GetConnectionString("Core")));
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IAssetRepository, AssetRepository>();
            return services;
        }
    }
}
