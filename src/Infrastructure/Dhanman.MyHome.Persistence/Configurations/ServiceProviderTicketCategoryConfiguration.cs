using Dhanman.MyHome.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Dhanman.MyHome.Domain.Entities.ServiceProviderTicketCategories;
using Dhanman.MyHome.Domain.Entities.ServiceProviders;
using Dhanman.MyHome.Domain.Entities.TicketCategories;

namespace Dhanman.MyHome.Persistence.Configurations
{
    internal class ServiceProviderTicketCategoryConfiguration : IEntityTypeConfiguration<ServiceProviderTicketCategory>
    {
        public void Configure(EntityTypeBuilder<ServiceProviderTicketCategory> builder)
        {
            builder.ToTable(TableNames.ServiceProviderTicketCategories);

            builder.HasKey(spc => spc.Id);

            builder.HasOne<ServiceProvider>()
                     .WithMany()
                     .HasForeignKey(sp => sp.ServiceProviderId)
                     .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne<TicketCategory>()
                .WithMany()
                .HasForeignKey(sp => sp.TicketCategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(spc => spc.DeletedOnUtc)
                .HasColumnType("timestamp")
                .IsRequired(false);

            builder.Property(spc => spc.IsDeleted)
                .HasDefaultValue(false)
                .IsRequired();

            builder.HasQueryFilter(spc => !spc.IsDeleted);
        }
    }
}
