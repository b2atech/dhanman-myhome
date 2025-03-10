using Dhanman.MyHome.Domain.Entities.CommonEntities;
using Dhanman.MyHome.Domain.Entities.TicketStatuses;
using Dhanman.MyHome.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dhanman.MyHome.Persistence.Configurations;
internal class TicketStatusConfiguration : IEntityTypeConfiguration<TicketStatus>
{
    public void Configure(EntityTypeBuilder<TicketStatus> builder)
    {
        builder.ToTable(TableNames.TicketStatuses);
        builder.HasKey(ticketStatuses => ticketStatuses.Id);

        builder.Property(ticketStatuses => ticketStatuses.Name).HasColumnName("name").HasMaxLength(Name.MaxLength).IsRequired();

        builder.Property(ticketStatuses => ticketStatuses.DeletedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(ticketStatuses => ticketStatuses.IsDeleted).HasDefaultValue(false).IsRequired();

        builder.HasQueryFilter(ticketStatuses => !ticketStatuses.IsDeleted);

    }
}