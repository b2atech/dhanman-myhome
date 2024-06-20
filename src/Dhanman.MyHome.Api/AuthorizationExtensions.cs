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
            string connectionString = configuration.GetConnectionString(ConnectionString.PermissionDBKey);

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
        var manageUsersPermission = new PermissionRequirement("ManageUsers", new List<PermissionRequirement>
            {
                new PermissionRequirement("CreateUser",new List<PermissionRequirement>()),
                new PermissionRequirement("UpdateUser",new List<PermissionRequirement>()),
                new PermissionRequirement("DeleteUser",new List<PermissionRequirement>())
            });

        var adminPermission = new PermissionRequirement("Test", new List<PermissionRequirement>
            {
                manageUsersPermission
            });

        var tstPermission = new PermissionRequirement("read:coa", new List<PermissionRequirement>
            {
                manageUsersPermission
            });

        services.AddAuthorization(options =>
        {
            options.AddPolicy("ManageUsersPolicy", policy =>
            {
                policy.Requirements.Add(manageUsersPermission);
            });

            options.AddPolicy("AdminPolicy", policy =>
            {
                policy.Requirements.Add(adminPermission);
            });

            options.AddPolicy("coapolicy", policy =>
            {
                policy.Requirements.Add(tstPermission);
            });
        });

        // Register the handlers
        services.AddScoped<IAuthorizationHandler, PermissionHandler>();

        return services;
    }
}
