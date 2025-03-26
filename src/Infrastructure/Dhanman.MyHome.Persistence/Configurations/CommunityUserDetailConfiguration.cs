using Dhanman.MyHome.Domain.Entities.CommunityUserDetails;
using Dhanman.MyHome.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dhanman.MyHome.Persistence.Configurations;

public class CommunityUserDetailConfiguration : IEntityTypeConfiguration<CommunityUserDetail>
{
    #region Methods
    public void Configure(EntityTypeBuilder<CommunityUserDetail> builder)
    {
        builder.ToTable(TableNames.CommunityUserDetails);
        builder.HasKey(communityUserDetail => communityUserDetail.Id);

        builder.Property(communityUserDetail => communityUserDetail.ResidentId).HasColumnName("resident_id").IsRequired();

        builder.Property(communityUserDetail => communityUserDetail.MemberType).HasColumnName("member_type").IsRequired(false);

        builder.Property(communityUserDetail => communityUserDetail.Designation).HasColumnName("designation").IsRequired(false);

        builder.Property(communityUserDetail => communityUserDetail.HattyId).HasColumnName("hatty_id").IsRequired(true);

        builder.Property(communityUserDetail => communityUserDetail.CurrentAddressId).HasColumnName("current_address_id").IsRequired(true);

        builder.Property(communityUserDetail => communityUserDetail.DateOfBirth).HasColumnName("date_of_birth").HasColumnType("timestamp").IsRequired();

        builder.Property(communityUserDetail => communityUserDetail.Gender).HasColumnName("gender").HasColumnType("char").IsRequired();

        builder.Property(communityUserDetail => communityUserDetail.MaritalStatus).HasColumnName("marital_status").IsRequired(false);

        builder.Property(communityUserDetail => communityUserDetail.AboutYourself).HasColumnName("about_yourself").IsRequired(false);

        builder.Property(communityUserDetail => communityUserDetail.SpouseName).HasColumnName("spouse_name").IsRequired(false);

        builder.Property(communityUserDetail => communityUserDetail.SpouseHattyId).HasColumnName("spouse_hatty_id").IsRequired(false);

        builder.Property(communityUserDetail => communityUserDetail.CreatedOnUtc).HasColumnType("timestamp").IsRequired();

        builder.Property(communityUserDetail => communityUserDetail.ModifiedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(communityUserDetail => communityUserDetail.DeletedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(communityUserDetail => communityUserDetail.IsDeleted).HasDefaultValue(false).IsRequired();

        builder.HasQueryFilter(communityUserDetail => !communityUserDetail.IsDeleted);
    }
    #endregion
}
