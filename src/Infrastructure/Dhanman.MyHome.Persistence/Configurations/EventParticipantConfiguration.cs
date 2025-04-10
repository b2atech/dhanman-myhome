using Dhanman.MyHome.Domain.Entities.EventParticipants;
using Dhanman.MyHome.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dhanman.MyHome.Persistence.Configurations;

internal sealed class EventParticipantConfiguration : IEntityTypeConfiguration<EventParticipant>
{
    public void Configure(EntityTypeBuilder<EventParticipant> builder)
    {
        builder.ToTable(TableNames.EventParticipants); 

        builder.HasKey(eventParticipant => eventParticipant.Id);

        builder.Property(eventParticipant => eventParticipant.EventId)
            .HasColumnType("uuid")
            .IsRequired();

        builder.Property(eventParticipant => eventParticipant.UserId)
            .HasColumnType("uuid")
            .IsRequired();

        builder.Property(eventParticipant => eventParticipant.EventStatusId)
            .HasColumnName("event_status_id")
            .IsRequired();

        builder.Property(eventParticipant => eventParticipant.NotificationSent)
            .HasColumnName("notification_sent")
            .IsRequired();

        builder.Property(eventParticipant => eventParticipant.CreatedBy)
            .HasColumnType("uuid")
            .IsRequired();

        builder.Property(eventParticipant => eventParticipant.ModifiedBy)
            .HasColumnType("uuid");

        builder.Property(eventParticipant => eventParticipant.CreatedOnUtc)
            .HasColumnType("timestamp")
            .IsRequired();

        builder.Property(eventParticipant => eventParticipant.ModifiedOnUtc)
            .HasColumnType("timestamp");

        builder.Property(eventParticipant => eventParticipant.DeletedOnUtc)
            .HasColumnType("timestamp");

        builder.Property(eventParticipant => eventParticipant.IsDeleted)
            .HasDefaultValue(false)
            .IsRequired();

        builder.HasQueryFilter(eventParticipant => !eventParticipant.IsDeleted);
    }
}
