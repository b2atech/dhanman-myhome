using Dhanman.MyHome.Domain.Entities.VisitorUnitLogs;
using Dhanman.MyHome.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dhanman.MyHome.Persistence.Configurations;

internal sealed class VisitorUnitLogConfiguration : IEntityTypeConfiguration<VisitorUnitLog>
{
    public void Configure(EntityTypeBuilder<VisitorUnitLog> builder)
    {
        builder.ToTable(TableNames.VisitorUnitLogs);
        builder.HasKey(visitorUnitLogs => visitorUnitLogs.Id);

        builder.Property(visitorUnitLogs => visitorUnitLogs.VisitorLogId).HasColumnName("visitor_log_id").IsRequired();

        builder.Property(visitorUnitLogs => visitorUnitLogs.UnitId).HasColumnName("unit_id").IsRequired();   

        builder.Property(visitorUnitLogs => visitorUnitLogs.DeletedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(visitorUnitLogs => visitorUnitLogs.IsDeleted).HasDefaultValue(false).IsRequired();

        builder.HasQueryFilter(visitorUnitLogs => !visitorUnitLogs.IsDeleted);
    }
}
