using Dhanman.MyHome.Domain.Entities.VisitorVehicles;
using Dhanman.MyHome.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dhanman.MyHome.Persistence.Configurations;

internal sealed class VisitorVehiclesConfiguration : IEntityTypeConfiguration<VisitorVehicles>
{
    public void Configure(EntityTypeBuilder<VisitorVehicles> builder)
    {
        builder.ToTable(TableNames.VisitorVehicles);
        builder.HasKey(visitorVehicles => visitorVehicles.Id);

        builder.Property(visitorVehicles => visitorVehicles.VisitorLogId).HasColumnName("visitor_log_id").IsRequired();

        builder.Property(visitorVehicles => visitorVehicles.VehicleNumber).HasColumnName("visitor_number").IsRequired();

        builder.Property(visitorVehicles => visitorVehicles.VehicleType).HasColumnName("vehicle_type").IsRequired();

        builder.Property(visitorVehicles => visitorVehicles.CreatedBy).HasColumnType("uuid");

        builder.Property(visitorVehicles => visitorVehicles.ModifiedBy).HasColumnType("uuid");

        builder.Property(visitorVehicles => visitorVehicles.CreatedOnUtc).HasColumnType("timestamp").IsRequired();

        builder.Property(visitorVehicles => visitorVehicles.ModifiedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(visitorVehicles => visitorVehicles.DeletedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(visitorVehicles => visitorVehicles.IsDeleted).HasDefaultValue(false).IsRequired();

        builder.HasQueryFilter(visitorVehicles => !visitorVehicles.IsDeleted);
    }
}
