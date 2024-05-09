using Dhanman.MyHome.Domain.Entities.CommonEntities;
using Dhanman.MyHome.Domain.Entities.EventStatuses;
using Dhanman.MyHome.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dhanman.MyHome.Persistence.Configurations;

public class EventStatusConfiguration : IEntityTypeConfiguration<EventStatus>
{
    #region Methods

    public void Configure(EntityTypeBuilder<EventStatus> builder)
    {
        builder.ToTable(TableNames.EventStatus);
        builder.HasKey(eventStatus => eventStatus.Id);

        builder.Property(eventStatus => eventStatus.Name)
            .HasColumnName("name")
             .HasMaxLength(Name.MaxLength)
             .IsRequired();

        builder.Property(eventStatus => eventStatus.CreatedOnUtc).HasColumnType("timestamp").IsRequired();

        builder.Property(eventStatus => eventStatus.ModifiedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(eventStatus => eventStatus.DeletedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(eventStatus => eventStatus.IsDeleted).HasDefaultValue(false).IsRequired();

        builder.HasQueryFilter(eventStatus => !eventStatus.IsDeleted);
    }
    #endregion
}
