using Dhanman.MyHome.Persistence.Constants;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Dhanman.MyHome.Domain.Entities.Apartments;

namespace Dhanman.MyHome.Persistence.Configurations;

internal sealed class ServiceProviderLogConfiguration : IEntityTypeConfiguration<ServiceProviderLog>
{
    public void Configure(EntityTypeBuilder<ServiceProviderLog> builder)
    {
        builder.ToTable(TableNames.ServiceProviderLogs);
        builder.HasKey(serviceProviderLogs => serviceProviderLogs.Id);

        builder.Property(serviceProviderLogs => serviceProviderLogs.ServiceProviderId).HasColumnName("service_provider_id").IsRequired();

        builder.Property(serviceProviderLogs => serviceProviderLogs.VisitingUnitId).HasColumnName("visiting_unit_id").IsRequired();

        builder.Property(serviceProviderLogs => serviceProviderLogs.VisitPurposeId).HasColumnName("visit_purpose_id").IsRequired();
         
        builder.Property(serviceProviderLogs => serviceProviderLogs.VisitingFrom).HasColumnName("visiting_from").IsRequired();

        builder.Property(serviceProviderLogs => serviceProviderLogs.CurrentStatusId).HasColumnName("current_status_id").IsRequired();

        builder.Property(serviceProviderLogs => serviceProviderLogs.EntryTime).HasColumnName("entry_time").IsRequired();

        builder.Property(serviceProviderLogs => serviceProviderLogs.ExitTime).HasColumnName("exit_time").IsRequired();

        builder.Property(serviceProviderLogs => serviceProviderLogs.CreatedBy).HasColumnType("uuid");

        builder.Property(serviceProviderLogs => serviceProviderLogs.ModifiedBy).HasColumnType("uuid");

        builder.Property(serviceProviderLogs => serviceProviderLogs.CreatedOnUtc).HasColumnType("timestamp").IsRequired();

        builder.Property(serviceProviderLogs => serviceProviderLogs.ModifiedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(serviceProviderLogs => serviceProviderLogs.DeletedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(serviceProviderLogs => serviceProviderLogs.IsDeleted).HasDefaultValue(false).IsRequired();

        builder.HasQueryFilter(serviceProviderLogs => !serviceProviderLogs.IsDeleted);
    }
}