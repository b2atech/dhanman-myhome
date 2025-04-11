using B2aTech.CrossCuttingConcern.Authorization;
using B2aTech.CrossCuttingConcern.Persistence;
using Dhanman.MyHome.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using IDateTime = B2aTech.CrossCuttingConcern.Persistence.IDateTime;

namespace Dhanman.MyHome.Api;

public static class AuthorizationExtensions
{
    public static IServiceCollection AddPermissionService(
     this IServiceCollection services,
     IConfiguration configuration)
    {
        if (configuration != null)
        {
            var connectionString = Environment.GetEnvironmentVariable(ConnectionString.PermissionDBKey)
                     ?? configuration.GetConnectionString(ConnectionString.PermissionDBKey);

            if (connectionString.Length > 0)
            {
                services.AddDbContext<PermissionDbContext>(options =>
                    options
                .UseNpgsql(connectionString)
                .UseSnakeCaseNamingConvention()
                        .EnableSensitiveDataLogging());
            }

            services.AddMemoryCache();

            services.AddTransient<IDateTime, MachineDateTime>();

            services.AddScoped<IPermissionDbContextFactory, PermissionDbContextFactory>();

            services.AddScoped<IPermissionService, PermissionService>();



        }
        return services;
    }
    public static IServiceCollection AddCustomAuthorization(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy("DynamicPermissionPolicy", policy =>
                policy.Requirements.Add(new PermissionRequirement("DynamicPermission")));
        });

        services.AddScoped<IAuthorizationHandler, DynamicPermissionHandler>();

        return services;
    }
}
