using Dhanman.MyHome.Persistence.Constants;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Dhanman.MyHome.Domain.Entities.AppartmentTypes;
using Dhanman.MyHome.Domain.Entities.CommonEntities;


namespace Dhanman.MyHome.Persistence.Configurations;

internal class ApartmentTypeConfiguration : IEntityTypeConfiguration<ApartmentType>
{
    public void Configure(EntityTypeBuilder<ApartmentType> builder)
    {
        builder.ToTable(TableNames.ApartmentTypes);
        builder.HasKey(apartmentType => apartmentType.Id);

        builder.Property(apartmentType => apartmentType.Name).HasColumnName("name").HasMaxLength(Name.MaxLength).IsRequired();

        builder.Property(apartmentType => apartmentType.DeletedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(apartmentType => apartmentType.IsDeleted).HasDefaultValue(false).IsRequired();

        builder.HasQueryFilter(apartmentType => !apartmentType.IsDeleted);

    }
}