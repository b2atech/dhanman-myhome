using Dhanman.MyHome.Domain.Entities.Portfolios;
using Dhanman.MyHome.Persistence.Constants;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Dhanman.MyHome.Persistence.Configurations;

internal sealed class PortfolioConfiguration : IEntityTypeConfiguration<Portfolio>
{
    public void Configure(EntityTypeBuilder<Portfolio> builder)
    {
        builder.ToTable(TableNames.Portfolios);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.ApartmentId)
            .HasColumnName("apartment_id")
            .HasColumnType("uuid")
            .IsRequired();

        builder.Property(x => x.Name)
            .HasColumnName("name")
            .HasColumnType("text")
            .IsRequired();

        builder.Property(x => x.Description)
            .HasColumnName("description")
            .HasColumnType("text")
            .IsRequired(false);

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
