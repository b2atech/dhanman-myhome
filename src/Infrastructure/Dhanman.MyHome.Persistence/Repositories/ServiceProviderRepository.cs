using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Domain.Entities.ServiceProviders;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using NpgsqlTypes;

namespace Dhanman.MyHome.Persistence.Repositories;

internal sealed class ServiceProviderRepository : IServiceProviderRepository
{
    #region Properties
    private readonly IApplicationDbContext _dbContext;
    #endregion

    #region Contructor
    public ServiceProviderRepository(IApplicationDbContext dbContext) => _dbContext = dbContext;
    #endregion

    #region Methods
    public DbSet<ServiceProvider> ServiceProvider => _dbContext.SetInt<ServiceProvider>();
    public async Task<ServiceProvider?> GetBydIdIntAsync(int id) => await _dbContext.GetBydIdIntAsync<ServiceProvider>(id);
    //public void Insert(ServiceProvider serviceProvider) => _dbContext.InsertInt(serviceProvider);
    public void Insert(ServiceProvider serviceProvider)
    {
        object[] xparams = {
        new NpgsqlParameter("@p_first_name", NpgsqlDbType.Varchar) { Value = serviceProvider.FirstName },
        new NpgsqlParameter("@p_last_name", NpgsqlDbType.Varchar) { Value = serviceProvider.LastName },
        new NpgsqlParameter("@p_email", NpgsqlDbType.Varchar) { Value = serviceProvider.Email },
        new NpgsqlParameter("@p_visiting_from", NpgsqlDbType.Varchar) { Value = serviceProvider.VisitingFrom },
        new NpgsqlParameter("@p_contact_number", NpgsqlDbType.Varchar) { Value = serviceProvider.ContactNumber },
        new NpgsqlParameter("@p_permanent_address_id", NpgsqlDbType.Uuid) { Value = serviceProvider.PermanentAddressId },
        new NpgsqlParameter("@p_present_address_id", NpgsqlDbType.Uuid) { Value = serviceProvider.PresentAddressId },
        new NpgsqlParameter("@p_service_provider_type_id", NpgsqlDbType.Integer) { Value = serviceProvider.ServiceProviderTypeId },
        new NpgsqlParameter("@p_service_provider_sub_type_id", NpgsqlDbType.Integer) { Value = serviceProvider.ServiceProviderSubTypeId },
        new NpgsqlParameter("@p_vehicle_number", NpgsqlDbType.Varchar) { Value = serviceProvider.VehicleNumber },
        new NpgsqlParameter("@p_identity_type_id", NpgsqlDbType.Integer) { Value = serviceProvider.IdentityTypeId },
        new NpgsqlParameter("@p_identity_number", NpgsqlDbType.Varchar) { Value = serviceProvider.IdentityNumber },
        new NpgsqlParameter("@p_validity_date", NpgsqlDbType.Date) { Value = serviceProvider.ValidityDate },
        new NpgsqlParameter("@p_police_verification_status", NpgsqlDbType.Boolean) { Value = serviceProvider.PoliceverificationStatus },                                                                                                          
        new NpgsqlParameter("@p_is_hireable", NpgsqlDbType.Boolean) { Value = serviceProvider.IsHireable },
        new NpgsqlParameter("@p_is_visible", NpgsqlDbType.Boolean) { Value = serviceProvider.IsVisible },
        new NpgsqlParameter("@p_is_frequent_visitor", NpgsqlDbType.Boolean) { Value = serviceProvider.IsFrequentVisitor },
        new NpgsqlParameter("@p_apartment_id", NpgsqlDbType.Uuid) { Value = serviceProvider.ApartmentId },
        new NpgsqlParameter("@p_created_by", NpgsqlDbType.Uuid) { Value = serviceProvider.CreatedBy }
    };

        _dbContext.Database.ExecuteSqlRaw("CALL public.create_service_provider(" +
            "@p_first_name, " +
            "@p_last_name, " +
            "@p_email, " +
            "@p_visiting_from, " +
            "@p_contact_number, " +
            "@p_permanent_address_id, " +
            "@p_present_address_id, " +
            "@p_service_provider_type_id, " +
            "@p_service_provider_sub_type_id, " +
            "@p_vehicle_number, " +
            "@p_identity_type_id, " +
            "@p_identity_number, " +
            "@p_validity_date, " +
            "@p_police_verification_status, " +
            "@p_is_hireable, " +
            "@p_is_visible, " +
            "@p_is_frequent_visitor, " +
            "@p_apartment_id, " +
            "@p_created_by)", xparams);
    }

    public void Delete(ServiceProvider serviceProvider) => _dbContext.RemoveInt(serviceProvider);
    public void Update(ServiceProvider serviceProvider) => _dbContext?.UpdateInt(serviceProvider);
   // public int GetTotalRecordsCount() => ServiceProvider.Count();

    #endregion
}