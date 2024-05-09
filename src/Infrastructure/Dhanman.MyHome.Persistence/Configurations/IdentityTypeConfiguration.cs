using Dhanman.MyHome.Persistence.Constants;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Dhanman.MyHome.Domain.Entities.IdentityTypes;
using Dhanman.MyHome.Domain.Entities.CommonEntities;

namespace Dhanman.MyHome.Persistence.Configurations;

internal class IdentityTypeConfiguration : IEntityTypeConfiguration<IdentityType>
{
    public void Configure(EntityTypeBuilder<IdentityType> builder)
    {
        builder.ToTable(TableNames.IdentityTypes);
        builder.HasKey(identityType => identityType.Id);

        builder.Property(identityType => identityType.Name).HasColumnName("name").HasMaxLength(Name.MaxLength).IsRequired();

        builder.Property(identityType => identityType.DeletedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(identityType => identityType.IsDeleted).HasDefaultValue(false).IsRequired();

        builder.HasQueryFilter(identityType => !identityType.IsDeleted);

    }
}