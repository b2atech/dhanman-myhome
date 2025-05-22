using Dhanman.MyHome.Domain.Entities.MeetingNotes;
using Dhanman.MyHome.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dhanman.MyHome.Persistence.Configurations;

internal class MeetingNoteConfiguration : IEntityTypeConfiguration<MeetingNote>
{
    public void Configure(EntityTypeBuilder<MeetingNote> builder)
    {
        builder.ToTable(TableNames.MeetingNotes);
        builder.HasKey(meetingNotes => meetingNotes.Id);

        builder.Property(meetingNotes => meetingNotes.OccurrenceId).HasColumnName("occurrence_id");

        builder.Property(meetingNotes => meetingNotes.NoteText).HasColumnName("note_text").IsRequired(false);

        builder.Property(meetingNotes => meetingNotes.CreatedBy).HasColumnType("uuid");

        builder.Property(meetingNotes => meetingNotes.ModifiedBy).HasColumnType("uuid");

        builder.Property(meetingNotes => meetingNotes.CreatedOnUtc).HasColumnType("timestamp").IsRequired();

        builder.Property(meetingNotes => meetingNotes.ModifiedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(meetingNotes => meetingNotes.DeletedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(meetingNotes => meetingNotes.IsDeleted).HasDefaultValue(false).IsRequired();

        builder.HasQueryFilter(meetingNotes => !meetingNotes.IsDeleted);
    }
}
