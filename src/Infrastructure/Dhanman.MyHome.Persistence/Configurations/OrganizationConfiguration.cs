using Dhanman.MyHome.Domain.Entities.Organizations;
using Dhanman.MyHome.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dhanman.MyHome.Persistence.Configurations;

internal sealed class OrganizationConfiguration : IEntityTypeConfiguration<Organization>
{
    public void Configure(EntityTypeBuilder<Organization> builder)
    {
        builder.ToTable(TableNames.Organizations);
        builder.HasKey(organization => organization.Id);

        builder.Property(organization => organization.Name).HasColumnName("name").IsRequired();

        builder.Property(organization => organization.CreatedBy).HasColumnType("uuid");

        builder.Property(organization => organization.ModifiedBy).HasColumnType("uuid");

        builder.Property(organization => organization.CreatedOnUtc).HasColumnType("timestamp").IsRequired();

        builder.Property(organization => organization.ModifiedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(organization => organization.DeletedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(organization => organization.IsDeleted).HasDefaultValue(false).IsRequired();

        builder.HasQueryFilter(organization => !organization.IsDeleted);
    }
}
