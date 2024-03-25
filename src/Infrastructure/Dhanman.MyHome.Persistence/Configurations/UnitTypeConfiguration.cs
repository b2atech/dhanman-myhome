using Dhanman.MyHome.Domain.Entities.Apartments;
using Dhanman.MyHome.Persistence.Constants;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Dhanman.MyHome.Persistence.Configurations;

internal class UnitTypeConfiguration : IEntityTypeConfiguration<UnitType>
{
    public void Configure(EntityTypeBuilder<UnitType> builder)
    {
        builder.ToTable(TableNames.UnitTypes);
        builder.HasKey(unitType => unitType.Id);

        builder.Property(unitType => unitType.Name).HasColumnName("name").HasMaxLength(Name.MaxLength).IsRequired();

        builder.Property(unitType => unitType.DeletedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(unitType => unitType.IsDeleted).HasDefaultValue(false).IsRequired();

        builder.HasQueryFilter(unitType => !unitType.IsDeleted);

    }
}