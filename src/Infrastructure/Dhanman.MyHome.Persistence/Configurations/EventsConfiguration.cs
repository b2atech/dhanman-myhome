using Dhanman.MyHome.Domain.Entities.Events;
using Dhanman.MyHome.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dhanman.MyHome.Persistence.Configurations;

internal sealed class EventsConfiguration : IEntityTypeConfiguration<Event>
{
    #region Methods

    public void Configure(EntityTypeBuilder<Event> builder)
    {
        builder.ToTable(TableNames.Events);

        builder.HasKey(eventEntity => eventEntity.Id);

        builder.Property(eventEntity => eventEntity.CompanyId)
            .HasColumnType("uuid") 
            .IsRequired();

        builder.Property(eventEntity => eventEntity.CommunityCalenderId)
            .HasColumnName("community_calender_id")
            .IsRequired();

        builder.Property(eventEntity => eventEntity.Title)
            .HasColumnName("title")
            .IsRequired();

        builder.Property(eventEntity => eventEntity.Description)
            .HasColumnName("description"); 

        builder.Property(eventEntity => eventEntity.StartTime)
            .HasColumnName("start_time")
            .IsRequired();

        builder.Property(eventEntity => eventEntity.EndTime)
            .HasColumnName("end_time")
            .IsRequired();

        builder.Property(eventEntity => eventEntity.IsRecurring)
            .HasColumnName("is_recurring")
            .IsRequired();

        builder.Property(eventEntity => eventEntity.RecurrenceRule)
           .HasColumnName("recurrence_rule").IsRequired(false);

        builder.Property(eventEntity => eventEntity.RecurrenceRuleId)
            .HasColumnName("recurrence_rule_id");

        builder.Property(eventEntity => eventEntity.RecurrenceEndDate)
            .HasColumnType("timestamp");

        builder.Property(eventEntity => eventEntity.CreatedBy)
            .HasColumnType("uuid")
            .IsRequired();

        builder.Property(eventEntity => eventEntity.ModifiedBy)
            .HasColumnType("uuid");

        builder.Property(eventEntity => eventEntity.CreatedOnUtc)
            .HasColumnType("timestamp")
            .IsRequired();

        builder.Property(eventEntity => eventEntity.ModifiedOnUtc)
            .HasColumnType("timestamp");

        builder.Property(eventEntity => eventEntity.DeletedOnUtc)
            .HasColumnType("timestamp"); 

        builder.Property(eventEntity => eventEntity.IsDeleted)
            .HasDefaultValue(false)
            .IsRequired();

        builder.HasQueryFilter(eventEntity => !eventEntity.IsDeleted);
    }

    #endregion

}
