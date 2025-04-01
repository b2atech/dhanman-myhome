using Dhanman.MyHome.Persistence.Constants;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Dhanman.MyHome.Domain.Entities.GuestApprovals;

namespace Dhanman.MyHome.Persistence.Configurations;

internal sealed class GuestApprovalConfiguration : IEntityTypeConfiguration<GuestApproval>
{
    public void Configure(EntityTypeBuilder<GuestApproval> builder)
    {
        builder.ToTable(TableNames.GuestApprovals);
        builder.HasKey(guestApproval => guestApproval.Id);

        builder.Property(guestApproval => guestApproval.VisitorLogId).HasColumnName("visitor_log_id");

        builder.Property(guestApproval => guestApproval.ApprovedBy).HasColumnName("approved_by");

        builder.Property(guestApproval => guestApproval.VisitorStatusId).HasColumnName("visitor_status_id");

        builder.Property(guestApproval => guestApproval.CreatedBy).HasColumnType("uuid");

        builder.Property(guestApproval => guestApproval.ModifiedBy).HasColumnType("uuid");

        builder.Property(guestApproval => guestApproval.CreatedOnUtc).HasColumnType("timestamp").IsRequired();

        builder.Property(guestApproval => guestApproval.ModifiedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(guestApproval => guestApproval.DeletedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(guestApproval => guestApproval.IsDeleted).HasDefaultValue(false).IsRequired();

        builder.HasQueryFilter(guestApproval => !guestApproval.IsDeleted);
    }
}
