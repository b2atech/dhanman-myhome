using Dhanman.MyHome.Domain.Entities.MeetingAgendaItems;
using Dhanman.MyHome.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dhanman.MyHome.Persistence.Configurations;

internal sealed class MeetingAgendaItemConfiguration : IEntityTypeConfiguration<MeetingAgendaItem>
{
    public void Configure(EntityTypeBuilder<MeetingAgendaItem> builder)
    {
        builder.ToTable(TableNames.MeetingAgendaItems);
        builder.HasKey(meetingAgendaItems => meetingAgendaItems.Id);

        builder.Property(meetingAgendaItems => meetingAgendaItems.OccurrenceId).HasColumnName("occurrence_id");

        builder.Property(meetingAgendaItems => meetingAgendaItems.ItemText).HasColumnName("item_text");

        builder.Property(meetingAgendaItems => meetingAgendaItems.OrderNo).HasColumnName("order_no");

        builder.Property(meetingAgendaItems => meetingAgendaItems.CreatedBy).HasColumnType("uuid");

        builder.Property(meetingAgendaItems => meetingAgendaItems.ModifiedBy).HasColumnType("uuid");

        builder.Property(meetingAgendaItems => meetingAgendaItems.CreatedOnUtc).HasColumnType("timestamp").IsRequired();

        builder.Property(meetingAgendaItems => meetingAgendaItems.ModifiedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(meetingAgendaItems => meetingAgendaItems.DeletedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(meetingAgendaItems => meetingAgendaItems.IsDeleted).HasDefaultValue(false).IsRequired();

        builder.HasQueryFilter(meetingAgendaItems => !meetingAgendaItems.IsDeleted);
    }
}