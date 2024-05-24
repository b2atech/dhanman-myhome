using Dhanman.MyHome.Domain.Entities.Categories;
using Dhanman.MyHome.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dhanman.MyHome.Persistence.Configurations;

internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable(TableNames.Categories);

        builder.HasKey(category => category.Id);

        builder.Property(category => category.Name).HasColumnName("name").IsRequired();

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
