using B2aTech.CrossCuttingConcern.Abstractions;
using B2aTech.CrossCuttingConcern.Services;
using B2aTech.CrossCuttingConcern.UserContext;
using Dhanman.MyHome.Api;
using Dhanman.MyHome.Api.Middleware;
using Dhanman.MyHome.Api.Services;
using Dhanman.MyHome.Application;
using Dhanman.MyHome.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.OpenApi.Models;
using Prometheus;
using Serilog;
using System.IO.Compression;

var DhanManSpecificOrigins = "_dhanmanAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

// Add configuration files based on environment
builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

// Use Serilog, reading from configuration
builder.Host.UseSerilog((context, services, configuration) =>
{
    configuration
        .ReadFrom.Configuration(context.Configuration)
        .ReadFrom.Services(services)
        .Enrich.FromLogContext()
        .Enrich.WithProperty("Application_name", nameof(Dhanman))
        .Enrich.WithCorrelationIdHeader("correlation-id");
});

// Add services
builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
builder.Services.AddHeaderPropagation(options => options.Headers.Add("correlation-id"));
builder.Services.AddScoped<IUserContextService, UserContextService>();
builder.Services.AddScoped<IEmailService, EmailService>();

builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddPermissionService(builder.Configuration);
builder.Services.AddAuthentication(builder.Configuration, "");
builder.Services.AddCustomAuthorization();
builder.Services.AddApplication(builder.Configuration);
builder.Services.AddTemplateService(builder.Configuration);
builder.Services.AddHealthChecks();
builder.Services.AddScoped<RabbitMqCommandHandlers>();
builder.Services.AddScoped<RabbitMqEventHandlers>();


builder.Services.AddApiVersioning(config =>
{
    config.DefaultApiVersion = new ApiVersion(1, 0);
    config.AssumeDefaultVersionWhenUnspecified = true;
    config.ReportApiVersions = true;
    config.ApiVersionReader = ApiVersionReader.Combine(
        new HeaderApiVersionReader("x-api-version"),
        new QueryStringApiVersionReader("api-version"));
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
    options.Level = CompressionLevel.Fastest;
});

builder.Services.Configure<GzipCompressionProviderOptions>(options =>
{
    options.Level = CompressionLevel.Optimal;
});

// Swagger setup
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Community", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter the word 'Bearer' followed by space and JWT",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityDefinition("x-organization-id", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "x-organization-id",
        Type = SecuritySchemeType.ApiKey,
        Description = "Organization ID header"
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
             },
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "x-organization-id"
                }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Community");
        if (app.Environment.IsProduction())
            c.InjectJavascript("/swagger/custom.prod.js");
        else
            c.InjectJavascript("/swagger/custom.qa.js");
        c.InjectStylesheet("/swagger/SwaggerHeader.css");
        c.OAuthClientId(builder.Configuration["Auth0:ClientId"]);
        c.OAuthAppName("Demo API - Swagger");
        c.OAuthUsePkce();
    });
}
app.MapHealthChecks("/health");
// Configure middleware
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
app.UseResponseCompression();
app.UseHttpsRedirection();
app.UseRouting();
app.UseStaticFiles();
app.UseCors(DhanManSpecificOrigins);
app.UseAuthentication();
app.UseAuthorization();

// Metrics Middleware
app.UseHttpMetrics(); // <-- Add this to start capturing metrics

// Error Handling
app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapMetrics();
});


app.Run();
