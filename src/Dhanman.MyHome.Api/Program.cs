using Azure.Identity;
using B2aTech.CrossCuttingConcern.Abstractions;
using B2aTech.CrossCuttingConcern.UserContext;
using Dhanman.MyHome.Api;
using Dhanman.MyHome.Api.Middleware;
using Dhanman.MyHome.Application;
using Dhanman.MyHome.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Azure.KeyVault;
using Microsoft.OpenApi.Models;
using Serilog;
using System.IO.Compression;

var DhanManSpecificOrigins = "_dhanmanAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

var logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .Enrich.FromLogContext()
            .Enrich.WithProperty("Application_name", nameof(Dhanman))
            .Enrich.WithCorrelationIdHeader("correlation-id")
                .WriteTo.ApplicationInsights(
    new Microsoft.ApplicationInsights.Extensibility.TelemetryConfiguration
    {
        ConnectionString = "InstrumentationKey=ae98c694-ba50-4047-b24d-545377d1dd32;IngestionEndpoint=https://southindia-0.in.applicationinsights.azure.com/;LiveEndpoint=https://southindia.livediagnostics.monitor.azure.com/;ApplicationId=87b507a0-428e-4b59-9bc2-08dd4b2a03e2"
    },
    new Serilog.Sinks.ApplicationInsights.TelemetryConverters.TraceTelemetryConverter()
)
            .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

builder.Services.AddEndpointsApiExplorer();

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

    // Retrieve and set connection strings from Key Vault
    var secretMyHomeDb = await keyVaultClient.GetSecretAsync(keyVaultEndpoint.ToString(), "MyHomeDb");
    builder.Configuration["ConnectionStrings:MyHomeDb"] = secretMyHomeDb.Value;

    var secretPermissionsDb = await keyVaultClient.GetSecretAsync(keyVaultEndpoint.ToString(), "PermissionsDb");
    builder.Configuration["ConnectionStrings:PermissionsDb"] = secretPermissionsDb.Value;
}

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();
builder.Services.AddHeaderPropagation(options => options.Headers.Add("correlation-id"));
builder.Services.AddScoped<IUserContextService, UserContextService>();


// Add application services
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddPermissionService(builder.Configuration);
builder.Services.AddAuthentication(builder.Configuration, "");
builder.Services.AddCustomAuthorization();
builder.Services.AddApplication(builder.Configuration);
builder.Services.AddTemplateService(builder.Configuration, builder.Configuration["ConnectionStrings:CommonDb"]);

builder.Services.AddApiVersioning(config =>
{
    config.DefaultApiVersion = new ApiVersion(1, 0);
    config.AssumeDefaultVersionWhenUnspecified = true;
    config.ReportApiVersions = true;
    config.ApiVersionReader = ApiVersionReader.Combine(new HeaderApiVersionReader("x-api-version"), new QueryStringApiVersionReader("api-version"));
});

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: DhanManSpecificOrigins, policy =>
    {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
    options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
        new[] { "application/json", "application/xml", "text/html", "text/plain" });
});

builder.Services.Configure<BrotliCompressionProviderOptions>(options =>
{
    options.Level = CompressionLevel.Fastest; // Use Optimal for better compression
});

builder.Services.Configure<GzipCompressionProviderOptions>(options =>
{
    options.Level = CompressionLevel.Optimal;
});

// Add Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter into field the word 'Bearer' followed by space and JWT",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            new string[] { }
        }
    });
});

var app = builder.Build();

if (!app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.OAuthClientId(builder.Configuration["Auth0:ClientId"]);
        c.OAuthAppName("Demo API - Swagger");
        c.OAuthUsePkce();
    });
}

// Configure middleware
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
app.UseResponseCompression();
app.UseHttpsRedirection();
app.UseRouting();
app.UseCors(DhanManSpecificOrigins);
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<ExceptionHandlerMiddleware>();
app.UseEndpoints(endpoints => endpoints.MapControllers());

app.Run();
