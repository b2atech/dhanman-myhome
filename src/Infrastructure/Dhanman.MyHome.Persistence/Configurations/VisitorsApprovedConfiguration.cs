using Dhanman.MyHome.Domain.Entities.VisitorsApproved;
using Dhanman.MyHome.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dhanman.MyHome.Persistence.Configurations;

internal sealed class VisitorsApprovedConfiguration : IEntityTypeConfiguration<VisitorsApproved>
{
    public void Configure(EntityTypeBuilder<VisitorsApproved> builder)
    {
        builder.ToTable(TableNames.VisitorsApproved);
        builder.HasKey(visitorsApproveds => visitorsApproveds.Id);

        builder.Property(visitorsApproveds => visitorsApproveds.VisitorId).HasColumnName("visitor_id").IsRequired();

        builder.Property(visitorsApproveds => visitorsApproveds.VisitTypeId).HasColumnName("visit_type_id").IsRequired();

        builder.Property(visitorsApproveds => visitorsApproveds.StartDate).HasColumnName("start_date").IsRequired();

        builder.Property(visitorsApproveds => visitorsApproveds.EndDate).HasColumnName("end_date").IsRequired();

        builder.Property(visitorsApproveds => visitorsApproveds.EntryTime).HasColumnName("entry_time").IsRequired(false);

        builder.Property(visitorsApproveds => visitorsApproveds.ExitTime).HasColumnName("exit_time").IsRequired(false);

        builder.Property(visitorsApproveds => visitorsApproveds.CreatedBy).HasColumnType("uuid");

        builder.Property(visitorsApproveds => visitorsApproveds.ModifiedBy).HasColumnType("uuid").IsRequired(false);

        builder.Property(visitorsApproveds => visitorsApproveds.CreatedOnUtc).HasColumnType("timestamp").IsRequired();

        builder.Property(visitorsApproveds => visitorsApproveds.ModifiedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(visitorsApproveds => visitorsApproveds.DeletedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(visitorsApproveds => visitorsApproveds.IsDeleted).HasDefaultValue(false).IsRequired();

        builder.HasQueryFilter(visitorsApproveds => visitorsApproveds.IsDeleted);
    }
}
