using Dhanman.MyHome.Persistence.Constants;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Dhanman.MyHome.Domain.Entities.Apartments;

namespace Dhanman.MyHome.Persistence.Configurations;

internal sealed class ApartmentConfiguration : IEntityTypeConfiguration<Apartment>
{
    public void Configure(EntityTypeBuilder<Apartment> builder)
    {
        builder.ToTable(TableNames.Apartments);
        builder.HasKey(apartments => apartments.Id);

        builder.Property(apartments => apartments.Name).HasColumnName("name").IsRequired();

        builder.Property(apartments => apartments.OrganizationId).HasColumnName("organization_id").HasColumnType("uuid");

        builder.Property(apartments => apartments.ApartmentTypeId).HasColumnName("apartment_type_id");

        builder.Property(apartments => apartments.AddressId).HasColumnName("address_id");

        builder.Property(apartments => apartments.Phone).HasColumnName("phone").IsRequired();

        builder.Property(apartments => apartments.PAN).HasColumnName("pan");

        builder.Property(apartments => apartments.TAN).HasColumnName("tan");

        builder.Property(apartments => apartments.AssociationName).HasColumnName("association_name").IsRequired();       

        builder.Property(apartments => apartments.CreatedBy).HasColumnType("uuid");

        builder.Property(apartments => apartments.ModifiedBy).HasColumnType("uuid");

        builder.Property(apartments => apartments.CreatedOnUtc).HasColumnType("timestamp").IsRequired();

        builder.Property(apartments => apartments.ModifiedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(apartments => apartments.DeletedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(apartments => apartments.IsDeleted).HasDefaultValue(false).IsRequired();

        builder.HasQueryFilter(apartments => !apartments.IsDeleted);
    }
}