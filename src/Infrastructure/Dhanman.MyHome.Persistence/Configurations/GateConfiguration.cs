using Dhanman.MyHome.Domain.Entities.Apartments;
using Dhanman.MyHome.Persistence.Constants;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Dhanman.MyHome.Persistence.Configurations;

internal sealed class GateConfiguration : IEntityTypeConfiguration<Gate>
{
    public void Configure(EntityTypeBuilder<Gate> builder)
    {
        builder.ToTable(TableNames.Gates);
        builder.HasKey(gates => gates.Id);

        builder.Property(gates => gates.Name).HasColumnName("name").IsRequired();

        builder.Property(gates => gates.ApartmentId).HasColumnName("apartment_id").IsRequired();

        builder.Property(gates => gates.BuildingId).HasColumnName("building_id").IsRequired();

        builder.Property(gates => gates.GateTypeId).HasColumnName("gate_type_id").IsRequired();

        builder.Property(gates => gates.IsUsedForIn).HasColumnName("is_used_for_in").HasDefaultValue(false).IsRequired();

        builder.Property(gates => gates.IsUsedForOut).HasColumnName("is_used_for_out").HasDefaultValue(false).IsRequired();

        builder.Property(gates => gates.IsAllUsersAllowed).HasColumnName("is_all_users_allowed").HasDefaultValue(false).IsRequired();

        builder.Property(gates => gates.IsResidentsAllowed).HasColumnName("is_residents_allowed").HasDefaultValue(false).IsRequired();

        builder.Property(gates => gates.IsStaffAllowed).HasColumnName("is_staff_allowed").HasDefaultValue(false).IsRequired();

        builder.Property(gates => gates.IsVendorAllowed).HasColumnName("is_vendor_allowed").HasDefaultValue(false).IsRequired();

        builder.Property(gates => gates.CreatedBy).HasColumnType("uuid");

        builder.Property(gates => gates.ModifiedBy).HasColumnType("uuid");

        builder.Property(gates => gates.CreatedOnUtc).HasColumnType("timestamp").IsRequired();

        builder.Property(gates => gates.ModifiedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(gates => gates.DeletedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(gates => gates.IsDeleted).HasDefaultValue(false).IsRequired();

        builder.HasQueryFilter(gates => !gates.IsDeleted);
    }
}