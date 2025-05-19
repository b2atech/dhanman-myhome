using Dhanman.MyHome.Persistence.Constants;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Dhanman.MyHome.Domain.Entities.EventOccurrenceStatuses;
using Dhanman.MyHome.Domain.Entities.CommonEntities;

namespace Dhanman.MyHome.Persistence.Configurations;

public class EventOccurrenceStatusConfiguration : IEntityTypeConfiguration<EventOccurrenceStatus>
{
    #region Methods

    public void Configure(EntityTypeBuilder<EventOccurrenceStatus> builder)
    {
        builder.ToTable(TableNames.EventOccurrenceStatus);
        builder.HasKey(eventOccurrenceStatus => eventOccurrenceStatus.Id);

        builder.Property(eventOccurrenceStatus => eventOccurrenceStatus.Name)
            .HasColumnName("name")
             .HasMaxLength(Name.MaxLength)
             .IsRequired();
    }
    #endregion
}