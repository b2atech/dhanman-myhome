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
        builder.HasKey(floors => floors.Id);

        builder.Property(floors => floors.Name).HasColumnName("name").IsRequired();

        builder.Property(floors => floors.Description).HasColumnName("description").IsRequired();

        builder.Property(floors => floors.IsFullDay).HasColumnName("is_full_day").IsRequired();

        builder.Property(floors => floors.BackgroundColor).HasColumnName("background_color").IsRequired();

        builder.Property(floors => floors.TextColor).HasColumnName("text_color").IsRequired();

        builder.Property(floors => floors.UnitId).HasColumnName("unit_id").IsRequired();

        builder.Property(floors => floors.ReservationDate).HasColumnName("reservation_date").IsRequired();

        builder.Property(floors => floors.StartDate).HasColumnName("start_date").IsRequired();

        builder.Property(floors => floors.EndDate).HasColumnName("end_date").IsRequired();

        builder.Property(floors => floors.Pourpose).HasColumnName("purpose").IsRequired();

        builder.Property(floors => floors.StatusId).HasColumnName("status_id").IsRequired();

        builder.Property(floors => floors.CreatedBy).HasColumnType("uuid");

        builder.Property(floors => floors.ModifiedBy).HasColumnType("uuid");

        builder.Property(floors => floors.CreatedOnUtc).HasColumnType("timestamp").IsRequired();

        builder.Property(floors => floors.ModifiedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(floors => floors.DeletedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(floors => floors.IsDeleted).HasDefaultValue(false).IsRequired();

        builder.HasQueryFilter(floors => !floors.IsDeleted);
    }

    #endregion

}
