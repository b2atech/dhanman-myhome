using Dhanman.MyHome.Domain.Entities.Pins;
using Dhanman.MyHome.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dhanman.MyHome.Persistence.Configurations;

internal sealed class PinConfiguration : IEntityTypeConfiguration<Pin>
{
    public void Configure(EntityTypeBuilder<Pin> builder)
    {
        builder.ToTable(TableNames.Pins);

        builder.HasKey(p => p.ServiceProviderId);

        builder.Property(p => p.ServiceProviderId).IsRequired();
    }
}