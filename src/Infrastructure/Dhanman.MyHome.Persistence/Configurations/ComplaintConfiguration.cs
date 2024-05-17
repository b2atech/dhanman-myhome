using Dhanman.MyHome.Domain.Entities.Complaints;
using Dhanman.MyHome.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dhanman.MyHome.Persistence.Configurations;

internal sealed class ComplaintConfiguration : IEntityTypeConfiguration<Complaint>
{
    #region Methods
    public void Configure(EntityTypeBuilder<Complaint> builder)
    {
        builder.ToTable(TableNames.Complaints);
        builder.HasKey(complaint => complaint.Id);

        // Required properties
        builder.Property(complaint => complaint.Subject).HasColumnName("subject").IsRequired();
        builder.Property(complaint => complaint.PrefferedTime).HasColumnName("preffered_time").IsRequired();
        builder.Property(complaint => complaint.CategoryId).HasColumnName("category_id").IsRequired();
        builder.Property(complaint => complaint.SubCategoryId).HasColumnName("sub_category_id").IsRequired();
        builder.Property(complaint => complaint.PriorityId).HasColumnName("priority_id").IsRequired();
        builder.Property(complaint => complaint.DepartmentId).HasColumnName("department_id").IsRequired();
        builder.Property(complaint => complaint.OccuredDate).HasColumnName("occured_date").IsRequired();
        builder.Property(complaint => complaint.PrefferedDate).HasColumnName("preffered_date").IsRequired();
        builder.Property(complaint => complaint.IsUrgent).HasColumnName("is_urgent").IsRequired();

        // Not required properties
        builder.Property(complaint => complaint.Description).HasColumnName("description").IsRequired(false);
        builder.Property(complaint => complaint.DocLink).HasColumnName("doc_link").IsRequired(false);

        builder.Property(complaint => complaint.ModifiedBy).HasColumnType("uuid");
        builder.Property(complaint => complaint.CreatedOnUtc).HasColumnType("timestamp").IsRequired();
        builder.Property(complaint => complaint.ModifiedOnUtc).HasColumnType("timestamp").IsRequired(false);
        builder.Property(complaint => complaint.DeletedOnUtc).HasColumnType("timestamp").IsRequired(false);
        builder.Property(complaint => complaint.IsDeleted).HasDefaultValue(false).IsRequired();
        builder.HasQueryFilter(complaint => !complaint.IsDeleted);
    }
    #endregion
}