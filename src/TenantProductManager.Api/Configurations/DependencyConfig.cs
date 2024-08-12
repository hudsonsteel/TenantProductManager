using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using TenantProductManager.Api.Mappers;
using TenantProductManager.Api.Validations.Auth;
using TenantProductManager.Api.Validations.Category;
using TenantProductManager.Api.Validations.Product;
using TenantProductManager.Api.Validations.Tenant;
using TenantProductManager.Application.Configurations.AppSettings;

namespace TenantProductManager.Api.Configurations
{
    public static class DependencyConfig
    {
        public static void AddApiConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapperConfig();
            services.AddJwtAuthenticationConfig(configuration);

            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
                options.JsonSerializerOptions.MaxDepth = 64;
            });
            services.AddRouting(options => options.LowercaseUrls = true);

            services.AddFluentValidationConfig();

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGenConfig();
        }

        private static void AddSwaggerGenConfig(this IServiceCollection services)
        {
            services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Asp.Net 8 Tenant Product Manager Web Api",
                    Description = "Authentication with JWT"
                });
                swagger.AddSecurityDefinition("Barear", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Barear",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme."
                });
                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });
        }

        private static void AddFluentValidationConfig(this IServiceCollection services)
        {
            services
                   .AddFluentValidationAutoValidation()
                   .AddFluentValidationClientsideAdapters()
                   .AddValidatorsFromAssemblyContaining<LoginRequestValidator>()
                   .AddValidatorsFromAssemblyContaining<RegisterRequestValidator>()
                   .AddValidatorsFromAssemblyContaining<CreateCategoryRequestValidator>()
                   .AddValidatorsFromAssemblyContaining<UpdateCategoryRequestValidator>()
                   .AddValidatorsFromAssemblyContaining<CreateProductRequestValidator>()
                   .AddValidatorsFromAssemblyContaining<UpdateProductRequestValidator>()
                   .AddValidatorsFromAssemblyContaining<CreateTenantRequestValidator>()
                   .AddValidatorsFromAssemblyContaining<UpdateTenantRequestValidator>();
        }

        public static void AddAutoMapperConfig(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(CategoryProfile));
            services.AddAutoMapper(typeof(ProductProfile));
            services.AddAutoMapper(typeof(TenantProfile));
        }

        public static void AddJwtAuthenticationConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

            var jwtSettings = configuration.GetSection("JwtSettings").Get<JwtSettings>()
                               ?? throw new InvalidOperationException("JwtSettings configuration is missing or invalid.");


            services.AddSingleton(jwtSettings);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        // Log or inspect context.Exception for debugging
                        var exception = context.Exception;
                        // Log or inspect context.Exception
                        return Task.CompletedTask;
                    }
                };

                if (string.IsNullOrEmpty(jwtSettings.SecretKey))
                {
                    throw new InvalidOperationException("JWT SecretKey is not configured.");
                }

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(jwtSettings.SecretKey))
                };
            });
        }
    }
}
