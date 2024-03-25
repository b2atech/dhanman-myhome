using Dhanman.MyHome.Domain.Entities.Apartments;
using Dhanman.MyHome.Persistence.Constants;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Dhanman.MyHome.Persistence.Configurations;

internal class ResidentRequestStatusConfiguration : IEntityTypeConfiguration<ResidentRequestStatus>
{
    public void Configure(EntityTypeBuilder<ResidentRequestStatus> builder)
    {
        builder.ToTable(TableNames.ResidentRequestStatuses);
        builder.HasKey(residentRequestStatus => residentRequestStatus.Id);

        builder.Property(residentRequestStatus => residentRequestStatus.Name).HasColumnName("name").HasMaxLength(Name.MaxLength).IsRequired();

        builder.Property(residentRequestStatus => residentRequestStatus.DeletedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(residentRequestStatus => residentRequestStatus.IsDeleted).HasDefaultValue(false).IsRequired();

        builder.HasQueryFilter(residentRequestStatus => !residentRequestStatus.IsDeleted);

    }
}