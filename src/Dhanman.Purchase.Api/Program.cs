using Dhanman.Infrastructure.Authentication;
using Dhanman.MyHome.Api.Extensions;
using Dhanman.MyHome.Api.Middleware;
using Dhanman.MyHome.Application;
using Dhanman.MyHome.Migrations.Core.Extensions;
using Dhanman.MyHome.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Serilog;

var DhanManSpecificOrigins = "_dhanmanAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddAuthentication(builder.Configuration);

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

var logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .Enrich.FromLogContext()
            .Enrich.WithProperty("Application_name", nameof(Dhanman))
            .Enrich.WithCorrelationIdHeader("correlation-id")
            .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

builder.Services.AddHttpContextAccessor();
builder.Services.AddHeaderPropagation(options => options.Headers.Add("correlation-id"));

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

app.UseSwagger();
app.UseSwaggerUI();


if (app.Environment.IsDevelopment())
{
    string connectionString = builder.Configuration.GetConnectionString(ConnectionString.SettingsKey);

    if (connectionString.Length > 0)
    {
        app.ExecuteMigrations(connectionString);
    }

    app.ApplyMigrations();
}

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

app.UseHttpsRedirection();
app.UseRouting();

app.UseRateLimiter();


app.UseCors(DhanManSpecificOrigins);

app.UseMiddleware<ExceptionHandlerMiddleware>();

//app.MapCarter();
app.UseEndpoints(endpoints => endpoints.MapControllers());

app.Run();
