using Dhanman.MyHome.Domain.Entities.VehicleTypes;
using Dhanman.MyHome.Persistence.Constants;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Dhanman.MyHome.Domain.Entities.VerificationTypes;
using Dhanman.MyHome.Domain.Entities.CommonEntities;

namespace Dhanman.MyHome.Persistence.Configurations;

internal class VerificationTypeConfiguration : IEntityTypeConfiguration<VerificationType>
{
    public void Configure(EntityTypeBuilder<VerificationType> builder)
    {
        builder.ToTable(TableNames.VerificationTypes);
        builder.HasKey(verificationType => verificationType.Id);

        builder.Property(verificationType => verificationType.Name).HasColumnName("name").HasMaxLength(Name.MaxLength).IsRequired();

        builder.Property(verificationType => verificationType.DeletedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(verificationType => verificationType.IsDeleted).HasDefaultValue(false).IsRequired();

        builder.HasQueryFilter(verificationType => !verificationType.IsDeleted);

    }
}