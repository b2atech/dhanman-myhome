using Dhanman.MyHome.Domain.Entities.CommitteeMembers;
using Dhanman.MyHome.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dhanman.Common.Persistence.Configurations;

internal sealed class CommitteeMemberConfiguration : IEntityTypeConfiguration<CommitteeMember>
{
    public void Configure(EntityTypeBuilder<CommitteeMember> builder)
    {
        builder.ToTable(TableNames.CommitteeMembers);

        builder.HasKey(e => e.Id);

        builder.Property(e => e.UserId)
               .HasColumnName("user_id")
               .HasColumnType("uuid")
               .IsRequired();

        builder.Property(e => e.ApartmentId)
               .HasColumnName("apartment_id")
               .HasColumnType("uuid")
               .IsRequired();
        builder.Property(e => e.EffectiveStartDate)
               .HasColumnName("effective_start_date")
               .HasColumnType("timestamp")
               .IsRequired();

        builder.Property(e => e.EffectiveEndDate)
               .HasColumnName("effective_end_date")
               .HasColumnType("timestamp")
               .IsRequired();

        builder.Property(e => e.RoleId)
               .HasColumnName("role_id")
               .IsRequired();

        builder.Property(e => e.PortfolioId)
               .HasColumnName("portfolio_id")
               .IsRequired();

        builder.Property(e => e.CreatedOnUtc)
               .HasColumnName("created_on_utc")
               .HasColumnType("timestamp")
               .IsRequired();

        builder.Property(e => e.ModifiedOnUtc)
               .HasColumnName("modified_on_utc")
               .HasColumnType("timestamp");

        builder.Property(e => e.DeletedOnUtc)
               .HasColumnName("deleted_on_utc")
               .HasColumnType("timestamp");

        builder.Property(e => e.IsDeleted)
               .HasColumnName("is_deleted")
               .HasDefaultValue(false)
               .IsRequired();

        builder.Property(e => e.CreatedBy)
               .HasColumnName("created_by")
               .HasColumnType("uuid")
               .IsRequired();

        builder.Property(e => e.ModifiedBy)
               .HasColumnName("modified_by")
               .HasColumnType("uuid");

        builder.HasQueryFilter(e => !e.IsDeleted);
    }
}
