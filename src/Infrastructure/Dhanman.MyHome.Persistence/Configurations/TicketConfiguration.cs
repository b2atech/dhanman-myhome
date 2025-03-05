using Dhanman.MyHome.Domain.Entities.Tickets;
using Dhanman.MyHome.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dhanman.MyHome.Persistence.Configurations;

internal sealed class TicketConfiguration : IEntityTypeConfiguration<Ticket>
{
    public void Configure(EntityTypeBuilder<Ticket> builder)
    {

        builder.ToTable(TableNames.Tickets);

        builder.HasKey(ticket => ticket.Id);

        builder.Property(ticket => ticket.ApartmentId).HasColumnName("apartment_id");

        builder.Property(ticket => ticket.UnitId).HasColumnType("integer").IsRequired();

        builder.Property(ticket => ticket.Title).HasMaxLength(255).IsRequired();

        builder.Property(ticket => ticket.Description).HasColumnType("text").IsRequired();

        builder.Property(ticket => ticket.TicketCategoryId).HasColumnType("integer").IsRequired();

        builder.Property(ticket => ticket.TicketPriorityId).HasColumnType("integer").IsRequired();

        builder.Property(ticket => ticket.TicketStatusId).HasColumnType("integer").IsRequired();

        builder.Property(ticket => ticket.TicketAssignedTo).HasColumnType("integer").IsRequired(false);

        builder.Property(ticket => ticket.CreatedBy).HasColumnType("uuid").IsRequired();

        builder.Property(ticket => ticket.ModifiedBy).HasColumnType("uuid").IsRequired(false);

        builder.Property(ticket => ticket.CreatedOnUtc).HasColumnType("timestamp").IsRequired();

        builder.Property(ticket => ticket.ModifiedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(ticket => ticket.DeletedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(ticket => ticket.IsDeleted).HasDefaultValue(false).IsRequired();

        builder.HasQueryFilter(ticket => !ticket.IsDeleted);
    }
}
