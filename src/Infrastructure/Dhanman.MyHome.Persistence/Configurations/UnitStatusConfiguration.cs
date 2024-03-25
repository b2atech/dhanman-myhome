using Dhanman.MyHome.Domain.Entities.Apartments;
using Dhanman.MyHome.Persistence.Constants;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Dhanman.MyHome.Persistence.Configurations;

internal class UnitStatusConfiguration : IEntityTypeConfiguration<UnitStatus>
{
    public void Configure(EntityTypeBuilder<UnitStatus> builder)
    {
        builder.ToTable(TableNames.UnitStatuses);
        builder.HasKey(unitStatus => unitStatus.Id);

        builder.Property(unitStatus => unitStatus.Name).HasColumnName("name").HasMaxLength(Name.MaxLength).IsRequired();

        builder.Property(unitStatus => unitStatus.DeletedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(unitStatus => unitStatus.IsDeleted).HasDefaultValue(false).IsRequired();

        builder.HasQueryFilter(unitStatus => !unitStatus.IsDeleted);

    }
}