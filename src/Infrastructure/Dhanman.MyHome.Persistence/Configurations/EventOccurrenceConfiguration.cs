using Dhanman.MyHome.Domain.Entities.EventOccurrences;
using Dhanman.MyHome.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dhanman.MyHome.Persistence.Configurations;
 
internal sealed class EventOccurrenceConfiguration : IEntityTypeConfiguration<EventOccurrence>
{
    public void Configure(EntityTypeBuilder<EventOccurrence> builder)
    {
        builder.ToTable(TableNames.EventOccurrences);
        builder.HasKey(eventOccurrences => eventOccurrences.Id);

        builder.Property(eventOccurrences => eventOccurrences.EventId).HasColumnName("event_id");

        builder.Property(eventOccurrences => eventOccurrences.OccurrenceDate).HasColumnName("occurrence_date");

        builder.Property(eventOccurrences => eventOccurrences.StartTime).HasColumnName("start_time").IsRequired(); ;

        builder.Property(eventOccurrences => eventOccurrences.EndTime).HasColumnName("end_time").IsRequired(); ;

        builder.Property(eventOccurrences => eventOccurrences.GeneratedFromRecurrence).HasColumnName("generated_from_recurrence");

        builder.Property(eventOccurrences => eventOccurrences.EventOccurrenceStatusId).HasColumnName("EventOccurrenceStatusId").IsRequired(true);

        builder.Property(eventOccurrences => eventOccurrences.RecordingUrl).HasColumnName("recording_url").IsRequired(false);

        builder.Property(eventOccurrences => eventOccurrences.Notes).HasColumnName("notes").IsRequired(false);

        builder.Property(eventOccurrences => eventOccurrences.CreatedBy).HasColumnType("uuid");

        builder.Property(eventOccurrences => eventOccurrences.ModifiedBy).HasColumnType("uuid");

        builder.Property(eventOccurrences => eventOccurrences.CreatedOnUtc).HasColumnType("timestamp").IsRequired();

        builder.Property(eventOccurrences => eventOccurrences.ModifiedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(eventOccurrences => eventOccurrences.DeletedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(eventOccurrences => eventOccurrences.IsDeleted).HasDefaultValue(false).IsRequired();

        builder.HasQueryFilter(eventOccurrences => !eventOccurrences.IsDeleted);
    }
}