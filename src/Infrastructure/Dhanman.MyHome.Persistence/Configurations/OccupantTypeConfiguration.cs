using Dhanman.MyHome.Persistence.Constants;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Dhanman.MyHome.Domain.Entities.OccupantTypes;
using Dhanman.MyHome.Domain.Entities.CommonEntities;

namespace Dhanman.MyHome.Persistence.Configurations;

internal class OccupantTypeConfiguration : IEntityTypeConfiguration<OccupantType>
{
    public void Configure(EntityTypeBuilder<OccupantType> builder)
    {
        builder.ToTable(TableNames.OccupantTypes);
        builder.HasKey(occupantType => occupantType.Id);

        builder.Property(occupantType => occupantType.Name).HasColumnName("name").HasMaxLength(Name.MaxLength).IsRequired();

        builder.Property(occupantType => occupantType.DeletedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(occupantType => occupantType.IsDeleted).HasDefaultValue(false).IsRequired();

        builder.HasQueryFilter(occupantType => !occupantType.IsDeleted);

    }
}