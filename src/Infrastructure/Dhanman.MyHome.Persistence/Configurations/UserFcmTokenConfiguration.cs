using Dhanman.MyHome.Domain.Entities.UserFcmTokens;
using Dhanman.MyHome.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dhanman.MyHome.Persistence.Configurations;

public class UserFcmTokenConfiguration : IEntityTypeConfiguration<UserFcmToken>
{
    #region Methods
    public void Configure(EntityTypeBuilder<UserFcmToken> builder)
    {
        builder.ToTable(TableNames.UserFcmTokens);
        builder.HasKey(reidentToken => reidentToken.Id);
        builder.Property(reidentToken => reidentToken.Id).UseIdentityColumn();

        builder.Property(reidentToken => reidentToken.UserId).HasColumnName("user_id").IsRequired();
        builder.Property(reidentToken => reidentToken.DeviceId).HasColumnName("device_id").IsRequired();

        builder.Property(reidentToken => reidentToken.FCMToken).HasColumnName("fcm_token").IsRequired();
    }
    #endregion
}