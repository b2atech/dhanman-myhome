using Dhanman.MyHome.Domain.Entities.CommonEntities;
using Dhanman.MyHome.Domain.Entities.VisitTypes;
using Dhanman.MyHome.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dhanman.MyHome.Persistence.Configurations;

internal class VisitTypeConfiguration : IEntityTypeConfiguration<VisitType>
{
    public void Configure(EntityTypeBuilder<VisitType> builder)
    {
        builder.ToTable(TableNames.VisitTypes);
        builder.HasKey(visitType => visitType.Id);

        builder.Property(visitType => visitType.Name).HasColumnName("name").HasMaxLength(Name.MaxLength).IsRequired();
    }
}
