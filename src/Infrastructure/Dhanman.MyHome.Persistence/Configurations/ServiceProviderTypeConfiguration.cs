using Dhanman.MyHome.Persistence.Constants;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Dhanman.MyHome.Domain.Entities.ServiceProviderTypes;
using Dhanman.MyHome.Domain.Entities.CommonEntities;

namespace Dhanman.MyHome.Persistence.Configurations;

internal class ServiceProviderTypeConfiguration : IEntityTypeConfiguration<ServiceProviderType>
{
    public void Configure(EntityTypeBuilder<ServiceProviderType> builder)
    {
        builder.ToTable(TableNames.ServiceProviderTypes);
        builder.HasKey(serviceProviderType => serviceProviderType.Id);

        builder.Property(serviceProviderType => serviceProviderType.Name).HasColumnName("name").HasMaxLength(Name.MaxLength).IsRequired();

        builder.Property(serviceProviderType => serviceProviderType.DeletedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(serviceProviderType => serviceProviderType.IsDeleted).HasDefaultValue(false).IsRequired();

        builder.HasQueryFilter(serviceProviderType => !serviceProviderType.IsDeleted);

    }
}