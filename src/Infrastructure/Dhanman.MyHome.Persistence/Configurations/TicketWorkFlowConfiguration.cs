using Dhanman.MyHome.Persistence.Constants;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Dhanman.MyHome.Domain.Entities.TicketWorkflows;

namespace Dhanman.MyHome.Persistence.Configurations;

internal class TicketWorkFlowConfiguration : IEntityTypeConfiguration<TicketWorkFlow>
{
    public void Configure(EntityTypeBuilder<TicketWorkFlow> builder)
    {
        builder.ToTable(TableNames.TicketWorkFlow);
        builder.HasKey(ticketWorkflow => ticketWorkflow.Id);
        builder.Property(m => m.Id).UseIdentityColumn();

        builder.Property(ticketWorkflow => ticketWorkflow.ApartmentId)
        .HasColumnName("apartment_id")
        .IsRequired();

        builder.Property(ticketWorkflow => ticketWorkflow.Status)
       .HasColumnName("status")
       .IsRequired();

        builder.Property(ticketWorkflow => ticketWorkflow.NextStatus)
       .HasColumnName("next_status")
       .IsRequired();

        builder.Property(ticketWorkflow => ticketWorkflow.PreviousStatus)
       .HasColumnName("previous_status")
       .IsRequired();

        builder.Property(ticketWorkflow => ticketWorkflow.IsInitial)
       .HasColumnName("is_initial")
       .IsRequired();

        builder.Property(ticketWorkflow => ticketWorkflow.CreatedOnUtc).HasColumnType("timestamp").IsRequired();

        builder.Property(ticketWorkflow => ticketWorkflow.ModifiedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(ticketWorkflow => ticketWorkflow.DeletedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(ticketWorkflow => ticketWorkflow.IsDeleted).HasDefaultValue(false).IsRequired();

        builder.HasQueryFilter(ticketWorkflow => !ticketWorkflow.IsDeleted);
    }
}
