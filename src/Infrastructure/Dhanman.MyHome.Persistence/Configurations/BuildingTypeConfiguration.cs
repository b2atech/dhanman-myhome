using Dhanman.MyHome.Persistence.Constants;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Dhanman.MyHome.Domain.Entities.BuildingTypes;
using Dhanman.MyHome.Domain.Entities.CommonEntities;

namespace Dhanman.MyHome.Persistence.Configurations;

internal class BuildingTypeConfiguration : IEntityTypeConfiguration<BuildingType>
{
    public void Configure(EntityTypeBuilder<BuildingType> builder)
    {
        builder.ToTable(TableNames.BuildingTypes);
        builder.HasKey(buildingType => buildingType.Id);

        builder.Property(buildingType => buildingType.Name).HasColumnName("name").HasMaxLength(Name.MaxLength).IsRequired();

        builder.Property(buildingType => buildingType.DeletedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(buildingType => buildingType.IsDeleted).HasDefaultValue(false).IsRequired();

        builder.HasQueryFilter(buildingType => !buildingType.IsDeleted);

    }
}