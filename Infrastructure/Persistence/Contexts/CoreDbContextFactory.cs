using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Persistence.Contexts
{
    public class CoreDbContextFactory : IDesignTimeDbContextFactory<CoreDbContext>
    {
        public CoreDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../API"))
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<CoreDbContext>();
            var connectionString = configuration.GetConnectionString("Core");

            optionsBuilder.UseSqlServer(connectionString,
                b => b.MigrationsAssembly(typeof(CoreDbContext).Assembly.FullName));

            return new CoreDbContext(optionsBuilder.Options);
        }
    }
}
