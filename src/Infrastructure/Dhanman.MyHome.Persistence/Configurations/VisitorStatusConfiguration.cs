using Dhanman.MyHome.Domain.Entities.CommonEntities;
using Dhanman.MyHome.Domain.Entities.VisitorStatuses;
using Dhanman.MyHome.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dhanman.MyHome.Persistence.Configurations;

internal class VisitorStatusConfiguration : IEntityTypeConfiguration<VisitorStatus>
{
    public void Configure(EntityTypeBuilder<VisitorStatus> builder)
    {
        builder.ToTable(TableNames.VisitorStatuses);
        builder.HasKey(visitorStatuses => visitorStatuses.Id);

        builder.Property(visitorStatuses => visitorStatuses.Name).HasColumnName("name").HasMaxLength(Name.MaxLength).IsRequired();

        builder.Property(visitorStatuses => visitorStatuses.DeletedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(visitorStatuses => visitorStatuses.IsDeleted).HasDefaultValue(false).IsRequired();

        builder.HasQueryFilter(visitorStatuses => !visitorStatuses.IsDeleted);

    }
}