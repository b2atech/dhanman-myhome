using Dhanman.MyHome.Domain.Entities.FacilityBookings;
using Dhanman.MyHome.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dhanman.MyHome.Persistence.Configurations;

internal sealed class FacilityBookingConfiguration : IEntityTypeConfiguration<FacilityBooking>
{
    public void Configure(EntityTypeBuilder<FacilityBooking> builder)
    {
        builder.ToTable(TableNames.FacilityBookings);
        builder.HasKey(facilityBooking => facilityBooking.Id);

        builder.Property(facilityBooking => facilityBooking.Name).HasColumnName("name").IsRequired();

        builder.Property(facilityBooking => facilityBooking.Name).HasColumnName("name").IsRequired();

        builder.Property(facilityBooking => facilityBooking.BuildingId).HasColumnName("building_id").IsRequired();

        builder.Property(facilityBooking => facilityBooking.CreatedBy).HasColumnType("uuid");

        builder.Property(facilityBooking => facilityBooking.ModifiedBy).HasColumnType("uuid");

        builder.Property(facilityBooking => facilityBooking.CreatedOnUtc).HasColumnType("timestamp").IsRequired();

        builder.Property(facilityBooking => facilityBooking.ModifiedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(facilityBooking => facilityBooking.DeletedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(facilityBooking => facilityBooking.IsDeleted).HasDefaultValue(false).IsRequired();

        builder.HasQueryFilter(facilityBooking => !facilityBooking.IsDeleted);
    }
}
