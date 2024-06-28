using Dhanman.MyHome.Domain.Entities.DeliveryCompanyCategories;
using Dhanman.MyHome.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dhanman.MyHome.Persistence.Configurations;

internal class DeliveryCompanyCategoryConfiguration : IEntityTypeConfiguration<DeliveryCompanyCategory>
{
    #region Methodes
    public void Configure(EntityTypeBuilder<DeliveryCompanyCategory> builder)
    {
        builder.ToTable(TableNames.DeliveryCompanyCategories);
        builder.HasKey(deliveryCompanyCategory => deliveryCompanyCategory.Id);

        builder.Property(deliveryCompanyCategory => deliveryCompanyCategory.Name).HasColumnName("name").HasColumnType("text").IsRequired();

        builder.Property(deliveryCompanyCategory => deliveryCompanyCategory.DeletedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(deliveryCompanyCategory => deliveryCompanyCategory.IsDeleted).HasDefaultValue(false).IsRequired();

        builder.HasQueryFilter(deliveryCompanyCategory => !deliveryCompanyCategory.IsDeleted);
    }
    #endregion
}