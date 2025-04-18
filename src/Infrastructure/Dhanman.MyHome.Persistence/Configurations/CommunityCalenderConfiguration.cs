using Dhanman.MyHome.Domain.Entities.CommunityCalenders;
using Dhanman.MyHome.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dhanman.MyHome.Persistence.Configurations;

internal sealed class CommunityCalenderConfiguration : IEntityTypeConfiguration<CommunityCalender>
{
    public void Configure(EntityTypeBuilder<CommunityCalender> builder)
    {
        builder.ToTable(TableNames.CommunityCalenders);
        builder.HasKey(communityCalenders => communityCalenders.Id);

        builder.Property(communityCalenders => communityCalenders.Name).HasColumnName("name").IsRequired();

        builder.Property(communityCalenders => communityCalenders.Color).HasColumnName("color").IsRequired();

        builder.Property(communityCalenders => communityCalenders.CreatedBy).HasColumnType("uuid").IsRequired();

        builder.Property(communityCalenders => communityCalenders.ModifiedBy).HasColumnType("uuid").IsRequired(false);

        builder.Property(communityCalenders => communityCalenders.CreatedOnUtc).HasColumnType("timestamp").IsRequired();

        builder.Property(communityCalenders => communityCalenders.ModifiedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(communityCalenders => communityCalenders.DeletedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(communityCalenders => communityCalenders.IsDeleted).HasDefaultValue(false).IsRequired();

        builder.HasQueryFilter(communityCalenders => !communityCalenders.IsDeleted);
    }
}
