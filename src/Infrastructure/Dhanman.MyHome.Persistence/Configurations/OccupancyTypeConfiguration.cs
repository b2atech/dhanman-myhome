using Dhanman.MyHome.Persistence.Constants;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Dhanman.MyHome.Domain.Entities.OccupancyTypes;
using Dhanman.MyHome.Domain.Entities.CommonEntities;

namespace Dhanman.MyHome.Persistence.Configurations;

internal class OccupancyTypeConfiguration : IEntityTypeConfiguration<OccupancyType>
{
    public void Configure(EntityTypeBuilder<OccupancyType> builder)
    {
        builder.ToTable(TableNames.OccupancyTypes);
        builder.HasKey(occupancyType => occupancyType.Id);

        builder.Property(occupancyType => occupancyType.Name).HasColumnName("name").HasMaxLength(Name.MaxLength).IsRequired();

        builder.Property(occupancyType => occupancyType.DeletedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(occupancyType => occupancyType.IsDeleted).HasDefaultValue(false).IsRequired();

        builder.HasQueryFilter(occupancyType => !occupancyType.IsDeleted);

    }
}