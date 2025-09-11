using Dhanman.MyHome.Domain.Entities.ResidentUnits;
using Dhanman.MyHome.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dhanman.MyHome.Persistence.Configurations;

internal sealed class ResidentUnitConfiguration : IEntityTypeConfiguration<ResidentUnit>
{
    #region Methods
    public void Configure(EntityTypeBuilder<ResidentUnit> builder)
    {
        builder.ToTable(TableNames.ResidentUnits);

        builder.HasKey(resident => resident.Id);

        builder.Property(resident => resident.UnitId).HasColumnName("unit_id").IsRequired();

        builder.Property(resident => resident.ResidentId).HasColumnName("resident_id").IsRequired();

        builder.Property(resident => resident.IsPrimaryOwner).HasColumnName("is_primary_owner");

        builder.Property(resident => resident.CreatedBy).HasColumnType("uuid");

        builder.Property(resident => resident.CreatedOnUtc).HasColumnType("timestamp").IsRequired();

        builder.Ignore(resident => resident.ModifiedBy);

        builder.Ignore(resident => resident.ModifiedOnUtc);

        builder.Property(resident => resident.DeletedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(resident => resident.IsDeleted).HasDefaultValue(false).IsRequired();

        builder.HasQueryFilter(resident => !resident.IsDeleted);

    }
    #endregion
}
