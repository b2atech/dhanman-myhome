using Dhanman.MyHome.Domain.Entities.ResidentTokens;
using Dhanman.MyHome.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dhanman.MyHome.Persistence.Configurations;

public class ResidentTokenConfiguration : IEntityTypeConfiguration<ResidentToken>
{
    #region Methods
    public void Configure(EntityTypeBuilder<ResidentToken> builder)
    {
        builder.ToTable(TableNames.ResidentTokens);
        builder.HasKey(reidentToken => reidentToken.Id);

        builder.Property(reidentToken => reidentToken.ResidentId).HasColumnName("resident_id").IsRequired();

        builder.Property(reidentToken => reidentToken.FCMToken).HasColumnName("fcm_token");
    }
    #endregion
}
