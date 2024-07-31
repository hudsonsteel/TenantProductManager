using Microsoft.Extensions.DependencyInjection;
using TenantProductManager.Application.Interfaces.Services;
using TenantProductManager.Application.Profiles;
using TenantProductManager.Application.Services;
using TenantProductManager.Domain.Interfaces.Services;

namespace TenantProductManager.Application.Configurations
{
    public static class DependencyConfig
    {
        public static void AddAplicationConfig(this IServiceCollection services)
        {
            services.AddAutoMapperConfig();

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITenantService, TenantService>();

            services.AddHttpContextAccessor();
            services.AddScoped<ITenantProvider, TenantProvider>();
        }
        public static void AddAutoMapperConfig(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ProductProfile));
            services.AddAutoMapper(typeof(CategoryProfile));
            services.AddAutoMapper(typeof(TenantProfile));
            services.AddAutoMapper(typeof(UserProfile));
        }
    }
}
