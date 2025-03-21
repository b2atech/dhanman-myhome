using Dhanman.MyHome.Persistence.Constants;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Dhanman.MyHome.Domain.Entities.Tickets;
using Dhanman.MyHome.Domain.Entities.ServiceProviders;

namespace Dhanman.MyHome.Persistence.Configurations
{
    internal sealed class TicketServiceProviderOtpConfiguration : IEntityTypeConfiguration<TicketServiceProviderOtp>
    {
        public void Configure(EntityTypeBuilder<TicketServiceProviderOtp> builder)
        {
            builder.ToTable(TableNames.TicketServiceProviderOtps); 
            builder.HasKey(otp => otp.Id); 

            builder.Property(otp => otp.Otp)
                   .HasColumnName("otp")
                   .IsRequired(); 

            builder.Property(otp => otp.ExpirationTime)
                   .HasColumnName("expiration_time")
                   .IsRequired(); 

            builder.Property(otp => otp.TicketId)
                   .HasColumnName("ticket_id")
                   .IsRequired();  
            builder.HasOne<Ticket>()
                   .WithMany() 
                   .HasForeignKey(otp => otp.TicketId)
                   .OnDelete(DeleteBehavior.Cascade);  
        }
    }
}
