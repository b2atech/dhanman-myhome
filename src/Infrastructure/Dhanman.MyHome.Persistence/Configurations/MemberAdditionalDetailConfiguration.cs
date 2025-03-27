using Dhanman.MyHome.Domain.Entities.MemberAdditionalDetails;
using Dhanman.MyHome.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dhanman.MyHome.Persistence.Configurations;

public class MemberAdditionalDetailConfiguration : IEntityTypeConfiguration<MemberAdditionalDetail>
{
    #region Methods
    public void Configure(EntityTypeBuilder<MemberAdditionalDetail> builder)
    {
        builder.ToTable(TableNames.MemberAdditionalDetails);
        builder.HasKey(memberAdditionalDetail => memberAdditionalDetail.Id);

        builder.Property(memberAdditionalDetail => memberAdditionalDetail.MemberType).HasColumnName("member_type").IsRequired(false);

        builder.Property(memberAdditionalDetail => memberAdditionalDetail.UserName).HasColumnName("user_name").IsRequired(false);

        builder.Property(memberAdditionalDetail => memberAdditionalDetail.Password).HasColumnName("password").IsRequired(false);

        builder.Property(memberAdditionalDetail => memberAdditionalDetail.CompanyName).HasColumnName("company_name").IsRequired(false);

        builder.Property(memberAdditionalDetail => memberAdditionalDetail.Designation).HasColumnName("designation").IsRequired(false);

        builder.Property(memberAdditionalDetail => memberAdditionalDetail.HattyId).HasColumnName("hatty_id").IsRequired(true);

        builder.Property(memberAdditionalDetail => memberAdditionalDetail.DateOfBirth).HasColumnName("date_of_birth").HasColumnType("timestamp").IsRequired();

        builder.Property(memberAdditionalDetail => memberAdditionalDetail.Gender).HasColumnName("gender").HasColumnType("char").IsRequired();

        builder.Property(memberAdditionalDetail => memberAdditionalDetail.MaritalStatus).HasColumnName("marital_status").IsRequired(false);

        builder.Property(memberAdditionalDetail => memberAdditionalDetail.AboutYourself).HasColumnName("about_yourself").IsRequired(false);

        builder.Property(memberAdditionalDetail => memberAdditionalDetail.SpouseName).HasColumnName("spouse_name").IsRequired(false);

        builder.Property(memberAdditionalDetail => memberAdditionalDetail.SpouseHattyId).HasColumnName("spouse_hatty_id").IsRequired(false);

        builder.Property(memberAdditionalDetail => memberAdditionalDetail.CreatedOnUtc).HasColumnType("timestamp").IsRequired();

        builder.Property(memberAdditionalDetail => memberAdditionalDetail.ModifiedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(memberAdditionalDetail => memberAdditionalDetail.DeletedOnUtc).HasColumnType("timestamp").IsRequired(false);

        builder.Property(memberAdditionalDetail => memberAdditionalDetail.IsDeleted).HasDefaultValue(false).IsRequired();

        builder.HasQueryFilter(memberAdditionalDetail => !memberAdditionalDetail.IsDeleted);
    }
    #endregion
}
