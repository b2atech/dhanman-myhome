using Dhanman.MyHome.Domain.Entities.Addresses;
using Dhanman.MyHome.Persistence.Constants;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Dhanman.MyHome.Persistence.Configurations;

internal sealed class AddressConfiguration : IEntityTypeConfiguration<Address>
{
    #region Methods
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.ToTable(TableNames.Addresses);
        builder.HasKey(address => address.Id);

        builder.Property(address => address.CountryId).HasColumnName("country_id").IsRequired();

        builder.Property(address => address.StateId).HasColumnName("state_id").IsRequired();

        builder.Property(address => address.CityId).HasColumnName("city_id").IsRequired();

        builder.Property(address => address.AddressLine1).HasColumnName("address_line1").IsRequired();

        builder.Property(address => address.AddressLine2).HasColumnName("address_line2").IsRequired(false);

        builder.Property(address => address.ZipCode).HasColumnName("zip_code").IsRequired();

        builder.Property(address => address.CreatedBy).HasColumnType("uuid");

        builder.Property(address => address.CreatedOnUtc).HasColumnType("timestamp").IsRequired();

        builder.Property(address => address.ModifiedBy).HasColumnType("uuid");

        builder.Property(address => address.ModifiedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(address => address.DeletedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(address => address.IsDeleted).HasDefaultValue(false).IsRequired();

        builder.Property(address => address.CreatedBy).HasColumnType("uuid");

        builder.Property(address => address.ModifiedBy).HasColumnType("uuid");

        builder.HasQueryFilter(address => !address.IsDeleted);

    }
    #endregion
}