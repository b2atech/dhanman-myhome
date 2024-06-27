using Dhanman.MyHome.Domain.Entities.Deliveries;
using Dhanman.MyHome.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dhanman.MyHome.Persistence.Configurations
{
    internal sealed class DeliveryPersonConfiguration : IEntityTypeConfiguration<DeliveryPerson>
    {
        public void Configure(EntityTypeBuilder<DeliveryPerson> builder)
        {
            builder.ToTable(TableNames.DeliveryPersons);
            builder.HasKey(person => person.Id);

            builder.Property(person => person.Name).HasColumnName("name").IsRequired();

            builder.Property(person => person.CompanyName).HasColumnName("company_name").IsRequired();

            builder.Property(person => person.MobileNumber).HasColumnName("mobile_number").IsRequired();

            builder.Property(person => person.CreatedBy).HasColumnType("uuid");

            builder.Property(person => person.ModifiedBy).HasColumnType("uuid");

            builder.Property(person => person.CreatedOnUtc).HasColumnType("timestamp").IsRequired();

            builder.Property(person => person.ModifiedOnUtc).HasColumnType("timestamp").IsRequired(false);

            builder.Property(person => person.DeletedOnUtc).HasColumnType("timestamp").IsRequired(false);

            builder.Property(person => person.IsDeleted).HasDefaultValue(false).IsRequired();

            builder.HasQueryFilter(person => !person.IsDeleted);
        }
    }
}
