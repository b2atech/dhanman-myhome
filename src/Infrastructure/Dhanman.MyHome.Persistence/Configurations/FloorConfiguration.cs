using Dhanman.MyHome.Domain.Entities.Apartments;
using Dhanman.MyHome.Persistence.Constants;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Dhanman.MyHome.Persistence.Configurations;

internal sealed class FloorConfiguration : IEntityTypeConfiguration<Floor>
{
    public void Configure(EntityTypeBuilder<Floor> builder)
    {
        builder.ToTable(TableNames.Floors);
        builder.HasKey(floors => floors.Id);

        builder.Property(floors => floors.Name).HasColumnName("name").IsRequired();

        builder.Property(floors => floors.BuildingId).HasColumnName("building_id").IsRequired();

        builder.Property(floors => floors.TotalUnits).HasColumnName("total_units").IsRequired();

        builder.Property(floors => floors.CreatedBy).HasColumnType("uuid");

        builder.Property(floors => floors.ModifiedBy).HasColumnType("uuid");

        builder.Property(floors => floors.CreatedOnUtc).HasColumnType("timestamp").IsRequired();

        builder.Property(floors => floors.ModifiedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(floors => floors.DeletedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(floors => floors.IsDeleted).HasDefaultValue(false).IsRequired();

        builder.HasQueryFilter(floors => !floors.IsDeleted);
    }
}