using Dhanman.MyHome.Domain.Entities.Apartments;
using Dhanman.MyHome.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dhanman.MyHome.Persistence.Configurations;

internal sealed class ResidentConfiguration : IEntityTypeConfiguration<Resident>
{
    public void Configure(EntityTypeBuilder<Resident> builder)
    {
        builder.ToTable(TableNames.Residents);
        builder.HasKey(residents => residents.Id);        

        builder.Property(residents => residents.ApartmentId).HasColumnName("apartment_id").IsRequired();

        builder.Property(residents => residents.BuildingId).HasColumnName("building_id").IsRequired();

        builder.Property(residents => residents.FloorId).HasColumnName("floor_id").IsRequired();

        builder.Property(residents => residents.UnitId).HasColumnName("unit_id").IsRequired();

        builder.Property(residents => residents.FirstName).HasColumnName("first_name").IsRequired();

        builder.Property(residents => residents.LastName).HasColumnName("last_name").IsRequired();

        builder.Property(residents => residents.Email).HasColumnName("email").IsRequired();

        builder.Property(residents => residents.ContactNumber).HasColumnName("contact_number").IsRequired();

        builder.Property(residents => residents.PermanentAddressId).HasColumnName("permanent_address_id").IsRequired();

        builder.Property(residents => residents.RequestStatusId).HasColumnName("request_status_id").IsRequired();

        builder.Property(residents => residents.ResidentTypeId).HasColumnName("resident_type_id").IsRequired();

        builder.Property(residents => residents.OccupancyStatusId).HasColumnName("occupancy_status_id").IsRequired();

        builder.Property(residents => residents.CreatedBy).HasColumnType("uuid");

        builder.Property(residents => residents.ModifiedBy).HasColumnType("uuid");

        builder.Property(residents => residents.CreatedOnUtc).HasColumnType("timestamp").IsRequired();

        builder.Property(residents => residents.ModifiedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(residents => residents.DeletedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(residents => residents.IsDeleted).HasDefaultValue(false).IsRequired();

        builder.HasQueryFilter(residents => !residents.IsDeleted);
    }
}