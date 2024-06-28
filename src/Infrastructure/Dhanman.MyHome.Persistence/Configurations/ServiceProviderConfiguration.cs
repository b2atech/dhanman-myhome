using Dhanman.MyHome.Domain.Entities.ServiceProviders;
using Dhanman.MyHome.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dhanman.MyHome.Persistence.Configurations;

internal sealed class ServiceProviderConfiguration : IEntityTypeConfiguration<ServiceProvider>
{
    public void Configure(EntityTypeBuilder<ServiceProvider> builder)
    {
        builder.ToTable(TableNames.ServiceProviders);
        builder.HasKey(serviceProviders => serviceProviders.Id);

        builder.Property(serviceProviders => serviceProviders.FirstName).HasColumnName("first_name").IsRequired();

        builder.Property(serviceProviders => serviceProviders.LastName).HasColumnName("last_name").IsRequired();

        builder.Property(serviceProviders => serviceProviders.Email).HasColumnName("email").IsRequired();

        builder.Property(serviceProviders => serviceProviders.VisitingFrom).HasColumnName("visiting_from").IsRequired(); 
        
        builder.Property(serviceProviders => serviceProviders.ContactNumber).HasColumnName("contact_number").IsRequired();

        builder.Property(serviceProviders => serviceProviders.PermanentAddressId).HasColumnName("permanent_address_id").IsRequired();

        builder.Property(serviceProviders => serviceProviders.PresentAddressId).HasColumnName("present_address_id").IsRequired();

        builder.Property(serviceProviders => serviceProviders.ServiceProviderTypeId).HasColumnName("service_provider_type_id").IsRequired();

        builder.Property(serviceProviders => serviceProviders.VehicleNumber).HasColumnName("vehicle_number").IsRequired();

        builder.Property(serviceProviders => serviceProviders.IdentityTypeId).HasColumnName("identity_type_id").IsRequired();

        builder.Property(serviceProviders => serviceProviders.IdentityNumber).HasColumnName("identity_number").IsRequired();

        builder.Property(serviceProviders => serviceProviders.IdentityImage).HasColumnName("identity_image").HasColumnType("bytea").IsRequired(false);

        builder.Property(serviceProviders => serviceProviders.ApartmentId).HasColumnName("apartment_id").IsRequired();

        builder.Property(serviceProviders => serviceProviders.Pin).HasColumnName("pin").IsRequired();

        builder.Property(serviceProviders => serviceProviders.CreatedBy).HasColumnType("uuid");

        builder.Property(serviceProviders => serviceProviders.ModifiedBy).HasColumnType("uuid");

        builder.Property(serviceProviders => serviceProviders.CreatedOnUtc).HasColumnType("timestamp").IsRequired();

        builder.Property(serviceProviders => serviceProviders.ModifiedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(serviceProviders => serviceProviders.DeletedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(serviceProviders => serviceProviders.IsDeleted).HasDefaultValue(false).IsRequired();

        builder.HasQueryFilter(serviceProviders => !serviceProviders.IsDeleted);
    }
}