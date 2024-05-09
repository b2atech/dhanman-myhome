using Dhanman.MyHome.Persistence.Constants;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Dhanman.MyHome.Domain.Entities.GateTypes;
using Dhanman.MyHome.Domain.Entities.CommonEntities;

namespace Dhanman.MyHome.Persistence.Configurations;

internal class GateTypeConfiguration : IEntityTypeConfiguration<GateType>
{
    public void Configure(EntityTypeBuilder<GateType> builder)
    {
        builder.ToTable(TableNames.GateTypes);
        builder.HasKey(gateType => gateType.Id);

        builder.Property(gateType => gateType.Name).HasColumnName("name").HasMaxLength(Name.MaxLength).IsRequired();

        builder.Property(gateType => gateType.DeletedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(gateType => gateType.IsDeleted).HasDefaultValue(false).IsRequired();

        builder.HasQueryFilter(gateType => !gateType.IsDeleted);

    }
}