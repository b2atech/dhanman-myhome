using Dhanman.MyHome.Persistence.Constants;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Dhanman.MyHome.Domain.Entities.VisitorTypes;
using Dhanman.MyHome.Domain.Entities.CommonEntities;

namespace Dhanman.MyHome.Persistence.Configurations;

internal class VisitorTypeConfiguration : IEntityTypeConfiguration<VisitorType>
{
    public void Configure(EntityTypeBuilder<VisitorType> builder)
    {
        builder.ToTable(TableNames.VisitorTypes);
        builder.HasKey(visitorType => visitorType.Id);

        builder.Property(visitorType => visitorType.Name).HasColumnName("name").HasMaxLength(Name.MaxLength).IsRequired();

        builder.Property(visitorType => visitorType.DeletedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(visitorType => visitorType.IsDeleted).HasDefaultValue(false).IsRequired();

        builder.HasQueryFilter(visitorType => !visitorType.IsDeleted);

    }
}