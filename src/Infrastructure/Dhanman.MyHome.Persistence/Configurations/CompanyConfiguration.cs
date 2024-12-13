using Dhanman.MyHome.Domain.Entities.Companies;
using Dhanman.MyHome.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dhanman.MyHome.Persistence.Configurations;

internal sealed class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.ToTable(TableNames.Companies);
        builder.HasKey(company => company.Id);

        builder.Property(company => company.Name).HasColumnName("name").IsRequired();

        builder.Property(company => company.OrganizationId).HasColumnName("organization_id").HasColumnType("uuid");        

        builder.Property(company => company.CreatedBy).HasColumnType("uuid");

        builder.Property(company => company.ModifiedBy).HasColumnType("uuid");

        builder.Property(company => company.CreatedOnUtc).HasColumnType("timestamp").IsRequired();

        builder.Property(company => company.ModifiedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(company => company.DeletedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(company => company.IsDeleted).HasDefaultValue(false).IsRequired();

        builder.HasQueryFilter(company => !company.IsDeleted);
    }
}