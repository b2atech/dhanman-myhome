using B2aTech.CrossCuttingConcern.Core.Abstractions;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Persistence.Common;
using Dhanman.MyHome.Persistence.Data;
using Dhanman.MyHome.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Dhanman.MyHome.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        if (configuration != null)
        {
            string connectionString = configuration.GetConnectionString(ConnectionString.SettingsKey);

            services.AddSingleton(new ConnectionString(connectionString));

            if (connectionString.Length > 0)
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options
                        .UseNpgsql(connectionString)
                        .UseSnakeCaseNamingConvention()
                        .EnableSensitiveDataLogging());
            }

            services.AddTransient<IDateTime, MachineDateTime>();

            services.AddScoped<ISqlConnectionFactory, SqlConnectionFactory>();

            services.AddScoped<IDbExecutor, DbExecutor>();


            services.AddScoped<IApplicationDbContext>(sp =>
                sp.GetRequiredService<ApplicationDbContext>());

            services.AddScoped<IUnitOfWork>(sp =>
                sp.GetRequiredService<ApplicationDbContext>());
 
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IResidentRequestRepository, ResidentRequestRepository>();
            services.AddTransient<IResidentRepository, ResidentRepository>();
            services.AddTransient<IUnitRepository, UnitRepository>();
            services.AddTransient<IUnitTypeRepository, UnitTypeRepository>();
            services.AddTransient<IOccupancyTypeRepository, OccupancyTypeRepository>();
            services.AddTransient<IOccupantTypeRepository, OccupantTypeRepository>();
            services.AddTransient<IEventRepository, EventRepository>();
            services.AddTransient<IBookingFacilitesRepository, BookingFacilitesRepository>();
            services.AddTransient<IServiceProviderRepository, ServiceProviderRepository>();
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
        }
        return services;
    }
}
