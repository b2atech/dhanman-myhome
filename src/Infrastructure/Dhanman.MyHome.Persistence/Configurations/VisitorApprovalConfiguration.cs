﻿using Dhanman.MyHome.Persistence.Constants;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Dhanman.MyHome.Domain.Entities.VisitorApprovals;

namespace Dhanman.MyHome.Persistence.Configurations;

internal sealed class VisitorApprovalConfiguration : IEntityTypeConfiguration<VisitorApproval>
{
    public void Configure(EntityTypeBuilder<VisitorApproval> builder)
    {
        builder.ToTable(TableNames.VisitorApprovals);
        builder.HasKey(approvedVisitors => approvedVisitors.Id);

        builder.Property(approvedVisitors => approvedVisitors.VisitorId).HasColumnName("visitor_id").IsRequired();

        builder.Property(approvedVisitors => approvedVisitors.VisitTypeId).HasColumnName("visit_type_id").IsRequired();

        builder.Property(approvedVisitors => approvedVisitors.StartDate).HasColumnName("start_date").HasColumnType("date").IsRequired();

        builder.Property(approvedVisitors => approvedVisitors.EndDate).HasColumnName("end_date").HasColumnType("date").IsRequired();

        builder.Property(approvedVisitors => approvedVisitors.EntryTime).HasColumnName("entry_time").HasColumnType("time").IsRequired(false);

        builder.Property(approvedVisitors => approvedVisitors.ExitTime).HasColumnName("exit_time").HasColumnType("time").IsRequired(false);

        builder.Property(approvedVisitors => approvedVisitors.VehicleNumber).HasColumnName("vehicle_number").IsRequired(false);

        builder.Property(approvedVisitors => approvedVisitors.CompanyName).HasColumnName("company_name").IsRequired(false);

        builder.Property(approvedVisitors => approvedVisitors.CreatedBy).HasColumnType("uuid");

        builder.Property(approvedVisitors => approvedVisitors.ModifiedBy).HasColumnType("uuid").IsRequired(false);

        builder.Property(approvedVisitors => approvedVisitors.CreatedOnUtc).HasColumnType("timestamp").IsRequired();

        builder.Property(approvedVisitors => approvedVisitors.ModifiedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(approvedVisitors => approvedVisitors.DeletedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(approvedVisitors => approvedVisitors.IsDeleted).HasDefaultValue(false).IsRequired();

        builder.HasQueryFilter(approvedVisitors => approvedVisitors.IsDeleted);
    }
}
