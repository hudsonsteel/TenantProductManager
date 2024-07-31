using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using TenantProductManager.Domain.Entities;
using TenantProductManager.Domain.Interfaces.Services;
using TenantProductManager.Infrastructure.Configurations;

namespace TenantProductManager.Infrastructure.DataBaseContext
{
    public class ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        IOptions<DbSettings> dbSettings,
        ITenantProvider tenantProvider) : DbContext(options)
    {
        private readonly DbSettings _dbSettings = dbSettings.Value;
        private readonly ITenantProvider _tenantProvider = tenantProvider;
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoryConfiguration(_tenantProvider));
            modelBuilder.ApplyConfiguration(new ProductConfiguration(_tenantProvider));
            modelBuilder.ApplyConfiguration(new TenantConfiguration(_tenantProvider));
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
        public static void ConfigureServices(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                 options.UseSqlServer(connectionString, b => b.MigrationsAssembly("TenantProductManager.Api")));
        }
    }
}
