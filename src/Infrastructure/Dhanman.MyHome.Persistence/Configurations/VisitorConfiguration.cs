using Dhanman.MyHome.Persistence.Constants;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Dhanman.MyHome.Domain.Entities.Visitors;

namespace Dhanman.MyHome.Persistence.Configurations;

internal sealed class VisitorConfiguration : IEntityTypeConfiguration<Visitor>
{
    public void Configure(EntityTypeBuilder<Visitor> builder)
    {
        builder.ToTable(TableNames.Visitors);
        builder.HasKey(visitors => visitors.Id);

        builder.Property(visitors => visitors.FirstName).HasColumnName("first_name").IsRequired();

        builder.Property(visitors => visitors.LastName).HasColumnName("last_name").IsRequired();

        builder.Property(visitors => visitors.Email).HasColumnName("email").IsRequired();

        builder.Property(visitors => visitors.VisitingFrom).HasColumnName("visiting_from").IsRequired();

        builder.Property(visitors => visitors.ContactNumber).HasColumnName("contact_number").IsRequired();

        builder.Property(visitors => visitors.VisitorTypeId).HasColumnName("visitor_type_id").IsRequired();

        builder.Property(visitors => visitors.VehicleNumber).HasColumnName("vehicle_number").IsRequired();

        builder.Property(visitors => visitors.IdentityTypeId).HasColumnName("identity_type_id").IsRequired();

        builder.Property(visitors => visitors.IdentityNumber).HasColumnName("identity_number").IsRequired();

        builder.Property(visitors => visitors.CreatedBy).HasColumnType("uuid");

        builder.Property(visitors => visitors.ModifiedBy).HasColumnType("uuid");

        builder.Property(visitors => visitors.CreatedOnUtc).HasColumnType("timestamp").IsRequired();

        builder.Property(visitors => visitors.ModifiedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(visitors => visitors.DeletedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(visitors => visitors.IsDeleted).HasDefaultValue(false).IsRequired();

        builder.HasQueryFilter(visitors => !visitors.IsDeleted);
    }
}