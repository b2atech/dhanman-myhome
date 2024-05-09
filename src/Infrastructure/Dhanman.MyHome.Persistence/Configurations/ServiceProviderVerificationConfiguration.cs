using Dhanman.MyHome.Persistence.Constants;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Dhanman.MyHome.Domain.Entities.ServiceProviderVerifications;

namespace Dhanman.MyHome.Persistence.Configurations;

internal sealed class ServiceProviderVerificationConfiguration : IEntityTypeConfiguration<ServiceProviderVerification>
{
    public void Configure(EntityTypeBuilder<ServiceProviderVerification> builder)
    {
        builder.ToTable(TableNames.ServiceProviderVerifications);
        builder.HasKey(serviceProviderVerifications => serviceProviderVerifications.Id);

        builder.Property(serviceProviderVerifications => serviceProviderVerifications.Name).HasColumnName("name").IsRequired();

        builder.Property(serviceProviderVerifications => serviceProviderVerifications.ServiceProviderId).HasColumnName("service_Provider_id").IsRequired();

        builder.Property(serviceProviderVerifications => serviceProviderVerifications.VerificationTypeId).HasColumnName("verification_type_id").IsRequired();

        builder.Property(serviceProviderVerifications => serviceProviderVerifications.Comments).HasColumnName("comments").IsRequired();

        builder.Property(serviceProviderVerifications => serviceProviderVerifications.IsVerified).HasColumnName("is_verified").IsRequired();         

        builder.Property(serviceProviderVerifications => serviceProviderVerifications.CreatedBy).HasColumnType("uuid");

        builder.Property(serviceProviderVerifications => serviceProviderVerifications.ModifiedBy).HasColumnType("uuid");

        builder.Property(serviceProviderVerifications => serviceProviderVerifications.CreatedOnUtc).HasColumnType("timestamp").IsRequired();

        builder.Property(serviceProviderVerifications => serviceProviderVerifications.ModifiedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(serviceProviderVerifications => serviceProviderVerifications.DeletedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(serviceProviderVerifications => serviceProviderVerifications.IsDeleted).HasDefaultValue(false).IsRequired();

        builder.HasQueryFilter(serviceProviderVerifications => !serviceProviderVerifications.IsDeleted);
    }
}