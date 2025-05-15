using Dhanman.MyHome.Domain.Entities.MeetingParticipants;
using Dhanman.MyHome.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dhanman.MyHome.Persistence.Configurations;

internal sealed class MeetingParticipantConfiguration : IEntityTypeConfiguration<MeetingParticipant>
{
    public void Configure(EntityTypeBuilder<MeetingParticipant> builder)
    {
        builder.ToTable(TableNames.MeetingParticipants);
        builder.HasKey(meetingParticipants => meetingParticipants.Id);

        builder.Property(meetingParticipants => meetingParticipants.OccurrenceId).HasColumnName("occurrence_id");

        builder.Property(meetingParticipants => meetingParticipants.UserId).HasColumnName("user_id");

        builder.Property(meetingParticipants => meetingParticipants.Role).HasColumnName("role");        

        builder.Property(meetingParticipants => meetingParticipants.CreatedBy).HasColumnType("uuid");

        builder.Property(meetingParticipants => meetingParticipants.ModifiedBy).HasColumnType("uuid");

        builder.Property(meetingParticipants => meetingParticipants.CreatedOnUtc).HasColumnType("timestamp").IsRequired();

        builder.Property(meetingParticipants => meetingParticipants.ModifiedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(meetingParticipants => meetingParticipants.DeletedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(meetingParticipants => meetingParticipants.IsDeleted).HasDefaultValue(false).IsRequired();

        builder.HasQueryFilter(meetingParticipants => !meetingParticipants.IsDeleted);
    }
}