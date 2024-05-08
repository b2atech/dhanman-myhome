using Dhanman.MyHome.Persistence.Constants;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Dhanman.MyHome.Domain.Entities.VisitorLogs;

namespace Dhanman.MyHome.Persistence.Configurations;

internal sealed class VisitorLogConfiguration : IEntityTypeConfiguration<VisitorLog>
{
    public void Configure(EntityTypeBuilder<VisitorLog> builder)
    {
        builder.ToTable(TableNames.VisitorLogs);
        builder.HasKey(visitorLogs => visitorLogs.Id);

        builder.Property(visitorLogs => visitorLogs.VisitorId).HasColumnName("visitor_id").IsRequired();

        builder.Property(visitorLogs => visitorLogs.VisitingUnitId).HasColumnName("visiting_unit_id").IsRequired();

        builder.Property(visitorLogs => visitorLogs.VisitPurposeId).HasColumnName("visit_purpose_id").IsRequired();

        builder.Property(visitorLogs => visitorLogs.VisitingFrom).HasColumnName("visiting_from").IsRequired();

        builder.Property(visitorLogs => visitorLogs.CurrentStatusId).HasColumnName("current_status_id").IsRequired();

        builder.Property(visitorLogs => visitorLogs.EntryTime).HasColumnName("entry_time").IsRequired();

        builder.Property(visitorLogs => visitorLogs.ExitTime).HasColumnName("exit_time").IsRequired();

        builder.Property(visitorLogs => visitorLogs.CreatedBy).HasColumnType("uuid");

        builder.Property(visitorLogs => visitorLogs.ModifiedBy).HasColumnType("uuid");

        builder.Property(visitorLogs => visitorLogs.CreatedOnUtc).HasColumnType("timestamp").IsRequired();

        builder.Property(visitorLogs => visitorLogs.ModifiedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(visitorLogs => visitorLogs.DeletedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(visitorLogs => visitorLogs.IsDeleted).HasDefaultValue(false).IsRequired();

        builder.HasQueryFilter(visitorLogs => !visitorLogs.IsDeleted);
    }
}