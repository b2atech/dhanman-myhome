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
        builder.HasKey(events => events.Id);

        builder.Property(events => events.Title).HasColumnName("title").IsRequired();

        builder.Property(events => events.Description).HasColumnName("description").IsRequired();

        builder.Property(events => events.AllDay).HasColumnName("all_day").IsRequired();

        builder.Property(events => events.Color).HasColumnName("color").IsRequired();

        builder.Property(events => events.TextColor).HasColumnName("text_color").IsRequired();

        builder.Property(events => events.ReserverdByUnitId).HasColumnName("reserved_by_unit_id").IsRequired();

        builder.Property(events => events.ReservationDate).HasColumnName("reservation_date").IsRequired();

        builder.Property(events => events.Start).HasColumnName("start").IsRequired();

        builder.Property(events => events.End).HasColumnName("end").IsRequired();

        builder.Property(events => events.Pourpose).HasColumnName("purpose").IsRequired();

        builder.Property(events => events.StatusId).HasColumnName("status_id").IsRequired();

        builder.Property(events => events.BookingFacilitiesId).HasColumnName("booking_facilities_id").IsRequired();

        builder.Property(events => events.CreatedBy).HasColumnType("uuid");

        builder.Property(events => events.ModifiedBy).HasColumnType("uuid");

        builder.Property(events => events.CreatedOnUtc).HasColumnType("timestamp").IsRequired();

        builder.Property(events => events.ModifiedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(events => events.DeletedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(events => events.IsDeleted).HasDefaultValue(false).IsRequired();

        builder.HasQueryFilter(events => !events.IsDeleted);
    }

    #endregion

}
