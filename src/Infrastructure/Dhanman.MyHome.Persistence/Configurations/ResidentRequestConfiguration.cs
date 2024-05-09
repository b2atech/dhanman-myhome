using Dhanman.MyHome.Domain.Entities.ResidentRequests;
using Dhanman.MyHome.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dhanman.MyHome.Persistence.Configurations;

internal sealed class ResidentRequestConfiguration : IEntityTypeConfiguration<ResidentRequest>
{
    public void Configure(EntityTypeBuilder<ResidentRequest> builder)
    {
        builder.ToTable(TableNames.ResidentRequests);
        builder.HasKey(residentRequests => residentRequests.Id);        

        builder.Property(residentRequests => residentRequests.ApartmentId).HasColumnName("apartment_id").IsRequired();

        builder.Property(residentRequests => residentRequests.BuildingId).HasColumnName("building_id").IsRequired();

        builder.Property(residentRequests => residentRequests.FloorId).HasColumnName("floor_id").IsRequired();

        builder.Property(residentRequests => residentRequests.UnitId).HasColumnName("unit_id").IsRequired();

        builder.Property(residentRequests => residentRequests.FirstName).HasColumnName("first_name").IsRequired();

        builder.Property(residentRequests => residentRequests.LastName).HasColumnName("last_name").IsRequired();

        builder.Property(residentRequests => residentRequests.Email).HasColumnName("email").IsRequired();

        builder.Property(residentRequests => residentRequests.ContactNumber).HasColumnName("contact_number").IsRequired();

        builder.Property(residentRequests => residentRequests.PermanentAddressId).HasColumnName("permanent_address_id").IsRequired();

        builder.Property(residentRequests => residentRequests.RequestStatusId).HasColumnName("request_status_id").IsRequired();

        builder.Property(residentRequests => residentRequests.ResidentTypeId).HasColumnName("resident_type_id").IsRequired();

        builder.Property(residentRequests => residentRequests.OccupancyStatusId).HasColumnName("occupancy_status_id").IsRequired();

        builder.Property(residentRequests => residentRequests.CreatedBy).HasColumnType("uuid");

        builder.Property(residentRequests => residentRequests.ModifiedBy).HasColumnType("uuid");

        builder.Property(residentRequests => residentRequests.CreatedOnUtc).HasColumnType("timestamp").IsRequired();

        builder.Property(residentRequests => residentRequests.ModifiedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(residentRequests => residentRequests.DeletedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(residentRequests => residentRequests.IsDeleted).HasDefaultValue(false).IsRequired();

        builder.HasQueryFilter(residentRequests => !residentRequests.IsDeleted);
    }
}