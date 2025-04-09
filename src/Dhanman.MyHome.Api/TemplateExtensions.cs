using B2aTech.CrossCuttingConcern.Abstractions;
using B2aTech.CrossCuttingConcern.Persistence.Entities;
using B2aTech.CrossCuttingConcern.Persistence;
using B2aTech.CrossCuttingConcern.Services;
using Microsoft.EntityFrameworkCore;

namespace Dhanman.MyHome.Api;

public static class TemplateExtensions
{
    public static IServiceCollection AddTemplateService(this IServiceCollection services, IConfiguration configuration, string connectionString)
    {
        // Register TemplateDbContext
        services.AddDbContext<TemplateDbContext>(options =>
                   options
               .UseNpgsql(connectionString)
        .UseSnakeCaseNamingConvention()
                       .EnableSensitiveDataLogging());

        services.Configure<SmtpSettings>(configuration.GetSection("SmtpSettings"));

        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IEmailTemplateService, EmailTemplateService>();
        return services;
    }
}
