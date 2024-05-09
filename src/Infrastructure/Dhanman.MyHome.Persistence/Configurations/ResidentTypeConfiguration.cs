using Dhanman.MyHome.Persistence.Constants;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Dhanman.MyHome.Domain.Entities.ResidentTypes;
using Dhanman.MyHome.Domain.Entities.CommonEntities;

namespace Dhanman.MyHome.Persistence.Configurations;

internal class ResidentTypeConfiguration : IEntityTypeConfiguration<ResidentType>
{
    public void Configure(EntityTypeBuilder<ResidentType> builder)
    {
        builder.ToTable(TableNames.ResidentTypes);
        builder.HasKey(residentType => residentType.Id);

        builder.Property(residentType => residentType.Name).HasColumnName("name").HasMaxLength(Name.MaxLength).IsRequired();

        builder.Property(residentType => residentType.DeletedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(residentType => residentType.IsDeleted).HasDefaultValue(false).IsRequired();

        builder.HasQueryFilter(residentType => !residentType.IsDeleted);

    }
}