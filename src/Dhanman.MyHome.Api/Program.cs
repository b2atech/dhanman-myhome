using Azure.Identity;
using Dhanman.Infrastructure.Authentication;
using Dhanman.MyHome.Api;
using Dhanman.MyHome.Api.Middleware;
using Dhanman.MyHome.Application;
using Dhanman.MyHome.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Azure.KeyVault;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

var DhanManSpecificOrigins = "_dhanmanAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

var logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .Enrich.FromLogContext()
            .Enrich.WithProperty("Application_name", nameof(Dhanman))
            .Enrich.WithCorrelationIdHeader("correlation-id")
            .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add configuration to load environment-specific settings
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                     .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);

// Add Azure Key Vault configuration
if (builder.Environment.EnvironmentName == "Production")
{
    var keyVaultEndpoint = new Uri(builder.Configuration["KeyVaultUri"]);
    var keyVaultClient = new KeyVaultClient(async (authority, resource, scope) =>
    {
        var credential = new DefaultAzureCredential();
        var token = await credential.GetTokenAsync(new Azure.Core.TokenRequestContext(new[] { "https://vault.azure.net/.default" }));
        return token.Token;
    });

    // Retrieve first connection string
    var secretMyHomeDb = await keyVaultClient.GetSecretAsync(keyVaultEndpoint.ToString(), "MyHomeDb");
    var connectionStringMyHomeDb = secretMyHomeDb.Value;
    // Add first connection string to configuration
    builder.Configuration["ConnectionStrings:MyHomeDb"] = connectionStringMyHomeDb;

    // Retrieve second connection string
    var secretPermissionsDb = await keyVaultClient.GetSecretAsync(keyVaultEndpoint.ToString(), "PermissionsDb");
    var connectionStringPermissionsDb = secretPermissionsDb.Value;
    // Add second connection string to configuration
    builder.Configuration["ConnectionStrings:PermissionsDb"] = connectionStringPermissionsDb;
}


builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddPermissionService(builder.Configuration);
builder.Services.AddAuthentication(builder.Configuration,"");

builder.Services.AddApplication();
builder.Services.AddControllers();
//.AddFluentValidation();
//builder.Services.AddCarter();

builder.Services.AddApiVersioning(config =>
{
    config.DefaultApiVersion = new ApiVersion(1, 0);
    config.AssumeDefaultVersionWhenUnspecified = true;
    config.ReportApiVersions = true;
    config.ApiVersionReader = ApiVersionReader.Combine(new HeaderApiVersionReader("x-api-version"), new QueryStringApiVersionReader("api-version"));
});



builder.Services.AddHttpContextAccessor();
builder.Services.AddHeaderPropagation(options => options.Headers.Add("correlation-id"));


builder.Services.AddAuth0Authentication(builder.Configuration);
builder.Services.AddCustomAuthorization();


builder.Services.AddCors(options =>
{
    options.AddPolicy(name: DhanManSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("http://localhost:3000",
                                "http://localhost:7240",
                               "https://dev.dhanman.com");
        });
});

var app = builder.Build();

if (!app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


if (app.Environment.IsDevelopment())
{
    string connectionString = builder.Configuration.GetConnectionString(ConnectionString.SettingsKey);

    if (connectionString.Length > 0)
    {
        //app.ExecuteMigrations(connectionString);
    }

    //app.ApplyMigrations();
}

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

app.UseHttpsRedirection();
app.UseRouting();

//app.UseRateLimiter();


app.UseCors(DhanManSpecificOrigins);

app.UseMiddleware<ExceptionHandlerMiddleware>();

//app.MapCarter();
app.UseEndpoints(endpoints => endpoints.MapControllers());

app.Run();
