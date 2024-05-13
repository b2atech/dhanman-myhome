using Dhanman.MyHome.Domain.Entities.UnitServiceProviders;
using Dhanman.MyHome.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dhanman.MyHome.Persistence.Configurations;

internal class UnitServicerProviderConfiguration : IEntityTypeConfiguration<UnitServiceProvider>
{
    #region Methods
    public void Configure(EntityTypeBuilder<UnitServiceProvider> builder)
    {
        builder.ToTable(TableNames.UnitServiceProviders);
        builder.HasKey(occupantType => occupantType.Id);

        builder.Property(resident => resident.UnitId).HasColumnName("unit_id").IsRequired();

        builder.Property(occupantType => occupantType.ServiceProviderId).HasColumnName("service_provider_id").IsRequired();

        builder.Property(events => events.Start).HasColumnName("start").IsRequired();

        builder.Property(events => events.End).HasColumnName("end").IsRequired();

        builder.Property(resident => resident.CreatedBy).HasColumnType("uuid");

        builder.Property(resident => resident.ModifiedBy).HasColumnType("uuid");

        builder.Property(resident => resident.CreatedOnUtc).HasColumnType("timestamp").IsRequired();

        builder.Property(resident => resident.ModifiedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(resident => resident.DeletedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(resident => resident.IsDeleted).HasDefaultValue(false).IsRequired();

    }
    #endregion

}
