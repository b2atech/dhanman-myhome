using Dhanman.MyHome.Persistence.Constants;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Dhanman.MyHome.Domain.Entities.Buildings;

namespace Dhanman.MyHome.Persistence.Configurations;

internal sealed class BuildingConfiguration : IEntityTypeConfiguration<Building>
{
    public void Configure(EntityTypeBuilder<Building> builder)
    {
        builder.ToTable(TableNames.Buildings);
        builder.HasKey(buildings => buildings.Id);

        builder.Property(buildings => buildings.Name).HasColumnName("name").IsRequired();

        builder.Property(buildings => buildings.BuildingTypeId).HasColumnName("building_type_id").IsRequired();

        builder.Property(buildings => buildings.ApartmentId).HasColumnName("apartment_id").IsRequired();

        builder.Property(buildings => buildings.TotalUnits).HasColumnName("total_units").IsRequired();

        builder.Property(buildings => buildings.CreatedBy).HasColumnType("uuid");

        builder.Property(buildings => buildings.ModifiedBy).HasColumnType("uuid");

        builder.Property(buildings => buildings.CreatedOnUtc).HasColumnType("timestamp").IsRequired();

        builder.Property(buildings => buildings.ModifiedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(buildings => buildings.DeletedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(buildings => buildings.IsDeleted).HasDefaultValue(false).IsRequired();

        builder.HasQueryFilter(buildings => !buildings.IsDeleted);
    }
}