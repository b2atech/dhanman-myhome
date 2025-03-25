using Dhanman.MyHome.Domain.Entities.CommunityResidentRequests;
using Dhanman.MyHome.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dhanman.MyHome.Persistence.Configurations;

internal sealed class CommunityResidentRequestConfiguration : IEntityTypeConfiguration<CommunityResidentRequest>
{
    public void Configure(EntityTypeBuilder<CommunityResidentRequest> builder)
    {
        builder.ToTable(TableNames.CommunityResidentRequests);
        builder.HasKey(x => x.Id);

        // Main Properties
        builder.Property(communityResidentRequests => communityResidentRequests.MemberType).HasColumnName("member_type").IsRequired();
        builder.Property(communityResidentRequests => communityResidentRequests.UserName).HasColumnName("user_name").IsRequired();
        builder.Property(communityResidentRequests => communityResidentRequests.Password).HasColumnName("password").IsRequired();
        builder.Property(communityResidentRequests => communityResidentRequests.FirstName).HasColumnName("first_name").IsRequired();
        builder.Property(communityResidentRequests => communityResidentRequests.LastName).HasColumnName("last_name").IsRequired();
        builder.Property(communityResidentRequests => communityResidentRequests.HattyId).HasColumnName("hatty_id");
        builder.Property(communityResidentRequests => communityResidentRequests.Email).HasColumnName("email").IsRequired();
        builder.Property(communityResidentRequests => communityResidentRequests.MobileNumber).HasColumnName("mobile_number").IsRequired();
        builder.Property(communityResidentRequests => communityResidentRequests.CompanyName).HasColumnName("company_name");
        builder.Property(communityResidentRequests => communityResidentRequests.Designation).HasColumnName("designation");
        builder.Property(communityResidentRequests => communityResidentRequests.CurrentAddressId).HasColumnName("current_address_id").IsRequired();
        builder.Property(communityResidentRequests => communityResidentRequests.DateOfBirth).HasColumnName("date_of_birth").IsRequired();
        builder.Property(communityResidentRequests => communityResidentRequests.Gender).HasColumnName("gender").IsRequired();
        builder.Property(communityResidentRequests => communityResidentRequests.MaritalStatus).HasColumnName("marital_status");
        builder.Property(communityResidentRequests => communityResidentRequests.AboutYourSelf).HasColumnName("about_yourself");
        builder.Property(communityResidentRequests => communityResidentRequests.SpouseName).HasColumnName("spouse_name");
        builder.Property(communityResidentRequests => communityResidentRequests.SpouseHattyId).HasColumnName("spouse_hatty_id");

        // Audit Properties
        builder.Property(communityResidentRequests => communityResidentRequests.CreatedBy).HasColumnName("created_by").HasColumnType("uuid").IsRequired();
        builder.Property(communityResidentRequests => communityResidentRequests.ModifiedBy).HasColumnName("modified_by").HasColumnType("uuid");
        builder.Property(communityResidentRequests => communityResidentRequests.CreatedOnUtc).HasColumnName("created_on_utc").HasColumnType("timestamp").IsRequired();
        builder.Property(communityResidentRequests => communityResidentRequests.ModifiedOnUtc).HasColumnName("modified_on_utc").HasColumnType("timestamp");
        builder.Property(communityResidentRequests => communityResidentRequests.DeletedOnUtc).HasColumnName("deleted_on_utc").HasColumnType("timestamp");
        builder.Property(communityResidentRequests => communityResidentRequests.IsDeleted).HasColumnName("is_deleted").HasDefaultValue(false).IsRequired();

        // Query Filter
        builder.HasQueryFilter(communityResidentRequests => !communityResidentRequests.IsDeleted);
    }
}