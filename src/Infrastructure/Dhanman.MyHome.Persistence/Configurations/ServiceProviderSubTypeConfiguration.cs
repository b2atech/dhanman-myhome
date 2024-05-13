using Dhanman.MyHome.Domain.Entities.CommonEntities;
using Dhanman.MyHome.Domain.Entities.ServiceProviderSubTypes;
using Dhanman.MyHome.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dhanman.MyHome.Persistence.Configurations;

internal sealed class ServiceProviderSubTypeConfiguration : IEntityTypeConfiguration<ServiveProviderSubType>
{
    public void Configure(EntityTypeBuilder<ServiveProviderSubType> builder)
    {
        builder.ToTable(TableNames.ServiceProviderSubTypes);
        builder.HasKey(serviceProviderSubType => serviceProviderSubType.Id);

        builder.HasKey(serviceProviderSubType => serviceProviderSubType.ServiceProviderTypeId);

        builder.Property(serviceProviderSubType => serviceProviderSubType.Name).HasColumnName("name").HasMaxLength(Name.MaxLength).IsRequired();
        builder.Property(serviceProviderSubType => serviceProviderSubType.CreatedBy).HasColumnType("uuid");

        builder.Property(serviceProviderSubType => serviceProviderSubType.ModifiedBy).HasColumnType("uuid");

        builder.Property(serviceProviderSubType => serviceProviderSubType.DeletedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(serviceProviderSubType => serviceProviderSubType.IsDeleted).HasDefaultValue(false).IsRequired();

        builder.HasQueryFilter(serviceProviderSubType => !serviceProviderSubType.IsDeleted);

    }
}
