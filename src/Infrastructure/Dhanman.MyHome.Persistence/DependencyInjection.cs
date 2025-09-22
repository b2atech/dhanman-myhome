using B2aTech.CrossCuttingConcern.Core.Abstractions;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Persistence.Common;
using Dhanman.MyHome.Persistence.Data;
using Dhanman.MyHome.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Dhanman.MyHome.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var logger = services.BuildServiceProvider().GetRequiredService<ILogger<ApplicationDbContext>>();

        if (configuration == null)
            throw new Exception("Configuration is null in AddPersistence.");

        
        if (configuration != null)
        {
            var connectionString = Environment.GetEnvironmentVariable(ConnectionString.SettingsKey)
                     ?? configuration.GetConnectionString(ConnectionString.SettingsKey);


            //string key = ConnectionString.SettingsKey;
            // string connectionString = configuration.GetConnectionString(key);

            //if (string.IsNullOrWhiteSpace(connectionString))
            //{
            //    Console.WriteLine($"[ERROR] Connection string for key '{key}' is missing.");
            //    Console.WriteLine($"[DEBUG] DOTNET_ENVIRONMENT: {configuration["DOTNET_ENVIRONMENT"]}");
            //    Console.WriteLine($"[DEBUG] Raw config value: {configuration["ConnectionStrings:" + key]}");
            //    throw new Exception($"Missing or empty connection string: '{key}'");
            //}

            services.AddSingleton(new ConnectionString(connectionString));

            if (connectionString.Length > 0)
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options
                        .UseNpgsql(connectionString)
                        .UseSnakeCaseNamingConvention()
                        .EnableSensitiveDataLogging()
                    // Adapt ILogger to Action<string> for EF Core logging
                    .LogTo(log => logger.LogInformation(log), LogLevel.Information)); // Use Action<string> here
            }

            services.AddTransient<IDateTime, MachineDateTime>();

            services.AddScoped<ISqlConnectionFactory, SqlConnectionFactory>();

            services.AddScoped<IDbExecutor, DbExecutor>();


            services.AddScoped<IApplicationDbContext>(sp =>
                sp.GetRequiredService<ApplicationDbContext>());

            services.AddScoped<IUnitOfWork>(sp =>
                sp.GetRequiredService<ApplicationDbContext>());
 
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserFcmTokenRepository, UserFcmTokenRepository>();
            services.AddScoped<IResidentRequestRepository, ResidentRequestRepository>();
            services.AddTransient<IResidentRepository, ResidentRepository>();
            services.AddTransient<IUnitRepository, UnitRepository>();
            services.AddTransient<IUnitTypeRepository, UnitTypeRepository>();
            services.AddTransient<IOccupancyTypeRepository, OccupancyTypeRepository>();
            services.AddTransient<IOccupantTypeRepository, OccupantTypeRepository>();
            services.AddTransient<IEventRepository, EventRepository>();
            services.AddTransient<IBookingFacilitesRepository, BookingFacilitesRepository>();
            services.AddTransient<IServiceProviderRepository, ServiceProviderRepository>(); services.AddTransient<IServiceProviderLogRepository, ServiceProviderLogRepository>();
            services.AddTransient<IAddressRepository, AddressRepository>();
            services.AddTransient<IUnitServiceProviderRespository, UnitServiceProviderRespository>();
            services.AddTransient<IComplaintRepository, ComplaintRepository>();
            services.AddTransient<IBuildingRepository, BuildingRepository>();
            services.AddTransient<IGateRepository, GateRepository>();
            services.AddTransient<IFloorRepository, FloorRepository>();
            services.AddTransient<IResidentUnitRepository, ResidentUnitRepository>();
            services.AddTransient<IVisitorRepository, VisitorRepository>();
            services.AddTransient<IVisitorLogRepository, VisitorLogRepository>();
            services.AddTransient<IVisitorUnitLogRepository, VisitorUnitLogRepository>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<ITicketRepository, TicketRepository>();
            services.AddScoped<IVisitorApprovalsRepository, VisitorApprovalsRepository>();
            services.AddScoped<ITicketServiceProviderOtpRepository, TicketServiceProviderOtpRepository>();
            services.AddScoped<IResidentTokenRepository, ResidentTokenRepository>();
            services.AddScoped<IMemberAdditionalDetailRepository, MemberAdditionalDetailRepository>();
            services.AddScoped<IMeetingParticipantRepository, MeetingParticipantRepository>();
            services.AddScoped<ICommitteeMemberRepository, CommitteeMemberRepository>();
            services.AddScoped<IMeetingAgendaItemRepository, MeetingAgendaItemRepository>();
            services.AddScoped<IEventOccurrenceRepository, EventOccurrenceRepository>();
            services.AddScoped<IWaterTankerDeliveryRepository, WaterTankerDeliveryRepository>();
        }
        return services;
    }
}
