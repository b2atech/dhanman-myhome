using Dhanman.MyHome.Domain.Entities.WaterTankerDeliveries;
using Dhanman.MyHome.Persistence.Constants;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Dhanman.MyHome.Persistence.Configurations;

internal sealed class WaterTankerDeliveryConfiguration : IEntityTypeConfiguration<WaterTankerDelivery>
{
    public void Configure(EntityTypeBuilder<WaterTankerDelivery> builder)
    {
        builder.ToTable(TableNames.WaterTankerDeliveries);

        builder.HasKey(wtd => wtd.Id);

        builder.Property(wtd => wtd.CompanyId)
            .HasColumnName("company_id")
            .HasColumnType("uuid")
            .IsRequired();

        builder.Property(wtd => wtd.VendorId)
            .HasColumnName("vendor_id")
            .HasColumnType("uuid")
            .IsRequired();

        builder.Property(wtd => wtd.DeliveryDate)
            .HasColumnName("delivery_date")
            .HasColumnType("date")
            .IsRequired();

        builder.Property(wtd => wtd.DeliveryTime)
            .HasColumnName("delivery_time")
            .HasColumnType("time")
            .IsRequired();

        builder.Property(wtd => wtd.TankerCapacityLiters)
            .HasColumnName("tanker_capacity_liters")
            .HasColumnType("integer")
            .IsRequired();

        builder.Property(wtd => wtd.ActualReceivedLiters)
            .HasColumnName("actual_received_liters")
            .HasColumnType("integer")
            .IsRequired();

        builder.Property(wtd => wtd.CreatedBy)
            .HasColumnName("created_by")
            .HasColumnType("uuid")
            .IsRequired();

        builder.Property(wtd => wtd.CreatedOnUtc)
            .HasColumnName("created_on_utc")
            .HasColumnType("timestamp with time zone")
            .IsRequired();

        builder.Property(wtd => wtd.DeletedOnUtc)
            .HasColumnType("timestamp").
            IsRequired(false);

        builder.Property(wtd => wtd.ModifiedBy)
            .HasColumnName("modified_by")
            .HasColumnType("uuid");

        builder.Property(wtd => wtd.ModifiedOnUtc)
            .HasColumnName("modified_on_utc")
            .HasColumnType("timestamp with time zone");

        builder.Property(wtd => wtd.IsDeleted).HasDefaultValue(false).IsRequired();

        builder.HasQueryFilter(wtd => !wtd.IsDeleted);

    }
}