using Dhanman.MyHome.Domain.Entities.Countries;
using Dhanman.MyHome.Persistence.Constants;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Dhanman.MyHome.Persistence.Configurations;
internal class CountryConfiguration : IEntityTypeConfiguration<Country>
{
    #region Methodes
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        builder.ToTable(TableNames.Countries);
        builder.HasKey(country => country.Id);

        builder.Property(country => country.Name).HasColumnName("name").IsRequired();

        builder.Property(country => country.ISOAlphaCode).HasColumnName("iso_alpha_code").IsRequired();

        builder.Property(country => country.CreatedBy).HasColumnType("uuid");

        builder.Property(country => country.CreatedOnUtc).HasColumnType("timestamp").IsRequired();

        builder.Property(country => country.ModifiedBy).HasColumnType("uuid");

        builder.Property(country => country.ModifiedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(country => country.DeletedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(country => country.IsDeleted).HasDefaultValue(false).IsRequired();

        builder.HasQueryFilter(country => !country.IsDeleted);
    }
    #endregion
}