using Dhanman.MyHome.Domain.Entities.CommonEntities;
using Dhanman.MyHome.Domain.Entities.TicketCategories;
using Dhanman.MyHome.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dhanman.MyHome.Persistence.Configurations;
 
internal class TicketCategoryConfiguration : IEntityTypeConfiguration<TicketCategory>
{
    public void Configure(EntityTypeBuilder<TicketCategory> builder)
    {
        builder.ToTable(TableNames.TicketCategories);
        builder.HasKey(ticketCategories => ticketCategories.Id);

        builder.Property(ticketCategories => ticketCategories.Name).HasColumnName("name").HasMaxLength(Name.MaxLength).IsRequired();

        builder.Property(ticketCategories => ticketCategories.DeletedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(ticketCategories => ticketCategories.IsDeleted).HasDefaultValue(false).IsRequired();

        builder.HasQueryFilter(ticketCategories => !ticketCategories.IsDeleted);

    }
}