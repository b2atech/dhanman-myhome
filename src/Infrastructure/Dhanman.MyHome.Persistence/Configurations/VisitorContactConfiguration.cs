using Dhanman.MyHome.Persistence.Constants;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Dhanman.MyHome.Domain.Entities.VisitorContacts;

namespace Dhanman.MyHome.Persistence.Configurations;

internal sealed class VisitorContactConfiguration : IEntityTypeConfiguration<VisitorContact>
{
    public void Configure(EntityTypeBuilder<VisitorContact> builder)
    {
        builder.ToTable(TableNames.VisitorContacts);
        builder.HasKey(visitorContact => visitorContact.Id);

        builder.Property(visitorContact => visitorContact.VisitorId).HasColumnName("visitor_id").IsRequired();

        builder.Property(visitorContact => visitorContact.ContactNumber).HasColumnName("contact_number").IsRequired();

        builder.Property(visitorContact => visitorContact.IsCurrent).HasColumnName("is_current").IsRequired();

        builder.Property(visitorContact => visitorContact.CreatedBy).HasColumnType("uuid");

        builder.Property(visitorContact => visitorContact.ModifiedBy).HasColumnType("uuid");

        builder.Property(visitorContact => visitorContact.CreatedOnUtc).HasColumnType("timestamp").IsRequired();

        builder.Property(visitorContact => visitorContact.ModifiedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(visitorContact => visitorContact.DeletedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(visitorContact => visitorContact.IsDeleted).HasDefaultValue(false).IsRequired();

        builder.HasQueryFilter(visitorContact => !visitorContact.IsDeleted);
    }
}
