using Dhanman.MyHome.Domain.Entities.Apartments;
using Dhanman.MyHome.Persistence.Constants;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Dhanman.MyHome.Persistence.Configurations;

internal sealed class UnitConfiguration : IEntityTypeConfiguration<Unit>
{
    public void Configure(EntityTypeBuilder<Unit> builder)
    {
        builder.ToTable(TableNames.Units);
        builder.HasKey(units => units.Id);

        builder.Property(units => units.Name).HasColumnName("name").IsRequired();

        builder.Property(units => units.BuildingId).HasColumnName("building_id").IsRequired();

        builder.Property(units => units.FloorId).HasColumnName("floor_id").IsRequired();

        builder.Property(units => units.UnitTypeId).HasColumnName("unit_type_id").IsRequired();

        builder.Property(units => units.AccountId).HasColumnName("account_id").IsRequired();

        builder.Property(units => units.CustomerId).HasColumnName("customer_id");

        builder.Property(units => units.Area).HasColumnName("area").IsRequired();

        builder.Property(units => units.BHKType).HasColumnName("bhk_type").IsRequired();

        builder.Property(units => units.OccupantTypeId).HasColumnName("occupant_type_id").IsRequired();

        builder.Property(units => units.OccupancyTypeId).HasColumnName("occupancy_type_id").IsRequired();

        builder.Property(units => units.PhoneExtention).HasColumnName("phone_extention").IsRequired();

        builder.Property(units => units.eIntercom).HasColumnName("e_intercom").IsRequired();

        builder.Property(units => units.Latitude).HasColumnName("latitude").IsRequired();

        builder.Property(units => units.Longitude).HasColumnName("longitude").IsRequired();

        builder.Property(units => units.CreatedBy).HasColumnType("uuid");

        builder.Property(units => units.ModifiedBy).HasColumnType("uuid");

        builder.Property(units => units.CreatedOnUtc).HasColumnType("timestamp").IsRequired();

        builder.Property(units => units.ModifiedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(units => units.DeletedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(units => units.IsDeleted).HasDefaultValue(false).IsRequired();

        builder.HasQueryFilter(units => !units.IsDeleted);
    }
}