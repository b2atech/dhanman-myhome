using Dhanman.MyHome.Persistence.Constants;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Dhanman.MyHome.Domain.Entities.Vehicals;

namespace Dhanman.MyHome.Persistence.Configurations;

internal sealed class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
{
    public void Configure(EntityTypeBuilder<Vehicle> builder)
    {
        builder.ToTable(TableNames.Vehicles);
        builder.HasKey(vehicles => vehicles.Id);

        builder.Property(vehicles => vehicles.VehicleNumber).HasColumnName("vehicle_number").IsRequired();

        builder.Property(vehicles => vehicles.VehicleTypeId).HasColumnName("vehicle_type_id").IsRequired();

        builder.Property(vehicles => vehicles.UnitId).HasColumnName("unit_id").IsRequired();

        builder.Property(vehicles => vehicles.VehicleRfid).HasColumnName("vehicle_rf_id");

        builder.Property(vehicles => vehicles.VehicleRfidSecretCode).HasColumnName("vehicle_rf_id_secretCode");        

        builder.Property(vehicles => vehicles.CreatedBy).HasColumnType("uuid");

        builder.Property(vehicles => vehicles.ModifiedBy).HasColumnType("uuid");

        builder.Property(vehicles => vehicles.CreatedOnUtc).HasColumnType("timestamp").IsRequired();

        builder.Property(vehicles => vehicles.ModifiedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(vehicles => vehicles.DeletedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(vehicles => vehicles.IsDeleted).HasDefaultValue(false).IsRequired();

        builder.HasQueryFilter(vehicles => !vehicles.IsDeleted);
    }
}