using Dhanman.MyHome.Persistence.Constants;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Dhanman.MyHome.Domain.Entities.UnitVehicleLimits;

namespace Dhanman.MyHome.Persistence.Configurations;

internal sealed class UnitVehicleLimitConfiguration : IEntityTypeConfiguration<UnitVehicleLimit>
{
    public void Configure(EntityTypeBuilder<UnitVehicleLimit> builder)
    {
        builder.ToTable(TableNames.UnitVehicleLimits);
        builder.HasKey(unitVehicleLimits => unitVehicleLimits.Id);

        builder.Property(unitVehicleLimits => unitVehicleLimits.UnitId).HasColumnName("unit_id").IsRequired();

        builder.Property(unitVehicleLimits => unitVehicleLimits.CarLimit).HasColumnName("car_limit").IsRequired();

        builder.Property(unitVehicleLimits => unitVehicleLimits.TwoWheelarsLimit).HasColumnName("two_wheelars_limit").IsRequired();

        builder.Property(unitVehicleLimits => unitVehicleLimits.NoOfCarsAllotted).HasColumnName("no_of_cars_allotted").IsRequired();

        builder.Property(unitVehicleLimits => unitVehicleLimits.NoOfTwoWheelarsAllotted).HasColumnName("no_of_two_wheelars_allotted").IsRequired();       

        builder.Property(unitVehicleLimits => unitVehicleLimits.CreatedBy).HasColumnType("uuid");

        builder.Property(unitVehicleLimits => unitVehicleLimits.ModifiedBy).HasColumnType("uuid");

        builder.Property(unitVehicleLimits => unitVehicleLimits.CreatedOnUtc).HasColumnType("timestamp").IsRequired();

        builder.Property(unitVehicleLimits => unitVehicleLimits.ModifiedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(unitVehicleLimits => unitVehicleLimits.DeletedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(unitVehicleLimits => unitVehicleLimits.IsDeleted).HasDefaultValue(false).IsRequired();

        builder.HasQueryFilter(unitVehicleLimits => !unitVehicleLimits.IsDeleted);
    }
}