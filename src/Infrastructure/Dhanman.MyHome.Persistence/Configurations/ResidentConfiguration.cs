using Dhanman.MyHome.Persistence.Constants;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Dhanman.MyHome.Domain.Entities.Residents;

namespace Dhanman.MyHome.Persistence.Configurations;

internal sealed class ResidentConfiguration : IEntityTypeConfiguration<Resident>
{
    public void Configure(EntityTypeBuilder<Resident> builder)
    {
        builder.ToTable(TableNames.Residents);
        builder.HasKey(resident => resident.Id);

        builder.Property(resident => resident.ApartmentId).HasColumnName("apartment_id").HasColumnType("uuid");

        builder.Property(resident => resident.FirstName).HasColumnName("first_name").IsRequired();

        builder.Property(resident => resident.LastName).HasColumnName("last_name").IsRequired();

        builder.Property(resident => resident.Email).HasColumnName("email").IsRequired();

        builder.Property(resident => resident.ContactNumber).HasColumnName("contact_number").IsRequired();

        builder.Property(resident => resident.PermanentAddressId).HasColumnName("permanent_address_id").IsRequired(false);

        builder.Property(resident => resident.ResidentTypeId).HasColumnName("resident_type_id").IsRequired();

        builder.Property(resident => resident.OccupancyStatusId).HasColumnName("occupancy_status_id").IsRequired();

        builder.Property(resident => resident.CreatedBy).HasColumnType("uuid");

        builder.Property(resident => resident.ModifiedBy).HasColumnType("uuid");

        builder.Property(resident => resident.CreatedOnUtc).HasColumnType("timestamp").IsRequired();

        builder.Property(resident => resident.ModifiedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(resident => resident.DeletedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(resident => resident.IsDeleted).HasDefaultValue(false).IsRequired();

        builder.HasQueryFilter(resident => !resident.IsDeleted);
    }
}