using Dhanman.MyHome.Domain.Entities.GateTypes;
using Dhanman.MyHome.Persistence.Constants;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Dhanman.MyHome.Domain.Entities.MultiUnitVisits;

namespace Dhanman.MyHome.Persistence.Configurations;

internal class MultiUnitVisitConfiguration : IEntityTypeConfiguration<MultiUnitVisit>
{
    public void Configure(EntityTypeBuilder<MultiUnitVisit> builder)
    {
        builder.ToTable(TableNames.MultiUnitVisits);
        builder.HasKey(multiUnitVisit => multiUnitVisit.Id);

        builder.Property(multiUnitVisit => multiUnitVisit.VisitorLogId).HasColumnName("visitor_log_id");

        builder.Property(multiUnitVisit => multiUnitVisit.UnitId).HasColumnName("unit_id").IsRequired();

        builder.Property(multiUnitVisit => multiUnitVisit.Visitor_Statuses).HasColumnName("visitor_statuses").IsRequired();

        builder.Property(multiUnitVisit => multiUnitVisit.CreatedBy).HasColumnType("uuid");

        builder.Property(multiUnitVisit => multiUnitVisit.ModifiedBy).HasColumnType("uuid");

        builder.Property(multiUnitVisit => multiUnitVisit.CreatedOnUtc).HasColumnType("timestamp").IsRequired();

        builder.Property(multiUnitVisit => multiUnitVisit.ModifiedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(multiUnitVisit => multiUnitVisit.DeletedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(multiUnitVisit => multiUnitVisit.IsDeleted).HasDefaultValue(false).IsRequired();

        builder.HasQueryFilter(multiUnitVisit => !multiUnitVisit.IsDeleted);

    }
}
