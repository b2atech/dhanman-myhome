using Dhanman.MyHome.Domain.Entities.MemberRequests;
using Dhanman.MyHome.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dhanman.MyHome.Persistence.Configurations;

internal sealed class MemberRequestConfiguration : IEntityTypeConfiguration<MemberRequest>
{
    public void Configure(EntityTypeBuilder<MemberRequest> builder)
    {
        builder.ToTable(TableNames.MemberRequests);
        builder.HasKey(x => x.Id);

        // Main Properties
        builder.Property(memberRequests => memberRequests.MemberType).HasColumnName("member_type").IsRequired();
        builder.Property(memberRequests => memberRequests.UserName).HasColumnName("user_name").IsRequired();
        builder.Property(memberRequests => memberRequests.Password).HasColumnName("password").IsRequired();
        builder.Property(memberRequests => memberRequests.FirstName).HasColumnName("first_name").IsRequired();
        builder.Property(memberRequests => memberRequests.LastName).HasColumnName("last_name").IsRequired();
        builder.Property(memberRequests => memberRequests.HattyId).HasColumnName("hatty_id");
        builder.Property(memberRequests => memberRequests.Email).HasColumnName("email").IsRequired();
        builder.Property(memberRequests => memberRequests.MobileNumber).HasColumnName("mobile_number").IsRequired();
        builder.Property(memberRequests => memberRequests.CompanyName).HasColumnName("company_name");
        builder.Property(memberRequests => memberRequests.Designation).HasColumnName("designation");
        builder.Property(memberRequests => memberRequests.CurrentAddressId).HasColumnName("current_address_id").IsRequired();
        builder.Property(memberRequests => memberRequests.DateOfBirth).HasColumnName("date_of_birth").IsRequired();
        builder.Property(memberRequests => memberRequests.Gender).HasColumnName("gender").IsRequired();
        builder.Property(memberRequests => memberRequests.MaritalStatus).HasColumnName("marital_status");
        builder.Property(memberRequests => memberRequests.AboutYourSelf).HasColumnName("about_yourself");
        builder.Property(memberRequests => memberRequests.SpouseName).HasColumnName("spouse_name");
        builder.Property(memberRequests => memberRequests.SpouseHattyId).HasColumnName("spouse_hatty_id");
        builder.Property(memberRequests => memberRequests.CommunityRequestStatusId).HasColumnName("community_request_status_id");


        // Audit Properties
        builder.Property(memberRequests => memberRequests.CreatedBy).HasColumnName("created_by").HasColumnType("uuid").IsRequired();
        builder.Property(memberRequests => memberRequests.ModifiedBy).HasColumnName("modified_by").HasColumnType("uuid");
        builder.Property(memberRequests => memberRequests.CreatedOnUtc).HasColumnName("created_on_utc").HasColumnType("timestamp").IsRequired();
        builder.Property(memberRequests => memberRequests.ModifiedOnUtc).HasColumnName("modified_on_utc").HasColumnType("timestamp");
        builder.Property(memberRequests => memberRequests.DeletedOnUtc).HasColumnName("deleted_on_utc").HasColumnType("timestamp");
        builder.Property(memberRequests => memberRequests.IsDeleted).HasColumnName("is_deleted").HasDefaultValue(false).IsRequired();

        // Query Filter
        builder.HasQueryFilter(memberRequests => !memberRequests.IsDeleted);
    }
}