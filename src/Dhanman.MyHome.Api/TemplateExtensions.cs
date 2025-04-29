using B2aTech.CrossCuttingConcern.Abstractions;
using B2aTech.CrossCuttingConcern.Persistence;
using B2aTech.CrossCuttingConcern.Services;
using B2aTech.CrossCuttingConcern.Settings;
using Dhanman.MyHome.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Dhanman.MyHome.Api;

public static class TemplateExtensions
{
    public static IServiceCollection AddTemplateService(this IServiceCollection services, IConfiguration configuration)
    {
        // Register TemplateDbContext
        var connectionString = Environment.GetEnvironmentVariable(ConnectionString.TemplateDBKey)
                    ?? configuration.GetConnectionString(ConnectionString.TemplateDBKey);
        services.AddDbContext<TemplateDbContext>(options =>
                   options
               .UseNpgsql(connectionString)
        .UseSnakeCaseNamingConvention()
                       .EnableSensitiveDataLogging());

        

        services.Configure<SmtpSettings>(configuration.GetSection("Smtp"));

        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IEmailTemplateService, EmailTemplateService>();
        services.AddScoped<TemplatedEmailService>();
        return services;

    }
}
