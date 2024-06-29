using Dhanman.MyHome.Domain.Entities.DeliveryCompanies;
using Dhanman.MyHome.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dhanman.MyHome.Persistence.Configurations;

internal class DeliveryCompanyConfiguration : IEntityTypeConfiguration<DeliveryCompany>
{
    #region Methodes
    public void Configure(EntityTypeBuilder<DeliveryCompany> builder)
    {
        builder.ToTable(TableNames.DeliveryCompanies);
        builder.HasKey(deliveryCompanies => deliveryCompanies.Id);

        builder.Property(deliveryCompanies => deliveryCompanies.Name).HasColumnName("name").HasColumnType("text").IsRequired();

        builder.Property(deliveryCompanies => deliveryCompanies.DeliveryCompanyCategoryId).HasColumnName("delivery_company_category_id").IsRequired();

        builder.Property(deliveryCompanies => deliveryCompanies.Icon).HasColumnName("icon").IsRequired();

        builder.Property(deliveryCompanies => deliveryCompanies.CreatedBy).HasColumnType("uuid");

        builder.Property(deliveryCompanies => deliveryCompanies.ModifiedBy).HasColumnType("uuid");

        builder.Property(deliveryCompanies => deliveryCompanies.CreatedOnUtc).HasColumnType("timestamp").IsRequired();

        builder.Property(deliveryCompanies => deliveryCompanies.ModifiedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(deliveryCompanies => deliveryCompanies.DeletedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(deliveryCompanies => deliveryCompanies.IsDeleted).HasDefaultValue(false).IsRequired();

        builder.HasQueryFilter(deliveryCompanies => !deliveryCompanies.IsDeleted);
    }
    #endregion
}