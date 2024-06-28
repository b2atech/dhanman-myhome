using Dhanman.MyHome.Domain.Entities.Deliveries;
using Dhanman.MyHome.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dhanman.MyHome.Persistence.Configurations;

public class PinConfiguration : IEntityTypeConfiguration<Pin>
{
    public void Configure(EntityTypeBuilder<Pin> builder)
    {
        builder.ToTable(TableNames.Pins);
        builder.HasKey(e => e.Id);

        builder.Property(e => e.PinCode)
                .IsRequired()
                .HasMaxLength(6);

        builder.Property(e => e.ServiceProviderId)
               .IsRequired(false);

        builder.Property(e => e.VisitorId)
            .IsRequired(false);

        builder.Property(e => e.DeliveryId)
            .IsRequired(false);

        builder.Property(e => e.EffectiveStartDateTime)
            .IsRequired();

        builder.Property(e => e.EffectiveEndDateTime)
            .IsRequired(false);
    }
}
