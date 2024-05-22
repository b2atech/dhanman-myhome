using Dhanman.MyHome.Domain.Entities.SubCategories;
using Dhanman.MyHome.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dhanman.MyHome.Persistence.Configurations;

internal class SubCategoryConfiguration : IEntityTypeConfiguration<SubCategory>
{
    public void Configure(EntityTypeBuilder<SubCategory> builder)
    {
        builder.ToTable(TableNames.SubCategories);

        builder.HasKey(category => category.Id);

        builder.Property(category => category.Name).HasColumnName("name").IsRequired();

        builder.Property(category => category.CategoryId).HasColumnName("category_id").IsRequired();

        builder.Property(category => category.DeletedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(category => category.IsDeleted).HasDefaultValue(false).IsRequired();

        builder.Property(category => category.CreatedBy).HasColumnType("uuid");

        builder.Property(category => category.ModifiedBy).HasColumnType("uuid");

        builder.Property(category => category.CreatedOnUtc).HasColumnType("timestamp").IsRequired();

        builder.Property(category => category.ModifiedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(category => category.DeletedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(category => category.IsDeleted).HasDefaultValue(false).IsRequired();

    }
}
