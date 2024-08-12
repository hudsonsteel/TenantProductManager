using TenantProductManager.Api.Configurations;
using TenantProductManager.Application.Configurations;
using TenantProductManager.Infrastructure.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiConfig(builder.Configuration);
builder.Services.AddAplicationConfig();
builder.Services.AddRepositoriesConfig(builder.Configuration);

var app = builder.Build();

app.AddApiConfig();