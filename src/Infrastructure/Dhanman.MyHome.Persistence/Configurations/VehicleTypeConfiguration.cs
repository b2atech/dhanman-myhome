using Dhanman.MyHome.Persistence.Constants;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Dhanman.MyHome.Domain.Entities.VehicleTypes;
using Dhanman.MyHome.Domain.Entities.CommonEntities;

namespace Dhanman.MyHome.Persistence.Configurations;

internal class VehicleTypeConfiguration : IEntityTypeConfiguration<VehicleType>
{
    public void Configure(EntityTypeBuilder<VehicleType> builder)
    {
        builder.ToTable(TableNames.VehicleTypes);
        builder.HasKey(vehicleType => vehicleType.Id);

        builder.Property(vehicleType => vehicleType.Name).HasColumnName("name").HasMaxLength(Name.MaxLength).IsRequired();

        builder.Property(vehicleType => vehicleType.DeletedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(vehicleType => vehicleType.IsDeleted).HasDefaultValue(false).IsRequired();

        builder.HasQueryFilter(vehicleType => !vehicleType.IsDeleted);

    }
}