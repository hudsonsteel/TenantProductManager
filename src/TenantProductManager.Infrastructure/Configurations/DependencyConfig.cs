using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TenantProductManager.Domain.Interfaces.Repositories;
using TenantProductManager.Infrastructure.DataBaseContext;
using TenantProductManager.Infrastructure.Repositories;

namespace TenantProductManager.Infrastructure.Configurations
{
    public static class DependencyConfig
    {
        public static void AddRepositoriesConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DbSettings>(configuration.GetSection("DbSettings"));
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")
                , b => b.MigrationsAssembly("TenantProductManager.Infrastructure")));

            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ITenantRepository, TenantRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}
