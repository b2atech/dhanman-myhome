using Dhanman.MyHome.Domain.Entities.CommonEntities;
using Dhanman.MyHome.Domain.Entities.TicketPriorities;
using Dhanman.MyHome.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dhanman.MyHome.Persistence.Configurations;
 
internal class TicketPriorityConfiguration : IEntityTypeConfiguration<TicketPriority>
{
    public void Configure(EntityTypeBuilder<TicketPriority> builder)
    {
        builder.ToTable(TableNames.TicketPriorities);
        builder.HasKey(ticketPriorities => ticketPriorities.Id);

        builder.Property(ticketPriorities => ticketPriorities.Name).HasColumnName("name").HasMaxLength(Name.MaxLength).IsRequired();

        builder.Property(ticketPriorities => ticketPriorities.DeletedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(ticketPriorities => ticketPriorities.IsDeleted).HasDefaultValue(false).IsRequired();

        builder.HasQueryFilter(ticketPriorities => !ticketPriorities.IsDeleted);

    }
}