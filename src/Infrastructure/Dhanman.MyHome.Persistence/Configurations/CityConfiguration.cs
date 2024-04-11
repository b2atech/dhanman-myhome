using Dhanman.MyHome.Domain.Entities.Cities;
using Dhanman.MyHome.Persistence.Constants;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Dhanman.MyHome.Persistence.Configurations;
internal sealed class CityConfiguration : IEntityTypeConfiguration<City>
{
    #region Methods

    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder.ToTable(TableNames.Cities);
        builder.HasKey(city => city.Id);

        builder.Property(city => city.StateId).HasColumnName("state_id").IsRequired();

        builder.Property(city => city.Name).HasColumnName("name").IsRequired();

        builder.Property(city => city.ZipCode).HasColumnName("zip_code").IsRequired();

        builder.Property(city => city.CreatedBy).HasColumnType("uuid");

        builder.Property(city => city.CreatedOnUtc).HasColumnType("timestamp").IsRequired();

        builder.Property(city => city.ModifiedBy).HasColumnType("uuid");

        builder.Property(city => city.ModifiedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(city => city.DeletedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(city => city.IsDeleted).HasDefaultValue(false).IsRequired();

        builder.HasQueryFilter(city => !city.IsDeleted);
    }

    #endregion
}