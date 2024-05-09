using Dhanman.MyHome.Domain.Entities.BookingFacilites;
using Dhanman.MyHome.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dhanman.MyHome.Persistence.Configurations;

internal sealed class BookingFacilitiesConfiguration : IEntityTypeConfiguration<BookingFacilitie>
{
    public void Configure(EntityTypeBuilder<BookingFacilitie> builder)
    {
        builder.ToTable(TableNames.BookingsFacilities);
        builder.HasKey(floors => floors.Id);

        builder.Property(floors => floors.Name).HasColumnName("name").IsRequired();

        builder.Property(floors => floors.BuildingId).HasColumnName("building_id").IsRequired();

        builder.Property(floors => floors.CreatedBy).HasColumnType("uuid");

        builder.Property(floors => floors.ModifiedBy).HasColumnType("uuid");

        builder.Property(floors => floors.CreatedOnUtc).HasColumnType("timestamp").IsRequired();

        builder.Property(floors => floors.ModifiedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(floors => floors.DeletedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(floors => floors.IsDeleted).HasDefaultValue(false).IsRequired();

        builder.HasQueryFilter(floors => !floors.IsDeleted);
    }
}
