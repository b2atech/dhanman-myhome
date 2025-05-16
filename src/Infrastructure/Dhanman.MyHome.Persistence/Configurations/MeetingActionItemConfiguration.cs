using Dhanman.MyHome.Domain.Entities.MeetingActionItems;
using Dhanman.MyHome.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dhanman.MyHome.Persistence.Configurations;

internal sealed class MeetingActionItemConfiguration : IEntityTypeConfiguration<MeetingActionItem>
{
    public void Configure(EntityTypeBuilder<MeetingActionItem> builder)
    {
        builder.ToTable(TableNames.MeetingActionItems);
        builder.HasKey(meetingActionItems => meetingActionItems.Id);

        builder.Property(meetingActionItems => meetingActionItems.OccurrenceId).HasColumnName("occurrence_id");

        builder.Property(meetingActionItems => meetingActionItems.ActionDescription).HasColumnName("action_description");

        builder.Property(meetingActionItems => meetingActionItems.AssignedToUserId).HasColumnName("assigned_to_user_id");

        builder.Property(meetingActionItems => meetingActionItems.DueDate).HasColumnName("due_date");

        builder.Property(meetingActionItems => meetingActionItems.Status).HasColumnName("status");

        builder.Property(meetingActionItems => meetingActionItems.CreatedBy).HasColumnType("uuid");

        builder.Property(meetingActionItems => meetingActionItems.ModifiedBy).HasColumnType("uuid");

        builder.Property(meetingActionItems => meetingActionItems.CreatedOnUtc).HasColumnType("timestamp").IsRequired();

        builder.Property(meetingActionItems => meetingActionItems.ModifiedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(meetingActionItems => meetingActionItems.DeletedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(meetingActionItems => meetingActionItems.IsDeleted).HasDefaultValue(false).IsRequired();

        builder.HasQueryFilter(meetingActionItems => !meetingActionItems.IsDeleted);
    }
}